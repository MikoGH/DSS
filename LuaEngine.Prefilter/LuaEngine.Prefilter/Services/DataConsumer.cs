using LuaEngine.Automaton.Models;
using LuaEngine.Automaton.Runner;
using LuaEngine.LuaOrchestrator.Models;
using LuaEngine.Prefilter.Models;
using LuaEngine.Prefilter.Repositories.Abstractions;
using LuaEngine.Scripts.Models.ScriptVersion;
using MassTransit;
using Monq.Core.Paging.Models;
using System.Text.Json;
using LuaEngine.Scripts.Models.Enums;
using LuaEngine.Automaton.Models.Enums;
using LuaEngine.Scripts.Models.RuleScript;

namespace LuaEngine.Prefilter.Services;

/// <summary>
/// Потребитель данных из очереди RabbitMq.
/// </summary>
public class DataConsumer : IConsumer<RawData>
{
    private readonly ILogger<DataConsumer> _logger;
    private readonly IScriptVersionRepository _scriptVersionRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly AutomatonRunnerContext _automatonRunnerContext;

    public DataConsumer(ILogger<DataConsumer> logger,
        IScriptVersionRepository scriptVersionRepository,
        IPublishEndpoint publishEndpoint,
        AutomatonRunnerContext automatonRunnerContext)
    {
        _logger = logger;
        _scriptVersionRepository = scriptVersionRepository;
        _publishEndpoint = publishEndpoint;
        _automatonRunnerContext = automatonRunnerContext;
    }

    // TODO: подумать, на каком уровне какие ошибки обрабатывать и логировать, + вынести всю обработку в отдельный сервис
    public async Task Consume(ConsumeContext<RawData> context)
    {
        _logger.LogInformation($"Сообщение доставлено! Тело сообщения: {context.Message.Body}.");

        // TODO: обернуть в проверку что десериализация возможна, разобраться как десериализовать в object
        //var variables = JsonSerializer.Deserialize<Dictionary<string, int>>(context.Message.Body).ToDictionary(x => x.Key, x => (object?)x.Value);
        JsonElement deserializedBody = JsonSerializer.Deserialize<JsonElement>(context.Message.Body);
        var variables = (Dictionary<string, object?>)ConvertJsonElementValue(deserializedBody);

        // получение активного префильтра для источника
        var prefilterScriptVersion = await GetPrefilterScriptVersion(context.Message.SourceId);

        // получение активных правил для источника
        var ruleScriptVersions = await GetRuleScriptVersions(context.Message.SourceId);

        _logger.LogInformation($"Версий скриптов-правил получено: {ruleScriptVersions.Count()}.");

        var orderedRuleScriptVersions = ruleScriptVersions
            .OrderBy(x => x.RuleScript.Priority)
            .ToList();

        // префильтр
        var prefilterResult = await ExecutePrefilter(prefilterScriptVersion, context.Message.SourceId, variables);
        if (!prefilterResult)
            return;

        // правила
        var ruleScriptVersion = await ExecuteRuleScripts(orderedRuleScriptVersions, context.Message.SourceId, variables);
        if (ruleScriptVersion is null)
            return;

        // получение скрипта-обработчика
        var processScript = await GetProcessScriptVersion(ruleScriptVersion.RuleScript);
        if (processScript is null)
            return;

        var filteredData = new FilteredData()
        {
            Script = processScript.Code,
            Body = context.Message.Body
        };

        await _publishEndpoint.Publish<FilteredData>(filteredData);

        _logger.LogInformation($"Сообщение отправлено в очередь! Тело сообщения: {filteredData.Body}.");
    }

    private async Task<ScriptVersionViewModel?> GetPrefilterScriptVersion(Guid sourceId)
    {
        var scriptVersionFilter = new ScriptVersionFilterViewModel()
        {
            Types = [ScriptType.Prefilter],
            SourceIds = [sourceId],
            Enabled = [true],
            Statuses = [Status.Executable]
        };
        var scriptVersionIncludeOptions = new ScriptVersionIncludeViewModel()
        {
            IncludePrefilterScript = true
        };

        var scriptVersions = await _scriptVersionRepository.GetAllAsync(new PagingModel(), scriptVersionIncludeOptions, scriptVersionFilter, CancellationToken.None);

        return scriptVersions.FirstOrDefault();
    }

    private Task<IEnumerable<ScriptVersionViewModel>> GetRuleScriptVersions(Guid sourceId)
    {
        var scriptVersionFilter = new ScriptVersionFilterViewModel()
        {
            Types = [ScriptType.Rule],
            SourceIds = [sourceId],
            Enabled = [true],
            Statuses = [Status.Executable]
        };
        var scriptVersionIncludeOptions = new ScriptVersionIncludeViewModel()
        {
            IncludeRuleScript = true
        };

        return _scriptVersionRepository.GetAllAsync(new PagingModel(), scriptVersionIncludeOptions, scriptVersionFilter, CancellationToken.None);
    }

    private async Task<ScriptVersionViewModel?> GetProcessScriptVersion(RuleScriptViewModel ruleScript)
    {
        var scriptVersionFilter = new ScriptVersionFilterViewModel()
        {
            ProcessScriptIds = [(Guid)ruleScript.ProcessScriptId],
            Statuses = [Status.Executable]
        };
        var scriptVersionIncludeOptions = new ScriptVersionIncludeViewModel();

        var processScriptVersions = await _scriptVersionRepository.GetAllAsync(new PagingModel(), scriptVersionIncludeOptions, scriptVersionFilter, CancellationToken.None);

        var processScriptVersion = processScriptVersions.FirstOrDefault();

        if (processScriptVersion is null)
        {
            _logger.LogError($"Скрипта-обработчика с id {ruleScript.ProcessScriptId} не существует или отсутствует исполняемая версия.");
            return null;
        }

        return processScriptVersion;
    }

