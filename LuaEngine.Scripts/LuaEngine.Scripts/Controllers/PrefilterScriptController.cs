using AutoMapper;
using LuaEngine.Scripts.Models.PrefilterScript;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Monq.Core.Paging.Models;
using static LuaEngine.Scripts.WebApi.Constants.AppConstants;

namespace LuaEngine.Scripts.WebApi.Controllers;

/// <summary>
/// Контроллер скриптов-префильтров.
/// </summary>
[Controller]
[Route($"{RoutePrefix}/[controller]")]
public class PrefilterScriptController : Controller
{
    private readonly IPrefilterScriptService _prefilterScriptService;
    private readonly IMapper _mapper;

    public PrefilterScriptController(IPrefilterScriptService prefilterScriptService,
                            IMapper mapper)
    {
        _prefilterScriptService = prefilterScriptService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить коллекцию скриптов-префильтров.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-префильтров.</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<PrefilterScriptViewModel>>> GetAllAsync(
        [FromQuery] PagingModel pagingModel,
        [FromBody] PrefilterScriptFilterViewModel filterViewModel,
        CancellationToken token)
    {
        var filter = _mapper.Map<PrefilterScriptFilter>(filterViewModel);

        var scripts = await _prefilterScriptService.GetAllAsync(pagingModel, filter, token);

        var scriptViewModels = _mapper.Map<IEnumerable<PrefilterScriptViewModel>>(scripts);

        return Ok(scriptViewModels);
    }

    /// <summary>
    /// Получить скрипт-префильтр по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-префильтр.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PrefilterScriptViewModel?>> GetAsync(
        [FromRoute] Guid id,
        CancellationToken token)
    {
        var script = await _prefilterScriptService.GetAsync(id, token);

        var scriptViewModel = _mapper.Map<PrefilterScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Добавить скрипт-префильтр.
    /// </summary>
    /// <param name="scriptPostViewModel">Скрипт-префильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-префильтр.</returns>
    [HttpPost()]
    public async Task<ActionResult<PrefilterScriptViewModel?>> AddAsync([FromBody] PrefilterScriptPostViewModel scriptPostViewModel, CancellationToken token)
    {
        var script = _mapper.Map<PrefilterScript>(scriptPostViewModel);

        script = await _prefilterScriptService.AddAsync(script, token);

        var scriptViewModel = _mapper.Map<PrefilterScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Изменить скрипт-префильтр по идентификатору.
    /// </summary>
    /// <param name="scriptPutViewModel">Скрипт-префильтр.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <exception cref="NotFoundResult">Объект не найден.</exception>
    /// <returns>Скрипт-префильтр.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<PrefilterScriptViewModel?>> UpdateAsync([FromRoute] Guid id, [FromBody] PrefilterScriptPutViewModel scriptPutViewModel, CancellationToken token)
    {
        var script = _mapper.Map<PrefilterScript>(scriptPutViewModel);

        script = await _prefilterScriptService.UpdateAsync(id, script, token);

        if (script is null)
            return NotFound();

        var scriptViewModel = _mapper.Map<PrefilterScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Удалить скрипт-префильтр по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] Guid id, CancellationToken token)
    {
        var result = await _prefilterScriptService.DeleteAsync(id, token);

        if (result == false)
            return NotFound(result);

        return Ok(result);
    }
}
