using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using LuaEngine.Scripts.WebApi.Services.Abstracts;

namespace LuaEngine.Scripts.WebApi.Services;

/// <summary>
/// Сервис скриптов-обработчиков.
/// </summary>
public class ProcessScriptService : IProcessScriptService
{
    private readonly IProcessScriptRepository _scriptRepository;
    private readonly IScriptValidator _scriptValidator;

    public ProcessScriptService(IProcessScriptRepository scriptRepository, IScriptValidator scriptValidator)
    {
        _scriptRepository = scriptRepository;
        _scriptValidator = scriptValidator;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProcessScript>> GetAllAsync(PagingModel pagingModel, ProcessScriptFilter filter, CancellationToken token)
    {
        var scripts = await _scriptRepository.GetAllAsync(pagingModel, filter, token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _scriptRepository.GetAsync(id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> AddAsync(ProcessScript script, CancellationToken token)
    {
        var resultScript = await _scriptRepository.AddAsync(script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> UpdateAsync(Guid id, ProcessScript script, CancellationToken token)
    {
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
