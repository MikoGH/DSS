using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Monq.Core.MvcExtensions.Extensions;
using Monq.Core.Paging.Extensions;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Database;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;

namespace LuaEngine.Scripts.WebApi.Repositories;

/// <summary>
/// Репозиторий скриптов-обработчиков.
/// </summary>
public class ProcessScriptRepository : IProcessScriptRepository
{
    private readonly ScriptsContext _context;
    private readonly IMapper _mapper;

    public ProcessScriptRepository(ScriptsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProcessScript>> GetAllAsync(PagingModel pagingModel, ProcessScriptFilter filter, CancellationToken token)
    {
        var scripts = await _context.ProcessScripts
            .AsNoTracking()
            .FilterBy(filter)
            .WithPaging(pagingModel, null, x => x.Name)
            .ToListAsync(token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _context.ProcessScripts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> AddAsync(ProcessScript script, CancellationToken token)
    {
        await _context.ProcessScripts.AddAsync(script, token);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<ProcessScript?> UpdateAsync(Guid id, ProcessScript updatedScript, CancellationToken token)
    {
        var script = await _context.ProcessScripts.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return null;

        _mapper.Map(updatedScript, script);
        script.Id = id;

        _context.ProcessScripts.Update(script);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var script = await _context.ProcessScripts.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return false;

        _context.ProcessScripts.Remove(script);
        await _context.SaveChangesAsync(token);

        return true;
    }
}
