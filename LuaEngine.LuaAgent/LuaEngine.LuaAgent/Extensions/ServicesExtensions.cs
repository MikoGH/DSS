using LuaEngine.LuaAgent.Services;
using LuaEngine.LuaAgent.Services.Abstractions;

namespace LuaEngine.LuaAgent.Extensions;

/// <summary>
/// Методы расширений коллекции дескрипторов служб.
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// Добавить сервисы.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов служб.</param>
    /// <returns>Коллекция дескрипторов служб.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IScriptExecutorService, ScriptExecutorService>();

        return services;
    }
}
