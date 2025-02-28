namespace LuaEngine.Prefilter.Constants;

/// <summary>
/// Константы приложения.
/// </summary>
public class AppConstants
{
    /// <summary>
    /// Название приложения в Swagger.
    /// </summary>
    public const string SwaggerApiName = "Prefilter API";

    /// <summary>
    /// Префикс пути.
    /// </summary>
    public const string RoutePrefix = "prefilter-api";

    /// <summary>
    /// Название секции RabbitMQ.
    /// </summary>
    public const string RabbitMqSectionName = "RabbitMq";

    /// <summary>
    /// Название секции в appsettings откуда брать название очереди исходных данных.
    /// </summary>
    public const string RawDataQueueNameSection = "Queues:RawDataQueue";

    /// <summary>
    /// Название секции в appsettings откуда брать префикс названия очереди.
    /// </summary>
    public const string PrefixSection = "CommonPrefix";

    /// <summary>
    /// Префикс для названия очереди.
    /// </summary>
    public const string Queue = "queue:";

    /// <summary>
    /// Название конфигурационной секции HTTP-клиента.
    /// </summary>
    public const string ScriptsConfigSectionName = "ScriptsServiceApi";

    /// <summary>
    /// Количество попыток для retry политики HTTP-клиента.
    /// </summary>
    public const int RetryPolicyAttemptsCount = 2;

    /// <summary>
    /// Количество секунд между повторными запросами для retry политики HTTP-клиента.
    /// </summary>
    public const int RetryPolicySecondsBetweenRetries = 3;
}
