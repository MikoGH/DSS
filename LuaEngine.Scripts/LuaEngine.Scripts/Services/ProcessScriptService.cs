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
    private readonly IProcessScriptRepository _processScriptRepository;
    private readonly IScriptValidator _scriptValidator;

    public ProcessScriptService(IProcessScriptRepository scriptRepository, IScriptValidator scriptValidator)
    {
        _processScriptRepository = scriptRepository;
        _scriptValidator = scriptValidator;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProcessScript>> GetAllAsync(PagingModel pagingModel, ProcessScriptFilter filter, CancellationToken token)
    {
        var scripts = await _processScriptRepository.GetAllAsync(pagingModel, filter, token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _processScriptRepository.GetAsync(id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> AddAsync(ProcessScript script, CancellationToken token)
    {
        var resultScript = await _processScriptRepository.AddAsync(script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> UpdateAsync(Guid id, ProcessScript script, CancellationToken token)
    {
        var resultScript = await _processScriptRepository.UpdateAsync(id, script, token);

        return resultScript;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var result = await _processScriptRepository.DeleteAsync(id, token);

        return result;
    }
}
