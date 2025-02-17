using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using LuaEngine.Scripts.WebApi.Services.Abstracts;

namespace LuaEngine.Scripts.WebApi.Services;

/// <summary>
/// Сервис скриптов-правил.
/// </summary>
public class RuleScriptService : IRuleScriptService
{
    private readonly IRuleScriptRepository _ruleScriptRepository;
    private readonly IScriptValidator _scriptValidator;

    public RuleScriptService(IRuleScriptRepository scriptRepository, IScriptValidator scriptValidator)
    {
        _ruleScriptRepository = scriptRepository;
        _scriptValidator = scriptValidator;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RuleScript>> GetAllAsync(PagingModel pagingModel, RuleScriptFilter filter, CancellationToken token)
    {
        var scripts = await _ruleScriptRepository.GetAllAsync(pagingModel, filter, token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<RuleScript?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _ruleScriptRepository.GetAsync(id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<RuleScript?> AddAsync(RuleScript script, CancellationToken token)
    {
        var resultScript = await _ruleScriptRepository.AddAsync(script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<RuleScript?> UpdateAsync(Guid id, RuleScript script, CancellationToken token)
    {
        var resultScript = await _ruleScriptRepository.UpdateAsync(id, script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var result = await _ruleScriptRepository.DeleteAsync(id, token);

        return result;
    }
}
