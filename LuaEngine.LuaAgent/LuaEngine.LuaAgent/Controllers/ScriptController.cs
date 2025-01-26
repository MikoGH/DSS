using LuaEngine.LuaAgent.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using static LuaEngine.LuaAgent.Constants.AppConstants;

namespace LuaEngine.LuaAgent.Controllers;

/// <summary>
/// Контроллер обработки скриптов.
/// </summary>
[ApiController]
[Route(RoutePrefix + "/[controller]")]
public class ScriptController : Controller
{
    private readonly IScriptExecutorService _scriptExecutorService;

    public ScriptController(IScriptExecutorService scriptExecutorService)
    {
        _scriptExecutorService = scriptExecutorService;
    }

    /// <summary>
    /// Выполнить скрипт с заданной моделью.
    /// </summary>
    /// <returns>Обработанная модель.</returns>
    [HttpPost]
    public async Task<object> ExecuteScriptAsync()
    {
        //var result = _scriptExecutorService.ExecuteScript(script, model);

        //return Task.FromResult(result);
        throw new NotImplementedException();
    }
}
