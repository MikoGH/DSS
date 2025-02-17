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

    public DataConsumer(ILogger<DataConsumer> logger, IRuleScriptRepository ruleScriptRepository, IProcessScriptRepository processScriptRepository)
    {
        _logger = logger;
        _ruleScriptRepository = ruleScriptRepository;
        _processScriptRepository = processScriptRepository;
    }

    public async Task Consume(ConsumeContext<RawData> context)
    {
        // TODO: извлечь источник

        var rules = await _ruleScriptRepository.GetAllAsync(new PagingModel(), new RuleScriptFilterViewModel(), CancellationToken.None);

        var processScripts = await _processScriptRepository.GetAllAsync(new PagingModel(), new ProcessScriptFilterViewModel(), CancellationToken.None);

        // TODO: отфильтровать по префильтру соответствующего источника

        // TODO: отфильтровать по правилам соответствующего источника

        // TODO: отправить в очередь
    }
}
