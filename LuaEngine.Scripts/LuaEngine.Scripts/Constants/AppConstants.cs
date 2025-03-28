namespace LuaEngine.Scripts.WebApi.Constants;

/// <summary>
/// Константы приложения.
/// </summary>
public class AppConstants
{
    /// <summary>
    /// Название приложения в Swagger.
    /// </summary>
    public const string SwaggerApiName = "Scripts API";

    /// <summary>
    /// Префикс пути.
    /// </summary>
    public const string RoutePrefix = "scripts-api";

    /// <summary>
    /// Название секции строки подключения к БД.
    /// </summary>
    public const string PostgresConnectionStringSectionName = "PostgreSQL";

    /// <summary>
    /// Название секции строки подключения к Graylog.
    /// </summary>
    public const string GraylogConnectionStringSectionName = "Graylog";

    /// <summary>
    /// Имя секции конфигурации Redis в appsettings.
    /// </summary>
    public const string RedisSectionName = "Redis";

    /// <summary>
    /// Название секции в appsettings - connection strings.
    /// </summary>
    public const string RedisConnectionString = "Redis";

}
