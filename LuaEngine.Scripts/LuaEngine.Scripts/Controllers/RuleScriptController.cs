using AutoMapper;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.Models.RuleScript;
using static LuaEngine.Scripts.WebApi.Constants.AppConstants;

namespace LuaEngine.Scripts.WebApi.Controllers;

/// <summary>
/// Контроллер скриптов-правил.
/// </summary>
[Controller]
[Route($"{RoutePrefix}/[controller]")]
public class RuleScriptController : Controller
{
    private readonly IRuleScriptService _ruleScriptService;
    private readonly IMapper _mapper;
    private readonly ILogger<RuleScriptController> _logger;

    public RuleScriptController(IRuleScriptService ruleScriptService,
                            IMapper mapper,
                            ILogger<RuleScriptController> logger)
    {
        _ruleScriptService = ruleScriptService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Получить коллекцию скриптов-правил.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-правил.</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<RuleScriptViewModel>>> GetAllAsync(
        [FromQuery] PagingModel pagingModel,
        [FromBody] RuleScriptFilterViewModel filterViewModel,
        CancellationToken token)
    {
        var filter = _mapper.Map<RuleScriptFilter>(filterViewModel);

        var scripts = await _ruleScriptService.GetAllAsync(pagingModel, filter, token);

        var scriptViewModels = _mapper.Map<IEnumerable<RuleScriptViewModel>>(scripts);

        return Ok(scriptViewModels);
    }

    /// <summary>
    /// Получить скрипт-правило по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RuleScriptViewModel?>> GetAsync(
        [FromRoute] Guid id,
        CancellationToken token)
    {
        var script = await _ruleScriptService.GetAsync(id, token);

        var scriptViewModel = _mapper.Map<RuleScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Добавить скрипт-правило.
    /// </summary>
    /// <param name="scriptPostViewModel">Скрипт-правило.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    [HttpPost()]
    public async Task<ActionResult<RuleScriptViewModel?>> AddAsync([FromBody] RuleScriptPostViewModel scriptPostViewModel, CancellationToken token)
    {
        var script = _mapper.Map<RuleScript>(scriptPostViewModel);

        script = await _ruleScriptService.AddAsync(script, token);

        var scriptViewModel = _mapper.Map<RuleScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Изменить скрипт-правило по идентификатору.
    /// </summary>
    /// <param name="scriptPutViewModel">Скрипт-правило.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <exception cref="NotFoundResult">Объект не найден.</exception>
    /// <returns>Скрипт-правило.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RuleScriptViewModel?>> UpdateAsync([FromRoute] Guid id, [FromBody] RuleScriptPutViewModel scriptPutViewModel, CancellationToken token)
    {
        var script = _mapper.Map<RuleScript>(scriptPutViewModel);

        script = await _ruleScriptService.UpdateAsync(id, script, token);

        if (script is null)
        {
            _logger.LogError("Тестовое сообщение об ошибке.");
            return NotFound();
        }

        var scriptViewModel = _mapper.Map<RuleScriptViewModel>(script);

        return Ok(scriptViewModel);
    }

    /// <summary>
    /// Удалить скрипт-правило по идентификатору.
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
