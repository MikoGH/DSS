using LuaEngine.Prefilter.Models.Options;
using LuaEngine.Prefilter.Repositories;
using LuaEngine.Prefilter.Repositories.Abstractions;
using LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System.Text.Json;
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

        services.AddScoped<IRuleScriptRepository, RuleScriptRepository>();
        services.AddScoped<IProcessScriptRepository, ProcessScriptRepository>();

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
        serializerOptions.Converters.Add(new ObjectToInferredTypesConverter());

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
}
