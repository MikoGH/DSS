using LuaEngine.Scripts.WebApi.Services.Caching.Abstracts;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;
using LuaEngine.Scripts.WebApi.Models.Options;
using StackExchange.Redis;
using Monq.Core.MvcExtensions.Extensions;

namespace LuaEngine.Scripts.WebApi.Services.Caching;

/// <summary>
/// Сервис кэширования.
/// </summary>
public class CacheService : ICacheService
{
    private readonly IDatabase _cache;
    private readonly TimeSpan _defaultLifeTime;
    private readonly ILogger<CacheService> _logger;
    private readonly string _keyPrefix;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly int _chunkSize;

    /// <summary>
    /// Конструктор сервиса кэширования.
    /// </summary>
    /// <param name="connectionService"></param>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    public CacheService(IRedisConnectionService connectionService, IOptions<CacheOptions> options, ILogger<CacheService> logger)
    {
        var connection = connectionService.GetConnection();
        _cache = connection.GetDatabase();
        _defaultLifeTime = options.Value.DefaultLifeTime;
        _keyPrefix = $"{options.Value.CommonPrefix}:{options.Value.ServiceName}";
        _chunkSize = options.Value.ChunkSize;
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    /// <inheritdoc/>
    public Task<bool> KeyExistsAsync<TValue>(Dictionary<string, string>? keyParams, CancellationToken token)
        => _cache.KeyExistsAsync(GetValuesKey<TValue>(keyParams));

    /// <inheritdoc/>
    public async Task<IEnumerable<TValue>> GetAllAsync<TValue>(Dictionary<string, string>? keyParams, CancellationToken token)
    {
        var hashFields = await _cache.HashKeysAsync(GetValuesKey<TValue>(keyParams));

        if (hashFields is null || hashFields.Length == 0)
            return [];

        var hashFieldsChunks = hashFields.Chunk(_chunkSize).ToArray();

        var hashValues = new List<RedisValue>();
        for (var chunkNumber = 0; chunkNumber < hashFieldsChunks.Length; chunkNumber++)
        {
            var hashValuesChunk = await _cache.HashGetAsync(GetValuesKey<TValue>(keyParams), hashFieldsChunks[chunkNumber]);
            hashValues.AddRange(hashValuesChunk);
        }

        return hashValues
            .Where(value => !value.IsNull)
            .Select(value => JsonSerializer.Deserialize<TValue>(value!, _serializerOptions)!)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<TValue?> GetAsync<TId, TValue>(TId id, Dictionary<string, string>? keyParams, CancellationToken token)
    {
        var hashValue = await _cache.HashGetAsync(GetValuesKey<TValue>(keyParams), JsonSerializer.Serialize(id, _serializerOptions));

        if (hashValue.IsNull)
            return default;

        return JsonSerializer.Deserialize<TValue>(hashValue!, _serializerOptions);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TValue>> GetAsync<TId, TValue>(IEnumerable<TId> ids, Dictionary<string, string>? keyParams, CancellationToken token)
    {
        ids = ids
            .Where(x => x is not null)
            .ToList();

        var hashFieldsChunks = ids
            .Select(x => new RedisValue(JsonSerializer.Serialize(x, _serializerOptions)))
            .Chunk(_chunkSize)
            .ToArray();

        var hashValues = new List<RedisValue>();
        for (var chunkNumber = 0; chunkNumber < hashFieldsChunks.Length; chunkNumber++)
        {
            var hashValuesChunk = await _cache.HashGetAsync(GetValuesKey<TValue>(keyParams), hashFieldsChunks[chunkNumber]);
            hashValues.AddRange(hashValuesChunk);
        }

        if (hashValues is null || hashValues.Count == 0)
            return [];

        return hashValues
            .Where(value => !value.IsNull)
            .Select(value => JsonSerializer.Deserialize<TValue>(value!, _serializerOptions)!)
            .ToList();
    }

    /// <inheritdoc/>
    public Task UpsertAsync<TId, TValue>(TId id, TValue value, Dictionary<string, string>? keyParams, CancellationToken token)
    {
        return Task.WhenAll(
            UpsertManageableTypeAsync<TValue>(keyParams, token),
            _cache.HashSetAsync(GetTtlKey<TValue>(keyParams), JsonSerializer.Serialize(id, _serializerOptions), JsonSerializer.Serialize(DateTimeOffset.UtcNow + _defaultLifeTime)),
            _cache.HashSetAsync(GetValuesKey<TValue>(keyParams), JsonSerializer.Serialize(id, _serializerOptions), JsonSerializer.Serialize(value, _serializerOptions))
        );
    }

    /// <inheritdoc/>
    public async Task UpsertAsync<TId, TValue>(Dictionary<TId, TValue> values, Dictionary<string, string>? keyParams, CancellationToken token)
    where TId : notnull
    {
        if (values is null)
            return;

        var expiresAt = DateTimeOffset.UtcNow + _defaultLifeTime;

        var entriesTtl = values
            .Select(x => new HashEntry(JsonSerializer.Serialize(x.Key, _serializerOptions), JsonSerializer.Serialize(expiresAt, _serializerOptions)))
            .Chunk(_chunkSize)
            .ToArray();

        var entries = values
            .Select(x => new HashEntry(JsonSerializer.Serialize(x.Key, _serializerOptions), JsonSerializer.Serialize(x.Value, _serializerOptions)))
            .Chunk(_chunkSize)
            .ToArray();

        await UpsertManageableTypeAsync<TValue>(keyParams, token);

        for (var chunkNumber = 0; chunkNumber < entries.Length; chunkNumber++)
        {
            await Task.WhenAll(_cache.HashSetAsync(GetTtlKey<TValue>(keyParams), entriesTtl[chunkNumber]),
                               _cache.HashSetAsync(GetValuesKey<TValue>(keyParams), entries[chunkNumber]));
        }
    }

    /// <inheritdoc/>
    public async Task UpsertWithFallbackAsync<TId, TValue>(TId id, TValue value, Dictionary<string, string>? keyParams, CancellationToken token)
    {
        try
        {
            await UpsertAsync(id, value, keyParams, token);
        }
        catch
        {
            if (_cache.IsConnected(default))
            {
                try
                {
                    await RemoveAsync<TId, TValue>(id, keyParams, token);
                }
                catch { }
            }
        }
    }

    /// <remarks>В случае ошибки записи в Redis удаляет значения.</remarks>
    /// <inheritdoc/>
    public async Task UpsertWithFallbackAsync<TId, TValue>(Dictionary<TId, TValue> values, Dictionary<string, string>? keyParams, CancellationToken token)
    where TId : notnull
    {
        try
        {
            await UpsertAsync(values, keyParams, token);
        }
        catch
        {
            if (_cache.IsConnected(default))
            {
                try
                {
                    await RemoveAsync<TId, TValue>(values.Keys, keyParams, token);
                }
                catch { }
            }
        }
    }

    /// <inheritdoc/>
    public Task<bool> IdExistsAsync<TId, TValue>(TId id, Dictionary<string, string>? keyParams, CancellationToken token)
        => _cache.HashExistsAsync(GetValuesKey<TValue>(keyParams), JsonSerializer.Serialize(id, _serializerOptions));

    /// <inheritdoc/>
    public Task RemoveAsync<TId, TValue>(TId id, Dictionary<string, string>? keyParams, CancellationToken token)
    {
        return Task.WhenAll(
            _cache.HashDeleteAsync(GetValuesKey<TValue>(keyParams), JsonSerializer.Serialize(id, _serializerOptions)),
            _cache.HashDeleteAsync(GetTtlKey<TValue>(keyParams), JsonSerializer.Serialize(id, _serializerOptions))
        );
    }

    /// <inheritdoc/>
    public Task RemoveAsync<TId, TValue>(IEnumerable<TId> ids, Dictionary<string, string>? keyParams, CancellationToken token)
    {
        var hashFields = ids
            .Where(x => x is not null)
            .Select(x => new RedisValue(JsonSerializer.Serialize(x, _serializerOptions)))
            .ToArray();
        return Task.WhenAll(
            _cache.HashDeleteAsync(GetValuesKey<TValue>(keyParams), hashFields),
            _cache.HashDeleteAsync(GetTtlKey<TValue>(keyParams), hashFields)
        );
    }

    /// <inheritdoc/>
    public Task RemoveAsync(string entityWithKeyParams, IEnumerable<string> ids, CancellationToken token)
    {
        var hashFields = ids
            .Where(x => x is not null)
            .Select(x => new RedisValue(x))
            .ToArray();
        return Task.WhenAll(
            _cache.HashDeleteAsync(GetValuesKey(entityWithKeyParams), hashFields),
            _cache.HashDeleteAsync(GetTtlKey(entityWithKeyParams), hashFields)
        );
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetExpiredIdsAsync(string entityWithKeyParams, CancellationToken token)
    {
        if (string.IsNullOrEmpty(entityWithKeyParams))
        {
            _logger?.LogCritical(new EventId(), $"[{nameof(CacheService)}]. Передан тип null или пустая строка");
            return [];
        }

        var collection = await _cache.HashGetAllAsync(GetTtlKey(entityWithKeyParams));

        if (collection.IsEmpty())
            return [];

        var expiredIds = collection
            .Where(entry => !entry.Value.IsNull)
            .Where(entry => JsonSerializer.Deserialize<DateTimeOffset>(entry.Value!) < DateTimeOffset.UtcNow)
            .Select(entry => entry.Name.ToString())
            .ToList();

        return expiredIds;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetManageableTypesAsync(CancellationToken token)
    {
        var manageableTypes = await _cache.SetMembersAsync(GetManageableTypesKey());
        return manageableTypes.Select(x => x.ToString());
    }

    /// <summary>
    /// Добавить контролируемый тип в коллекцию.
    /// </summary>
    /// <typeparam name="TValue">Тип сущности.</typeparam>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    private Task<bool> UpsertManageableTypeAsync<TValue>(Dictionary<string, string>? keyParams, CancellationToken token)
    {
        string manageableType = GetEntityWithKeyParams<TValue>(keyParams);
        return _cache.SetAddAsync(GetManageableTypesKey(), new RedisValue(manageableType));
    }

    /// <summary>
    /// Получить ключ для сущности <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <returns>Ключ для сущности <typeparamref name="T"/>.</returns>
    private string GetValuesKey<T>(Dictionary<string, string>? keyParams = null)
        => GetValuesKey(typeof(T), keyParams);

    /// <summary>
    /// Получить ключ для сущности <paramref name="type"/>.
    /// </summary>
    /// <param name="type">Тип сущности.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <returns>Ключ для сущности <paramref name="type"/>.</returns>
    private string GetValuesKey(Type type, Dictionary<string, string>? keyParams = null)
        => GetKey(type, "values", keyParams);

    /// <summary>
    /// Получить ключ для сущности <paramref name="entityWithKeyParams"/>.
    /// </summary>
    /// <param name="entityWithKeyParams">Тип сущности с дополнительными параметрами.</param>
    /// <returns>Ключ для сущности <paramref name="entityWithKeyParams"/>.</returns>
    private string GetValuesKey(string entityWithKeyParams)
        => $"{_keyPrefix}:{entityWithKeyParams}:values";

    /// <summary>
    /// Получить ключ для времени жизни для сущности <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <returns>Ключ для времени жизни для сущности <typeparamref name="T"/>.</returns>
    private string GetTtlKey<T>(Dictionary<string, string>? keyParams = null)
        => GetTtlKey(typeof(T), keyParams);

    /// <summary>
    /// Получить ключ для времени жизни для сущности <paramref name="type"/>.
    /// </summary>
    /// <param name="type">Тип сущности.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <returns>Ключ для времени жизни для сущности <paramref name="type"/>.</returns>
    private string GetTtlKey(Type type, Dictionary<string, string>? keyParams = null)
        => GetKey(type, "TTL", keyParams);

    /// <summary>
    /// Получить ключ для времени жизни для сущности <paramref name="entityWithKeyParams"/>.
    /// </summary>
    /// <param name="entityWithKeyParams">Тип сущности с дополнительными параметрами.</param>
    /// <returns>Ключ для времени жизни для сущности <paramref name="entityWithKeyParams"/>.</returns>
    private string GetTtlKey(string entityWithKeyParams)
        => $"{_keyPrefix}:{entityWithKeyParams}:TTL";

    private string GetKey(Type type, string valuesOrTtl, Dictionary<string, string>? keyParams = null)
    {
        if (keyParams is null || keyParams.Count == 0)
        {
            string? entity = ConvertPascalCaseToKebabCase(type.Name);
            return $"{_keyPrefix}:{entity}:{valuesOrTtl}";
        }

        string entityWithKeyParams = GetEntityWithKeyParams(type, keyParams);
        return $"{_keyPrefix}:{entityWithKeyParams}:{valuesOrTtl}";
    }

    /// <summary>
    /// Получить ключ, содержащий коллекцию контролируемых типов.
    /// </summary>
    /// <returns>Ключ, содержащий коллекцию контролируемых типов.</returns>
    private string GetManageableTypesKey()
    {
        return $"{_keyPrefix}:manageable-types";
    }

    private static string GetEntityWithKeyParams<T>(Dictionary<string, string>? keyParams)
        => GetEntityWithKeyParams(typeof(T), keyParams);

    private static string GetEntityWithKeyParams(Type type, Dictionary<string, string>? keyParams)
    {
        string? entity = ConvertPascalCaseToKebabCase(type.Name);
        if (keyParams is null || keyParams.Count == 0)
            return entity;
        string stringKeyParams = ConvertKeyParamsToString(keyParams);
        return $"{entity}:{stringKeyParams}";
    }

    private static string ConvertKeyParamsToString(Dictionary<string, string> keyParams)
    {
        var stringKeyParams = keyParams
            .Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value))
            .Select(x => new KeyValuePair<string, string>(ConvertPascalCaseToKebabCase(x.Key)!, x.Value))
            .Select(x => $"{x.Key}={x.Value}");
        return string.Join("&", stringKeyParams);
    }

    private static string ConvertPascalCaseToKebabCase(string? value)
    {
        if (value is null)
            return string.Empty;

        var camelCase = Regex.Replace(value, "^[A-Z]", x => x.Value.ToLower());
        var kebabCase = Regex.Replace(camelCase, "(?=.)[A-Z]", x => $"-{x.Value.ToLower()}");
        return kebabCase;
    }
}
