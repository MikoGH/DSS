using LuaEngine.Automaton.Models.Options;
using LuaEngine.Automaton.Runner;
using LuaEngine.Automaton.Services.Logger.Abstractions;
using LuaEngine.Automaton.Services.Logger;
using LuaEngine.Automaton.Services.Strategies.Abstractions;
using LuaEngine.Automaton.Services.Strategies;
using LuaEngine.LuaAgent.Services;
using static LuaEngine.LuaAgent.Constants.AppConstants;

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
        services.AddHostedService<HubConnectionService>();

        return services;
    }

    // TODO: вынести в проект Automaton.
    /// <summary>
    /// Добавить сервисы движка.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов служб.</param>
    /// <returns>Коллекция дескрипторов служб.</returns>
    public static IServiceCollection AddAutomatonServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        var automatonConfig = configuration.GetSection(AutomatonSectionName).Get<AutomatonEngineOptions>();

        services.AddTransient<IAutomatonStrategy, DefaultScriptStrategy>();
        services.AddTransient(typeof(IAutomatonLogger<>), typeof(AutomatonLogger<>));
        services.AddTransient<AutomatonRunnerContext>();

        return services;
    }
}
