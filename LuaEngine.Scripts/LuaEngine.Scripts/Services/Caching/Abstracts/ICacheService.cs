namespace LuaEngine.Scripts.WebApi.Services.Caching.Abstracts;

/// <summary>
/// Интерфейс, предоставляющий базовые операции работы с кэшем.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Проверка существования ключа в кэше.
    /// </summary>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если ключ существует в кэше, иначе <see langword="false"/>.</returns>
    Task<bool> KeyExistsAsync<TValue>(Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Получить все значения из кэша.
    /// </summary>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция элементов <typeparamref name="TValue"/>, содержащаяся в кэше.</returns>
    Task<IEnumerable<TValue>> GetAllAsync<TValue>(Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Получить значение по идентификатору.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="id">Идентификатор.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Объект <typeparamref name="TValue"/> с заданным идентификатором.</returns>
    Task<TValue?> GetAsync<TId, TValue>(TId id, Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Получить коллекцию по идентификаторам.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="ids">Коллекция идентификаторов.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция объектов <typeparamref name="TValue"/> с заданным идентификатором.</returns>
    Task<IEnumerable<TValue>> GetAsync<TId, TValue>(IEnumerable<TId> ids, Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Добавить или обновить значение.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="id">Идентификатор.</param>
    /// <param name="value">Обновленный объект <typeparamref name="TValue"/>.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    Task UpsertAsync<TId, TValue>(TId id, TValue value, Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Добавить или обновить коллекцию значений.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="values">Словарь обновленных объектов.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    Task UpsertAsync<TId, TValue>(Dictionary<TId, TValue> values, Dictionary<string, string>? keyParams, CancellationToken token) where TId : notnull;

    /// <summary>
    /// Добавить или обновить значение.
    /// В случае ошибки записи в Redis удаляет значение.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="id">Идентификатор.</param>
    /// <param name="value">Обновленный объект <typeparamref name="TValue"/>.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    Task UpsertWithFallbackAsync<TId, TValue>(TId id, TValue value, Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Добавить или обновить коллекцию значений.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="values">Словарь обновленных объектов.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    Task UpsertWithFallbackAsync<TId, TValue>(Dictionary<TId, TValue> values, Dictionary<string, string>? keyParams, CancellationToken token) where TId : notnull;

    /// <summary>
    /// Проверка существования идентификатора.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="id">Идентификатор.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если элемент с заданным идентификатором существует, иначе <see langword="false"/>.</returns>
    Task<bool> IdExistsAsync<TId, TValue>(TId id, Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Удаление элемента по идентификатору.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="id">Идентификатор.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    Task RemoveAsync<TId, TValue>(TId id, Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Удаление элементов по идентификаторам.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <typeparam name="TValue">Тип элемента.</typeparam>
    /// <param name="ids">Идентификаторы.</param>
    /// <param name="keyParams">Дополнительные параметры для ключа.</param>
    /// <param name="token">Токен отмены.</param>
    Task RemoveAsync<TId, TValue>(IEnumerable<TId> ids, Dictionary<string, string>? keyParams, CancellationToken token);

    /// <summary>
    /// Удаление элементов по идентификаторам.
    /// </summary>
    /// <param name="entityWithKeyParams">Тип коллекции элементов с дополнительными параметрами ключа.</param>
    /// <param name="ids">Идентификаторы.</param>
    /// <param name="token">Токен отмены.</param>
    Task RemoveAsync(string entityWithKeyParams, IEnumerable<string> ids, CancellationToken token);

    /// <summary>
    /// Получение всех идентификаторов элементов с истекшим сроком жизни.
    /// </summary>
    /// <param name="entityWithKeyParams">Тип коллекции элементов с дополнительными параметрами ключа.</param>
    /// <param name="token">Токен отмены.</param>
    Task<IEnumerable<string>> GetExpiredIdsAsync(string entityWithKeyParams, CancellationToken token);

    /// <summary>
    /// Получение коллекции контролируемых типов.
    /// </summary>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция контролируемых типов.</returns>
    Task<IEnumerable<string>> GetManageableTypesAsync(CancellationToken token);
}
