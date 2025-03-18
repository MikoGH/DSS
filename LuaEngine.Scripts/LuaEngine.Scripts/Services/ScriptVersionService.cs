using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models.IncludeOptions;

namespace LuaEngine.Scripts.WebApi.Services;

/// <summary>
/// Сервис версий скриптов.
/// </summary>
public class ScriptVersionService : IScriptVersionService
{
    private readonly IScriptVersionRepository _scriptVersionRepository;

    public ScriptVersionService(IScriptVersionRepository scriptRepository)
    {
        _scriptVersionRepository = scriptRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ScriptVersion>> GetAllAsync(PagingModel pagingModel, ScriptVersionIncludeOptions includeOptions, ScriptVersionFilter filter, CancellationToken token)
    {
        var scripts = await _scriptVersionRepository.GetAllAsync(pagingModel, includeOptions, filter, token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersion?> GetAsync(Guid id, ScriptVersionIncludeOptions includeOptions, CancellationToken token)
    {
        var script = await _scriptVersionRepository.GetAsync(id, includeOptions, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersion?> AddAsync(ScriptVersion script, CancellationToken token)
    {
        var resultScript = await _scriptVersionRepository.AddAsync(script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersion?> UpdateAsync(Guid id, ScriptVersion script, CancellationToken token)
    {
        var resultScript = await _scriptVersionRepository.UpdateAsync(id, script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var result = await _scriptVersionRepository.DeleteAsync(id, token);

        return result;
    }
}
