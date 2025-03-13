using AutoMapper;
using LuaEngine.Scripts.WebApi.Database;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using Monq.Core.Paging.Models;
using Microsoft.EntityFrameworkCore;
using Monq.Core.MvcExtensions.Extensions;
using Monq.Core.Paging.Extensions;

namespace LuaEngine.Scripts.WebApi.Repositories;

/// <summary>
/// Репозиторий версий скриптов.
/// </summary>
public class ScriptVersionRepository : IScriptVersionRepository
{
    private readonly ScriptsContext _context;
    private readonly IMapper _mapper;

    public ScriptVersionRepository(ScriptsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ScriptVersion>> GetAllAsync(PagingModel pagingModel, ScriptVersionFilter filter, CancellationToken token)
    {
        var scripts = await _context.ScriptVersions
            .AsNoTracking()
            .FilterBy(filter)
            .WithPaging(pagingModel, null, x => x.Id)
            .ToListAsync(token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersion?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _context.ScriptVersions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersion?> AddAsync(ScriptVersion script, CancellationToken token)
    {
        await _context.ScriptVersions.AddAsync(script, token);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersion?> UpdateAsync(Guid id, ScriptVersion updatedScript, CancellationToken token)
    {
        var script = await _context.ScriptVersions.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return null;

        _mapper.Map(updatedScript, script);
        script.Id = id;

        _context.ScriptVersions.Update(script);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var script = await _context.ScriptVersions.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return false;

        _context.ScriptVersions.Remove(script);
        await _context.SaveChangesAsync(token);

        return true;
    }
}
