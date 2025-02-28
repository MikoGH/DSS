using LuaEngine.Prefilter.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LuaEngine.Prefilter.Controllers;

// TODO: Временный контроллер для проверки работоспособности очереди. Потом удалить.
[Controller]
[Route("[controller]")]
public class DataController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;

    public DataController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Отправить строку в очередь.
    /// </summary>
    [HttpPost()]
    public async Task SendStringAsync([FromBody] RawData data, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish<RawData>(data);
    }
}
