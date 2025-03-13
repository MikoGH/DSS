using LuaEngine.LuaOrchestrator.Models;
using MassTransit;

namespace LuaEngine.LuaOrchestrator.Services;

/// <summary>
/// Обработчик сообщений <see cref="FilteredData"/>.
/// </summary>
public class FilteredDataConsumer : IConsumer<FilteredData>
{
    private readonly ILogger<FilteredDataConsumer> _logger;

    public FilteredDataConsumer(ILogger<FilteredDataConsumer> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Обработать сообщение.
    /// </summary>
    public Task Consume(ConsumeContext<FilteredData> context)
    {
        _logger.LogInformation($"Сообщение доставлено! Тело сообщения: {context.Message.Body}.");

        return Task.CompletedTask;
    }
}
