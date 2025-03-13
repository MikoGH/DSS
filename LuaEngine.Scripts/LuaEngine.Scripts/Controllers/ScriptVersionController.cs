using AutoMapper;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.Models.ScriptVersion;
using static LuaEngine.Scripts.WebApi.Constants.AppConstants;

namespace LuaEngine.Scripts.WebApi.Controllers;

/// <summary>
/// Контроллер версий скриптов.
/// </summary>
[Controller]
[Route($"{RoutePrefix}/[controller]")]
public class ScriptVersionController : Controller
{
    private readonly IScriptVersionService _ruleScriptService;
    private readonly IMapper _mapper;

    public ScriptVersionController(IScriptVersionService ruleScriptService,
                            IMapper mapper)
    {
        _ruleScriptService = ruleScriptService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить коллекцию версий скриптов.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция версий скриптов.</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<ScriptVersionViewModel>>> GetAllAsync(
        [FromQuery] PagingModel pagingModel,
        [FromBody] ScriptVersionFilterViewModel filterViewModel,
        CancellationToken token)
    {
        var filter = _mapper.Map<ScriptVersionFilter>(filterViewModel);

        var scripts = await _ruleScriptService.GetAllAsync(pagingModel, filter, token);

        var scriptViewModels = _mapper.Map<IEnumerable<ScriptVersionViewModel>>(scripts);

        return Ok(scriptViewModels);
    }

    /// <summary>
    /// Получить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ScriptVersionViewModel?>> GetAsync([FromRoute] Guid id, CancellationToken token)
    {
        var script = await _ruleScriptService.GetAsync(id, token);

        var scriptViewModel = _mapper.Map<ScriptVersionViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Добавить версию скрипта.
    /// </summary>
    /// <param name="scriptPostViewModel">Скрипт-правило.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    [HttpPost()]
    public async Task<ActionResult<ScriptVersionViewModel?>> AddAsync([FromBody] ScriptVersionPostViewModel scriptPostViewModel, CancellationToken token)
    {
        var script = _mapper.Map<ScriptVersion>(scriptPostViewModel);

        script = await _ruleScriptService.AddAsync(script, token);

        var scriptViewModel = _mapper.Map<ScriptVersionViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Изменить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="scriptPutViewModel">Скрипт-правило.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <exception cref="NotFoundResult">Объект не найден.</exception>
    /// <returns>Скрипт-правило.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ScriptVersionViewModel?>> UpdateAsync([FromRoute] Guid id, [FromBody] ScriptVersionPutViewModel scriptPutViewModel, CancellationToken token)
    {
        var script = _mapper.Map<ScriptVersion>(scriptPutViewModel);

        script = await _ruleScriptService.UpdateAsync(id, script, token);

        if (script is null)
            return NotFound();

        var scriptViewModel = _mapper.Map<ScriptVersionViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Удалить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] Guid id, CancellationToken token)
    {
        var result = await _ruleScriptService.DeleteAsync(id, token);

        if (result == false)
            return NotFound(result);

        return Ok(result);
    }
}
