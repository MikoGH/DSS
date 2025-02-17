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
/// Репозиторий скриптов-правил.
/// </summary>
public class RuleScriptRepository : IRuleScriptRepository
{
    private readonly ScriptsContext _context;
    private readonly IMapper _mapper;

    public RuleScriptRepository(ScriptsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RuleScript>> GetAllAsync(PagingModel pagingModel, RuleScriptFilter filter, CancellationToken token)
    {
        var scripts = await _context.RuleScripts
            .AsNoTracking()
            .FilterBy(filter)
            .WithPaging(pagingModel, null, x => x.ProcessScriptId)
            .ToListAsync(token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<RuleScript?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _context.RuleScripts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<RuleScript?> AddAsync(RuleScript script, CancellationToken token)
    {
        await _context.RuleScripts.AddAsync(script, token);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<RuleScript?> UpdateAsync(Guid id, RuleScript updatedScript, CancellationToken token)
    {
        var script = await _context.RuleScripts.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return null;

        _mapper.Map(updatedScript, script);
        script.Id = id;

        _context.RuleScripts.Update(script);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var script = await _context.RuleScripts.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return false;

        _context.RuleScripts.Remove(script);
        await _context.SaveChangesAsync(token);

        return true;
    }
}
