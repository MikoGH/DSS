using Microsoft.AspNetCore.SignalR;

namespace LuaEngine.LuaOrchestrator.Hubs;

/// <summary>
/// Хаб для оркестрации агентов.
/// </summary>
public class OrchestratorHub : Hub
{
    private readonly ILogger<OrchestratorHub> _logger;
    // TODO: подумать, как лучше хранить подключения и проверять их занятость. Пока что для проверки так, true - свободный, иначе false.
    private readonly Dictionary<string, bool> _connections = new();

    public OrchestratorHub(ILogger<OrchestratorHub> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Подключить нового клиента.
    /// </summary>
    public async override Task OnConnectedAsync()
    {
        _connections.Add(Context.ConnectionId, true);
        _logger.LogInformation($"Зарегистрирован новый агент с идентификатором подключения {Context.ConnectionId}.");
    }

    /// <summary>
    /// Отправить скрипт с данными на выполнение любому свободному агенту.
    /// </summary>
    /// <param name="script">Скрипт.</param>
    /// <param name="body">Данные.</param>
    public async Task Send(string script, string body)
    {
        var freeConnection = _connections.FirstOrDefault(x => x.Value == true).Key;

        // если нет свободных клиентов
        if (freeConnection is null)
        {
            _logger.LogInformation($"Отсутствуют свободные агенты.");
            return;
        }

        _connections[freeConnection] = true;
        await Clients.Client(freeConnection).SendAsync("Send", script, body);

        _logger.LogInformation($"Скрипт отправлен на выполнение агенту с идентификатором подключения {freeConnection}.");
    }

    /// <summary>
    /// Получить результат выполнения скрипта от агента.
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public Task Receive(string body)
    {
        _logger.LogInformation($"Агент с идентификатором подключения {Context.ConnectionId} прислал результат выполнения скрипта {body}.");
    
        return Task.CompletedTask;
    }
}
