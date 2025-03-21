using LuaEngine.Automaton.Models;
using LuaEngine.Automaton.Runner;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;

namespace LuaEngine.LuaAgent.Services;

public class HubConnectionService : BackgroundService
{
    private readonly HubConnection _connection;
    private readonly ILogger<HubConnectionService> _logger;
    private readonly AutomatonRunnerContext _automatonRunnerContext;

    public HubConnectionService(ILogger<HubConnectionService> logger, AutomatonRunnerContext automatonRunnerContext)
    {
        _logger = logger;
        _automatonRunnerContext = automatonRunnerContext;

        // TODO: вынести адрес в appsettings
        _connection = new HubConnectionBuilder()
            .WithUrl("http://orchestrator_api:8084/hub")
            .Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"Подключение к хабу...");

        _connection.On("Send", (string script, string body) =>
        {
            _logger.LogInformation($"Получены данные из хаба: {body}.");

            JsonElement deserializedBody = JsonSerializer.Deserialize<JsonElement>(body);
            var variables = (Dictionary<string, object?>)ConvertJsonElementValue(deserializedBody);

            var engineResult = ExecuteScript(script, variables).Result;

            var resultBody = JsonSerializer.Serialize(engineResult.Value);

            _logger.LogInformation($"Скрипт завершил выполнение. Статус: {engineResult.Status}. Сообщение: {engineResult.Message}. Результат: {resultBody}.");

            _connection.InvokeAsync("Receive", resultBody);
        });

        _connection.StartAsync().ConfigureAwait(false);

        _logger.LogInformation($"Подключение к хабу завершено.");

        return Task.CompletedTask;
    }

    private ValueTask<EngineParsedEventResult> ExecuteScript(string script, Dictionary<string, object?> variables)
    {
        var ruleEvent = new EngineParsedEvent
        {
            Script = script,
            Variables = variables
        };

        // TODO: при выполнении 2+ скриптов в одном контексте выдаёт ошибку, надо разобраться, скорее всего что-то лишнее нужно диспоузить
        return _automatonRunnerContext.Run(ruleEvent);
    }

    // TODO: конвертацию перенести в оркестратор, иначе много раз будет одно и то же выполняться для каждого скрипта
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
