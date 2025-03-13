using LuaEngine.LuaOrchestrator.Models.Options;
using LuaEngine.LuaOrchestrator.Services;
using MassTransit;
using System.Reflection;
using static LuaEngine.LuaOrchestrator.Constants.AppConstants;

namespace LuaEngine.LuaOrchestrator.Extensions;

/// <summary>
/// Методы расширения RabbitMQ.
/// </summary>
public static class RabbitMqExtensions
{
    /// <summary>
    /// Добавить RabbitMQ.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов служб.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <param name="assemblies">Сборки, в которых есть потребители.</param>
    /// <returns>Коллекция дескрипторов служб.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        var rabbitMqConfig = configuration.GetSection(RabbitMqSectionName).Get<RabbitMqOptions>();

        if (rabbitMqConfig is null)
            throw new ArgumentNullException("В конфигурации отсутствуют данные подключения к RabbitMQ.");

        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = rabbitMqConfig.WaitUntilStarted;
                options.StartTimeout = rabbitMqConfig.StartTimeout;
            });

        services.AddMassTransit(x =>
        {
            x.AddConsumers(assemblies);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqConfig.Host, rabbitMqConfig.Port, rabbitMqConfig.VirtualHost, h =>
                {
                    h.Username(rabbitMqConfig.UserName);
                    h.Password(rabbitMqConfig.Password);

                    h.RequestedConnectionTimeout(rabbitMqConfig.RequestedConnectionTimeout);
                });

                //cfg.AutoStart = true;

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
