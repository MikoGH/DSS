namespace LuaEngine.Scripts.WebApi.Models.Options;

/// <summary>
/// Класс конфигурации сервиса самообслуживания кеша.
/// </summary>
public class CacheMaintenanceOptions
{
    /// <summary>
    /// Частота запуска сервиса.
    /// </summary>
    public TimeSpan ExecutionInterval { get; set; }
}
