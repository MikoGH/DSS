using StackExchange.Redis;

namespace LuaEngine.Scripts.WebApi.Services.Caching.Abstracts;

/// <summary>
/// Сервис подключения к Redis
/// </summary>
public interface IRedisConnectionService
{
    /// <summary>
    /// Получить подключение
    /// </summary>
    IConnectionMultiplexer GetConnection();
}
