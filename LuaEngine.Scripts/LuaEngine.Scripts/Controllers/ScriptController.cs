using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using LuaEngine.Scripts.Models.Script;
using static LuaEngine.Scripts.WebApi.Constants.AppConstants;

namespace LuaEngine.Scripts.WebApi.Controllers;

/// <summary>
/// Контроллер Lua-скриптов.
/// </summary>
[Controller]
[Route($"{RoutePrefix}/[controller]")]
public class ScriptController : Controller
{
    private readonly IScriptService _scriptService;
    private readonly IMapper _mapper;

    public ScriptController(IScriptService scriptService,
                            IMapper mapper)
    {
        _scriptService = scriptService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить коллекцию Lua-скриптов.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция Lua-скриптов.</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<ScriptViewModel>>> GetAllAsync(
        [FromQuery] PagingModel pagingModel,
        [FromBody] ScriptFilterViewModel filterViewModel,
        CancellationToken token)
    {
        var filter = _mapper.Map<ScriptFilter>(filterViewModel);

        var scripts = await _scriptService.GetAllAsync(pagingModel, filter, token);

        var scriptViewModels = _mapper.Map<IEnumerable<ScriptViewModel>>(scripts);

        return Ok(scriptViewModels);
    }

    /// <summary>
    /// Получить Lua-скрипт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Lua-скрипт.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ScriptViewModel?>> GetAsync([FromRoute] Guid id, CancellationToken token)
    {
        var script = await _scriptService.GetAsync(id, token);

        var scriptViewModel = _mapper.Map<ScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Добавить Lua-скрипт.
    /// </summary>
    /// <param name="scriptPostViewModel">Lua-скрипт.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Lua-скрипт.</returns>
    [HttpPost()]
    public async Task<ActionResult<ScriptViewModel?>> AddAsync([FromBody] ScriptPostViewModel scriptPostViewModel, CancellationToken token)
    {
        var script = _mapper.Map<Script>(scriptPostViewModel);

        script = await _scriptService.AddAsync(script, token);

        var scriptViewModel = _mapper.Map<ScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Изменить Lua-скрипт по идентификатору.
    /// </summary>
    /// <param name="scriptPutViewModel">Lua-скрипт.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <exception cref="NotFoundResult">Объект не найден.</exception>
    /// <returns>Lua-скрипт.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ScriptViewModel?>> UpdateAsync([FromRoute] Guid id, [FromBody] ScriptPutViewModel scriptPutViewModel, CancellationToken token)
    {
        var script = _mapper.Map<Script>(scriptPutViewModel);

        script = await _scriptService.UpdateAsync(id, script, token);

        if (script is null)
            return NotFound();

        var scriptViewModel = _mapper.Map<ScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Удалить Lua-скрипт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] Guid id, CancellationToken token)
    {
        var result = await _scriptService.DeleteAsync(id, token);

        if (result == false)
            return NotFound(result);

        return Ok(result);
    }
}
