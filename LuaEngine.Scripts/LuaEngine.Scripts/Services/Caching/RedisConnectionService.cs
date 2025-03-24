using LuaEngine.Scripts.WebApi.Services.Caching.Abstracts;
using StackExchange.Redis;

namespace LuaEngine.Scripts.WebApi.Services.Caching;

/// <inheritdoc/>
public class RedisConnectionService : IRedisConnectionService
{
    private readonly Lazy<IConnectionMultiplexer> _connection;

    /// <inheritdoc/>
    public RedisConnectionService(string? connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString), "Отсутствует строка подключения");

        var options = ConfigurationOptions.Parse(connectionString);
        options.AbortOnConnectFail = false;

        _connection = new Lazy<IConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(options));
    }

    /// <inheritdoc/>
    public IConnectionMultiplexer GetConnection() => _connection.Value;
}
