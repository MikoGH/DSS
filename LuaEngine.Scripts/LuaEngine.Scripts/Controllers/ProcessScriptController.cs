using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using static LuaEngine.Scripts.WebApi.Constants.AppConstants;
using LuaEngine.Scripts.Models.ProcessScript;

namespace LuaEngine.Scripts.WebApi.Controllers;

/// <summary>
/// Контроллер скриптов-обработчиков.
/// </summary>
[Controller]
[Route($"{RoutePrefix}/[controller]")]
public class ProcessScriptController : Controller
{
    private readonly IProcessScriptService _scriptService;
    private readonly IMapper _mapper;

    public ProcessScriptController(IProcessScriptService scriptService,
                            IMapper mapper)
    {
        _scriptService = scriptService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить коллекцию скриптов-обработчиков.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-обработчиков.</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<ProcessScriptViewModel>>> GetAllAsync(
        [FromQuery] PagingModel pagingModel,
        [FromBody] ProcessScriptFilterViewModel filterViewModel,
        CancellationToken token)
    {
        var filter = _mapper.Map<ProcessScriptFilter>(filterViewModel);

        var scripts = await _scriptService.GetAllAsync(pagingModel, filter, token);

        var scriptViewModels = _mapper.Map<IEnumerable<ProcessScriptViewModel>>(scripts);

        return Ok(scriptViewModels);
    }

    /// <summary>
    /// Получить скрипт-обработчик по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-обработчик.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcessScriptViewModel?>> GetAsync(
        [FromRoute] Guid id,
        CancellationToken token)
    {
        var script = await _scriptService.GetAsync(id, token);

        var scriptViewModel = _mapper.Map<ProcessScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Добавить скрипт-обработчик.
    /// </summary>
    /// <param name="scriptPostViewModel">Скрипт-обработчик.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-обработчик.</returns>
    [HttpPost()]
    public async Task<ActionResult<ProcessScriptViewModel?>> AddAsync([FromBody] ProcessScriptPostViewModel scriptPostViewModel, CancellationToken token)
    {
        var script = _mapper.Map<ProcessScript>(scriptPostViewModel);

        script = await _scriptService.AddAsync(script, token);

        var scriptViewModel = _mapper.Map<ProcessScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Изменить скрипт-обработчик по идентификатору.
    /// </summary>
    /// <param name="scriptPutViewModel">Скрипт-обработчик.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <exception cref="NotFoundResult">Объект не найден.</exception>
    /// <returns>Скрипт-обработчик.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProcessScriptViewModel?>> UpdateAsync([FromRoute] Guid id, [FromBody] ProcessScriptPutViewModel scriptPutViewModel, CancellationToken token)
    {
        var script = _mapper.Map<ProcessScript>(scriptPutViewModel);

        script = await _scriptService.UpdateAsync(id, script, token);

        if (script is null)
            return NotFound();

        var scriptViewModel = _mapper.Map<ProcessScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Удалить скрипт-обработчик по идентификатору.
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
