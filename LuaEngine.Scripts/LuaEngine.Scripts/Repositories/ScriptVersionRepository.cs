using AutoMapper;
using LuaEngine.Scripts.WebApi.Database;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using Monq.Core.Paging.Models;
using Microsoft.EntityFrameworkCore;
using Monq.Core.MvcExtensions.Extensions;
using Monq.Core.Paging.Extensions;
using LuaEngine.Scripts.WebApi.Models.IncludeOptions;

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

    private IQueryable<ScriptVersion> GetQuery(ScriptVersionIncludeOptions includeOptions)
    {
        var query = _context.ScriptVersions.AsNoTracking();

        if (includeOptions.IncludePrefilterScript)
            query = query.Include(x => x.PrefilterScript);

        if (includeOptions.IncludeRuleScript)
            query = query.Include(x => x.RuleScript);

        if (includeOptions.IncludeProcessScript)
            query = query.Include(x => x.ProcessScript);

        if (includeOptions.IncludeParentScriptVersion)
            query = query.Include(x => x.Parent);

        return query;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ScriptVersion>> GetAllAsync(PagingModel pagingModel, ScriptVersionIncludeOptions includeOptions, ScriptVersionFilter filter, CancellationToken token)
    {
        var scripts = await GetQuery(includeOptions)
            .FilterBy(filter)
            .WithPaging(pagingModel, null, x => x.Id)
            .ToListAsync(token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Guid>> GetAllIdsAsync(PagingModel pagingModel, ScriptVersionIncludeOptions includeOptions, ScriptVersionFilter filter, CancellationToken token)
    {
        var scriptIds = await GetQuery(includeOptions)
            .FilterBy(filter)
            .WithPaging(pagingModel, null, x => x.Id)
            .Select(x => x.Id)
            .ToListAsync(token);

        return scriptIds;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersion?> GetAsync(Guid id, ScriptVersionIncludeOptions includeOptions, CancellationToken token)
    {
        var script = await GetQuery(includeOptions)
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
