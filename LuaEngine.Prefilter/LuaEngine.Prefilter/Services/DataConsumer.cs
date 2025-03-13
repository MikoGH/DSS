using LuaEngine.LuaOrchestrator.Models;
using LuaEngine.Prefilter.Models;
using LuaEngine.Prefilter.Repositories.Abstractions;
using LuaEngine.Scripts.Models.Rule;
using LuaEngine.Scripts.Models.Script;
using MassTransit;
using Monq.Core.Paging.Models;

namespace LuaEngine.Prefilter.Services;

/// <summary>
/// Потребитель данных из очереди RabbitMq.
/// </summary>
public class DataConsumer : IConsumer<RawData>
{
    private readonly ILogger<DataConsumer> _logger;
    private readonly IRuleScriptRepository _ruleScriptRepository;
    private readonly IProcessScriptRepository _processScriptRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public DataConsumer(ILogger<DataConsumer> logger,
        IRuleScriptRepository ruleScriptRepository,
        IProcessScriptRepository processScriptRepository,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _ruleScriptRepository = ruleScriptRepository;
        _processScriptRepository = processScriptRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<RawData> context)
    {
        // TODO: извлечь источник

        _logger.LogInformation($"Сообщение доставлено! Тело сообщения: {context.Message.Body}.");

        var rules = await _ruleScriptRepository.GetAllAsync(new PagingModel(), new RuleScriptFilterViewModel(), CancellationToken.None);

        var processScripts = await _processScriptRepository.GetAllAsync(new PagingModel(), new ProcessScriptFilterViewModel(), CancellationToken.None);

        _logger.LogInformation($"Скриптов-правил получено: {rules.Count()}. Скриптов-обработчиков получено: {processScripts.Count()}.");

        // TODO: отфильтровать по префильтру соответствующего источника

        // TODO: отфильтровать по правилам соответствующего источника

        // TODO: сформировать filteredData с использованием полученного скрипта-обработчика
        var filteredData = new FilteredData()
        {
            Script = "test",
            Body = context.Message.Body
        };

        await _publishEndpoint.Publish<FilteredData>(filteredData);

        _logger.LogInformation($"Сообщение отправлено в очередь! Тело сообщения: {filteredData.Body}.");
    }
}
