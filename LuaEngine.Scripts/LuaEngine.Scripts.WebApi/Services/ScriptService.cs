using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using LuaEngine.Scripts.WebApi.Services.Abstracts;

namespace LuaEngine.Scripts.WebApi.Services;

/// <summary>
/// Сервис скриптов.
/// </summary>
public class ScriptService : IScriptService
{
    private readonly IScriptRepository _scriptRepository;
    private readonly IScriptValidator _scriptValidator;

    public ScriptService(IScriptRepository scriptRepository, IScriptValidator scriptValidator)
    {
        _scriptRepository = scriptRepository;
        _scriptValidator = scriptValidator;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Script>> GetAllAsync(PagingModel pagingModel, ScriptFilter filter, CancellationToken token)
    {
        var scripts = await _scriptRepository.GetAllAsync(pagingModel, filter, token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<Script?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _scriptRepository.GetAsync(id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<Script?> AddAsync(Script script, CancellationToken token)
    {
        if (!_scriptValidator.ValidateScript(script))
            return null;

        var resultScript = await _scriptRepository.AddAsync(script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<Script?> UpdateAsync(Guid id, Script script, CancellationToken token)
    {
        if (!_scriptValidator.ValidateScript(script))
            return null;

        var resultScript = await _scriptRepository.UpdateAsync(id, script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var result = await _scriptRepository.DeleteAsync(id, token);

        return result;
    }
}
