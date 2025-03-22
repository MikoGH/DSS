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
/// Репозиторий скриптов-префильтров.
/// </summary>
public class PrefilterScriptRepository : IPrefilterScriptRepository
{
    private readonly ScriptsContext _context;
    private readonly IMapper _mapper;

    public PrefilterScriptRepository(ScriptsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PrefilterScript>> GetAllAsync(PagingModel pagingModel, PrefilterScriptFilter filter, CancellationToken token)
    {
        var scripts = await _context.PrefilterScripts
            .AsNoTracking()
            .FilterBy(filter)
            .WithPaging(pagingModel, null, x => x.Id)
            .ToListAsync(token);

        return scripts;
    }

    /// <inheritdoc/>
    public async Task<PrefilterScript?> GetAsync(Guid id, CancellationToken token)
    {
        var script = await _context.PrefilterScripts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<PrefilterScript?> AddAsync(PrefilterScript script, CancellationToken token)
    {
        await _context.PrefilterScripts.AddAsync(script, token);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<PrefilterScript?> UpdateAsync(Guid id, PrefilterScript updatedScript, CancellationToken token)
    {
        var script = await _context.PrefilterScripts.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return null;

        _mapper.Map(updatedScript, script);
        script.Id = id;

        _context.PrefilterScripts.Update(script);
        await _context.SaveChangesAsync(token);

        return script;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var script = await _context.PrefilterScripts.FirstOrDefaultAsync(x => x.Id == id, token);

        if (script is null)
            return false;

        _context.PrefilterScripts.Remove(script);
        await _context.SaveChangesAsync(token);

        return true;
    }
}
