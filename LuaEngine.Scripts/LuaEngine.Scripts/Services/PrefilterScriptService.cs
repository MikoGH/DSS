using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using Monq.Core.Paging.Models;

namespace LuaEngine.Scripts.WebApi.Services;

/// <summary>
/// Сервис скриптов-префильтров.
/// </summary>
public class PrefilterScriptService : IPrefilterScriptService
{
    private readonly IPrefilterScriptRepository _prefilterScriptRepository;
    private readonly IScriptValidator _scriptValidator;

    public PrefilterScriptService(IPrefilterScriptRepository scriptRepository, IScriptValidator scriptValidator)
    {
        _prefilterScriptRepository = scriptRepository;
        _scriptValidator = scriptValidator;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PrefilterScript>> GetAllAsync(PagingModel pagingModel, PrefilterScriptFilter filter, CancellationToken token)
    {
        var scripts = await _prefilterScriptRepository.GetAllAsync(pagingModel, filter, token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<PrefilterScript?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _prefilterScriptRepository.GetAsync(id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<PrefilterScript?> AddAsync(PrefilterScript script, CancellationToken token)
    {
        var resultScript = await _prefilterScriptRepository.AddAsync(script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<PrefilterScript?> UpdateAsync(Guid id, PrefilterScript script, CancellationToken token)
    {
        var resultScript = await _prefilterScriptRepository.UpdateAsync(id, script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var result = await _prefilterScriptRepository.DeleteAsync(id, token);

        return result;
    }
}
