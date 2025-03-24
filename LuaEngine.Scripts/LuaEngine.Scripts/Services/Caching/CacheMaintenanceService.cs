using LuaEngine.Scripts.WebApi.Models.Options;
using LuaEngine.Scripts.WebApi.Services.Caching.Abstracts;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace LuaEngine.Scripts.WebApi.Services.Caching;

/// <summary>
/// Сервис самообслуживания кэша.
/// </summary>
public class CacheMaintenanceService : BackgroundService
{
    private readonly TimeSpan _executionInterval;

    private readonly IServiceProvider _provider;
    private readonly ILogger<CacheMaintenanceService> _logger;

    /// <summary>
    /// Конструктор сервиса самообслуживания кэша.
    /// </summary>
    public CacheMaintenanceService(IServiceProvider provider, IOptions<CacheMaintenanceOptions>? options, ILogger<CacheMaintenanceService> logger)
    {
        _provider = provider;
        _executionInterval = options?.Value.ExecutionInterval ?? TimeSpan.FromMinutes(5);
        _logger = logger;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken token)
    {
        _logger.LogInformation($"{nameof(CacheMaintenanceService)} запускается.");

        var stopwatch = new Stopwatch();
        for (var cycle = 1L; !token.IsCancellationRequested; cycle++)
        {
            _logger.LogInformation($"[{nameof(CacheMaintenanceService)}]: Цикл #{cycle}");
            stopwatch.Restart();

            await HandleCycle(token);
            var message = $"[{nameof(CacheMaintenanceService)}]: Цикл #{cycle} завершён. Времени затрачено: {stopwatch.Elapsed}.";

            await Task.Delay(_executionInterval - stopwatch.Elapsed, token);
        }
    }

    private async Task HandleCycle(CancellationToken token)
    {
        try
        {
            using var scope = _provider.CreateScope();
            await CleanUpCache(scope, token);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(new EventId(), ex, ex.Message, ex);
        }
    }

    private async Task CleanUpCache(IServiceScope scope, CancellationToken token)
    {
        var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();

        var manageableTypes = await cache.GetManageableTypesAsync(token);

        foreach (var type in manageableTypes)
        {
            var expiredIds = await cache.GetExpiredIdsAsync(type, token);

            var expiredCount = expiredIds.Count();
            if (expiredCount == 0)
            {
                _logger.LogInformation($"[{nameof(CacheMaintenanceService)}]: Записей с истёкшим сроком жизни типа {type} не найдено.");
                continue;
            }
            _logger.LogInformation($"[{nameof(CacheMaintenanceService)}]: Записей типа {type} к удалению: {expiredCount}.");

            await cache.RemoveAsync(type, expiredIds, token);

            var entryList = string.Join(", ", expiredIds.Select(id => $"{type}/{id}"));
            _logger.LogInformation($"[{nameof(CacheMaintenanceService)}]: Записей с истёкшим сроком жизни успешно удалено: {entryList}.");
        }
    }
}
