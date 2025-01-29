using Microsoft.EntityFrameworkCore;
using Npgsql;
using LuaEngine.Scripts.WebApi.Database;

namespace LuaEngine.Scripts.WebApi.Extensions;

/// <summary>
/// Методы расширения Postgres.
/// </summary>
public static class PostgresExtensions
{
    /// <summary>
    /// Добавить контекст базы данных PostgreSQL.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="connectionString">Строка подключения к БД.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static IServiceCollection AddPostgresDbContext(this IServiceCollection services, string? connectionString)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException($"'{nameof(connectionString)}': строка подключения к БД пустая.", nameof(connectionString));

        var dataSource = new NpgsqlDataSourceBuilder(connectionString)
            .EnableDynamicJson()
            .Build();

        services.AddDbContext<ScriptsContext>(options => options
            .UseNpgsql(dataSource, opt => opt
                .EnableRetryOnFailure(3))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    /// <summary>
    /// Выполнение миграций БД при запуске
    /// </summary>
    /// <param name="app">Конвейер обработки запросов приложения.</param>
    public static async Task MigrateDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ScriptsContext>();
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetService<ILogger<ScriptsContext>>();
            logger?.LogError(ex, "Произошла ошибка при миграции БД.");
            throw;
        }
    }
}
