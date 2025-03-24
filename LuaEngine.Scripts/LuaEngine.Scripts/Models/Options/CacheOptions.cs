namespace LuaEngine.Scripts.WebApi.Models.Options;

/// <summary>
/// Класс конфигурации сервиса кэширования.
/// </summary>
public class CacheOptions
{
    /// <summary>
    /// Период хранения записей по умолчанию.
    /// </summary>
    public TimeSpan DefaultLifeTime { get; set; }

    /// <summary>
    /// Префикс ключа в Redis.
    /// </summary>
    public string CommonPrefix { get; set; }

    /// <summary>
    /// Название сервиса для ключа в Redis.
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Количество элементов в чанке.
    /// </summary>
    public int ChunkSize { get; set; } = 1000;

    /// <summary>
    /// Конструктор конфигурации сервиса кэширования без параметров.
    /// </summary>
    public CacheOptions()
    {
    }

    /// <summary>
    /// Конструктор конфигурации сервиса кэширования.
    /// </summary>
    /// <param name="defaultLifeTime">Период хранения записей по умолчанию.</param>
    /// <param name="commonPrefix">Префикс ключа в Redis.</param>
    /// <param name="serviceName">Название сервиса для ключа в Redis.</param>
    /// <param name="chunkSize">Количество элементов в чанке.</param>
    public CacheOptions(TimeSpan defaultLifeTime, string commonPrefix, string serviceName, int chunkSize)
    {
        DefaultLifeTime = defaultLifeTime;
        CommonPrefix = commonPrefix;
        ServiceName = serviceName;
        ChunkSize = chunkSize;
    }
}
