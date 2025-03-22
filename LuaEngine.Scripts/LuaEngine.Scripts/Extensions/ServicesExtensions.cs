using LuaEngine.Scripts.WebApi.Repositories;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using LuaEngine.Scripts.WebApi.Services;
using LuaEngine.Scripts.WebApi.Services.Abstracts;

namespace LuaEngine.Scripts.WebApi.Extensions;

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
        services.AddTransient<IPrefilterScriptService, PrefilterScriptService>();
        services.AddTransient<IRuleScriptService, RuleScriptService>();
        services.AddTransient<IProcessScriptService, ProcessScriptService>();
        services.AddTransient<IScriptVersionService, ScriptVersionService>();
        services.AddTransient<IScriptValidator, ScriptValidator>();

        return services;
    }

    /// <summary>
    /// Добавить репозитории.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов служб.</param>
    /// <returns>Коллекция дескрипторов служб.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IPrefilterScriptRepository, PrefilterScriptRepository>();
        services.AddTransient<IRuleScriptRepository, RuleScriptRepository>();
        services.AddTransient<IProcessScriptRepository, ProcessScriptRepository>();
        services.AddTransient<IScriptVersionRepository, ScriptVersionRepository>();

        return services;
    }
}
