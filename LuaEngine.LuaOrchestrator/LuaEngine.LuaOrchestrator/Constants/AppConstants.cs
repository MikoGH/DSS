namespace LuaEngine.LuaOrchestrator.Constants;

/// <summary>
/// Константы приложения.
/// </summary>
public class AppConstants
{
    /// <summary>
    /// Название приложения в Swagger.
    /// </summary>
    public const string SwaggerApiName = "LuaEngine API";

    /// <summary>
    /// Префикс пути.
    /// </summary>
    public const string RoutePrefix = "lua-engine-api";

    /// <summary>
    /// Название секции RabbitMQ.
    /// </summary>
    public const string RabbitMqSectionName = "RabbitMQ";

    /// <summary>
    /// Название секции в appsettings откуда брать название очереди отфильтрованных данных.
    /// </summary>
    public const string FilteredDataQueueNameSection = "Queues:FilteredDataQueue";

    /// <summary>
    /// Название секции в appsettings откуда брать префикс названия очереди.
    /// </summary>
    public const string PrefixSection = "CommonPrefix";

    /// <summary>
    /// Префикс для названия очереди.
    /// </summary>
    public const string Queue = "queue:";

}