    private ValueTask<EngineParsedEventResult> ExecuteScript(string script, Dictionary<string, object?> variables)
    {
        //IOptions<AutomatonEngineOptions> engineOptions = new OptionsWrapper<AutomatonEngineOptions>(new AutomatonEngineOptions());
        //var strategy = new DefaultScriptStrategy();
        //var loggerFactory = new AutomatonLoggerFactory(new StubLoggerFactory(), engineOptions);
        //var logger = loggerFactory.CreateAutomatonLogger<AutomatonRunnerContext>();

        // TODO: вероятно, добавить Id версии скрипта в модель, чтобы можно было отследить по логам в оркестраторе
        var ruleEvent = new EngineParsedEvent
        {
            Script = script,
            Variables = variables
        };

        //using var runnerContext = new AutomatonRunnerContext(strategy, logger, engineOptions);
        // TODO: при выполнении 2+ скриптов в одном контексте выдаёт ошибку, надо разобраться, скорее всего что-то лишнее нужно диспоузить
        return _automatonRunnerContext.Run(ruleEvent);
    }

    private async Task<bool> ExecutePrefilter(ScriptVersionViewModel? prefilter, Guid sourceId, Dictionary<string, object?> variables)
    {
        if (prefilter is null)
        {
            _logger.LogWarning($"Префильтра для источника {sourceId} не существует.");
            // TODO: решить, продолжать выполнение при отсутствии префильтра или нет. Пока продолжаем, поэтому true
            return true;
        }

        var engineResult = await ExecuteScript(prefilter.Code, variables);

        if (engineResult.Status != ExecutingResult.Success)
        {
            _logger.LogError($"Префильтр для источника {sourceId} завершился с ошибкой. Статус: {engineResult.Status}. Сообщение: {engineResult.Message}. Id версии скрипта: {prefilter.Id}.");
            return false;
        }

        if (engineResult.Value is null || engineResult.Value.GetType() != typeof(bool))
        {
            _logger.LogWarning($"Префильтр для источника {sourceId} возвращает не bool. Id версии скрипта: {prefilter.Id}.");
            // TODO: решить, продолжать выполнение при префильтре который возвращает не bool. Пока прекращаем выполнение, поэтому false
            return false;
        }

        var prefilterResult = (bool)engineResult.Value;

        _logger.LogInformation($"Префильтр для источника {sourceId} выполнился успешно. Результат выполнения скрипта: {prefilterResult}");

        return prefilterResult;
    }

    private async Task<ScriptVersionViewModel?> ExecuteRuleScripts(IEnumerable<ScriptVersionViewModel> ruleScriptVersions, Guid sourceId, Dictionary<string, object?> variables)
    {
        if (ruleScriptVersions.Count() == 0)
        {
            _logger.LogInformation($"Активных правил для источника {sourceId} не существует.");
            return null;
        }

        foreach (var ruleScriptVersion in ruleScriptVersions)
        {
            var engineResult = await ExecuteScript(ruleScriptVersion.Code, variables);

            if (engineResult.Status != ExecutingResult.Success)
            {
                _logger.LogError($"Правило для источника {sourceId} завершилось с ошибкой. Статус: {engineResult.Status}. Сообщение: {engineResult.Message}. Id версии скрипта: {ruleScriptVersion.Id}.");
                continue;
            }

            if (engineResult.Value is null || engineResult.Value.GetType() != typeof(bool))
            {
                _logger.LogWarning($"Правило для источника {sourceId} возвращает не bool. Id версии скрипта: {ruleScriptVersion.Id}.");
                continue;
            }

            var ruleScriptResult = (bool)engineResult.Value;

            _logger.LogInformation($"Правило для источника {sourceId} выполнилось успешно. Результат выполнения скрипта: {ruleScriptResult}");

            if (ruleScriptResult)
                return ruleScriptVersion;
        }

        _logger.LogInformation($"Не прошло ни одно правило для источника {sourceId}.");

        return null;
    }

    //private static Dictionary<string, object?> ConvertJsonToDictionary(string json)
    //{
    //    var dictionary = JsonSerializer.Deserialize<Dictionary<string, object?>>(json);

    //    foreach (var key in dictionary.Keys)
    //    {
    //        var value = dictionary[key];

    //        // Если значение является LuaTable, рекурсивно конвертируем
    //        if (value is LuaTable nestedTable)
    //        {
    //            dictionary[key.ToString()] = ConvertLuaTableToDictionary(nestedTable);
    //        }
    //        else
    //        {
    //            dictionary[key.ToString()] = value;
    //        }
    //    }
    //}

    private static object? ConvertJsonElementValue(JsonElement jsonElement)
    {
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.Object:
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, object?>>(jsonElement);
                foreach (var key in dictionary.Keys)
                {
                    dictionary[key] = ConvertJsonElementValue((JsonElement)dictionary[key]);
                }
                return dictionary;

            case JsonValueKind.Array:
                var list = new List<object?>();
                foreach (var item in jsonElement.EnumerateArray())
                {
                    list.Add(ConvertJsonElementValue(item));
                }
                return list;

            case JsonValueKind.String:
                return jsonElement.GetString();

            case JsonValueKind.Number:
                if (jsonElement.TryGetDouble(out double doubleValue))
                {
                    return doubleValue;
                }
                break;

            case JsonValueKind.True:
            case JsonValueKind.False:
                return jsonElement.GetBoolean();

            case JsonValueKind.Null:
                return null;

            default:
                return jsonElement.ToString();
        }

        return null;
    }
}
