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
/// Репозиторий скриптов.
/// </summary>
public class ScriptRepository : IScriptRepository
{
    private readonly ScriptsContext _context;
    private readonly IMapper _mapper;

    public ScriptRepository(ScriptsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Script>> GetAllAsync(PagingModel pagingModel, ScriptFilter filter, CancellationToken token)
    {
        var scripts = await _context.Scripts
            .AsNoTracking()
            .FilterBy(filter)
            .WithPaging(pagingModel, null, x => x.Name)
            .ToListAsync(token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<Script?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _context.Scripts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<Script?> AddAsync(Script script, CancellationToken token)
    {
        await _context.Scripts.AddAsync(script, token);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<Script?> UpdateAsync(Guid id, Script updatedScript, CancellationToken token)
    {
        var script = await _context.Scripts.FirstOrDefaultAsync(x => x.Id == id);

        if (script is null)
            return null;

        _mapper.Map(updatedScript, script);
        script.Id = id;

        _context.Scripts.Update(script);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var script = await _context.Scripts.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return false;

        _context.Scripts.Remove(script);
        await _context.SaveChangesAsync(token);

        return true;
    }
}
