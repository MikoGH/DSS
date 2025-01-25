using LuaEngine.LuaOrchestrator.Models;
using MassTransit;

namespace LuaEngine.LuaOrchestrator.Services;

/// <summary>
/// Обработчик сообщений <see cref="RawData"/>.
/// </summary>
public class RawDataConsumer : IConsumer<RawData>
{
    /// <summary>
    /// Обработать сообщение.
    /// </summary>
    public Task Consume(ConsumeContext<RawData> context)
    {
        // TODO
        Console.WriteLine(context.Message);
        return Task.CompletedTask;
    }
}
