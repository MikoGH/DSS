using LuaEngine.LuaOrchestrator.Hubs;
using LuaEngine.LuaOrchestrator.Models;
using MassTransit;

namespace LuaEngine.LuaOrchestrator.Services;

/// <summary>
/// Обработчик сообщений <see cref="FilteredData"/>.
/// </summary>
public class FilteredDataConsumer : IConsumer<FilteredData>
{
    private readonly ILogger<FilteredDataConsumer> _logger;
    private readonly OrchestratorHub _hub;

    public FilteredDataConsumer(ILogger<FilteredDataConsumer> logger, OrchestratorHub hub)
    {
        _logger = logger;
        _hub = hub;
    }

    /// <summary>
    /// Обработать сообщение.
    /// </summary>
    public async Task Consume(ConsumeContext<FilteredData> context)
    {
        _logger.LogInformation($"Сообщение доставлено! Тело сообщения: {context.Message.Body}.");

        await _hub.Send(context.Message.Script, context.Message.Body);
    }
}
