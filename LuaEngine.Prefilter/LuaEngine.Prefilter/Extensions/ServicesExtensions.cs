using LuaEngine.Automaton.Models.Options;
using LuaEngine.Automaton.Runner;
using LuaEngine.Automaton.Services.Logger;
using LuaEngine.Automaton.Services.Logger.Abstractions;
using LuaEngine.Automaton.Services.Strategies;
using LuaEngine.Automaton.Services.Strategies.Abstractions;
using LuaEngine.Prefilter.Models.Options;
using LuaEngine.Prefilter.Repositories;
using LuaEngine.Prefilter.Repositories.Abstractions;
using LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;
using static LuaEngine.Prefilter.Constants.AppConstants;

namespace LuaEngine.Prefilter.Extensions;

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
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var refitClientOptions = configuration.GetSection(ScriptsConfigSectionName)
                                              .Get<ScriptsApiOptions>();

        if (refitClientOptions is null)
            throw new NullReferenceException(nameof(refitClientOptions));

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                RetryPolicyAttemptsCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(RetryPolicySecondsBetweenRetries, retryAttempt)));

        services.AddRefitClientWithPolicy<IRuleScriptClient>(refitClientOptions, retryPolicy);
        services.AddRefitClientWithPolicy<IProcessScriptClient>(refitClientOptions, retryPolicy);
        services.AddRefitClientWithPolicy<IScriptVersionClient>(refitClientOptions, retryPolicy);

        services.AddScoped<IRuleScriptRepository, RuleScriptRepository>();
        services.AddScoped<IProcessScriptRepository, ProcessScriptRepository>();
        services.AddScoped<IScriptVersionRepository, ScriptVersionRepository>();

        return services;
    }

    /// <summary>
    /// Добавить refit-клиент
    /// </summary>
    private static void AddRefitClientWithPolicy<TClient>(this IServiceCollection services,
                                                         IRefitClientOptions options,
                                                         IAsyncPolicy<HttpResponseMessage> policy)
                                                            where TClient : class
    {
        var serializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        //serializerOptions.Converters.Add(new ObjectToInferredTypesConverter());
        serializerOptions.Converters.Add(new JsonStringEnumConverter());

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(serializerOptions)
        };

        services.AddRefitClient<TClient>(refitSettings)
            .ConfigureHttpClient(h =>
            {
                h.BaseAddress = new Uri(options.Host);
            });
        //.AddPolicyHandler(policy);
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
