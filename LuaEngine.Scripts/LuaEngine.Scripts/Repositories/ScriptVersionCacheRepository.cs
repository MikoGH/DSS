using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models.IncludeOptions;
using LuaEngine.Scripts.WebApi.Repositories.Abstracts;
using LuaEngine.Scripts.WebApi.Services.Caching.Abstracts;
using Monq.Core.Paging.Models;
using System.Collections.Specialized;

namespace LuaEngine.Scripts.WebApi.Repositories;

/// <summary>
/// Репозиторий кеширования версий скриптов.
/// </summary>
public class ScriptVersionCacheRepository : IScriptVersionRepository
{
    private readonly ScriptVersionRepository _scriptVersionRepository;
    private readonly ICacheService _cacheService;

    private readonly bool[][] _paramCombinations;

    public ScriptVersionCacheRepository(ScriptVersionRepository scriptVersionRepository, ICacheService cacheService)
    {
        _scriptVersionRepository = scriptVersionRepository;
        _cacheService = cacheService;

        // все комбинации include-параметров в кэше
        var paramCount = 4;
        _paramCombinations = GetParamCombinations(paramCount);
    }

    public Task<ScriptVersion?> AddAsync(ScriptVersion script, CancellationToken token)
    {
        return _scriptVersionRepository.AddAsync(script, token);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        await RemoveCachedAsync([id], token);
        var result = await _scriptVersionRepository.DeleteAsync(id, token);

        return result;
    }

    public async Task<IEnumerable<ScriptVersion>> GetAllAsync(PagingModel pagingModel, ScriptVersionIncludeOptions includeOptions, ScriptVersionFilter filter, CancellationToken token)
    {
        var keyParams = GetKeyParams(includeOptions);

        var ids = await _scriptVersionRepository.GetAllIdsAsync(pagingModel, includeOptions, filter, token);

        var cached = await _cacheService.GetAsync<Guid, ScriptVersion>(ids, keyParams, token);

        var cachedIds = cached.Select(x => x.Id).ToList();
        var requiredIds = ids.Except(cachedIds).ToList();

        if (requiredIds.Count == 0)
            return cached;

        filter.Ids = requiredIds;
        var stored = await _scriptVersionRepository.GetAllAsync(pagingModel, includeOptions, filter, token);

        var mapped = stored.ToDictionary(x => x.Id, x => x);
        await _cacheService.UpsertAsync(mapped, keyParams, token);

        var result = stored.Concat(cached);

        return result;
    }

    public Task<IEnumerable<Guid>> GetAllIdsAsync(PagingModel pagingModel, ScriptVersionIncludeOptions includeOptions, ScriptVersionFilter filter, CancellationToken token)
    {
        return _scriptVersionRepository.GetAllIdsAsync(pagingModel, includeOptions, filter, token);
    }

    public async Task<ScriptVersion?> GetAsync(Guid id, ScriptVersionIncludeOptions includeOptions, CancellationToken token)
    {
        var keyParams = GetKeyParams(includeOptions);

        var cached = await _cacheService.GetAsync<Guid, ScriptVersion?>(id, keyParams, token);

        if (cached is not null)
            return cached;

        var stored = await _scriptVersionRepository.GetAsync(id, includeOptions, token);

        await _cacheService.UpsertAsync(id, stored, keyParams, token);

        return stored;
    }

    public async Task<ScriptVersion?> UpdateAsync(Guid id, ScriptVersion script, CancellationToken token)
    {
        await RemoveCachedAsync([id], token);
        var updated = await _scriptVersionRepository.UpdateAsync(id, script, token);

        return updated;
    }

    private Task RemoveCachedAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        var tasks = new List<Task>();
        // удалить из кэша с существующими комбинациями параметров
        foreach (var paramCombination in _paramCombinations)
        {
            var includeOptions = new ScriptVersionIncludeOptions()
            {
                IncludePrefilterScript = paramCombination[0],
                IncludeRuleScript = paramCombination[1],
                IncludeProcessScript = paramCombination[2],
                IncludeParentScriptVersion = paramCombination[3]
            };
            var keyParams = GetKeyParams(includeOptions);

            tasks.Add(_cacheService.RemoveAsync<Guid, ScriptVersion>(ids, keyParams, cancellationToken));
        }

        return Task.WhenAll(tasks);
    }

    private static Dictionary<string, string> GetKeyParams(ScriptVersionIncludeOptions includeOptions)
    {
        var keyParams = new Dictionary<string, string>()
        {
            { nameof(includeOptions.IncludePrefilterScript), includeOptions.IncludePrefilterScript.ToString() },
            { nameof(includeOptions.IncludeRuleScript), includeOptions.IncludeRuleScript.ToString() },
            { nameof(includeOptions.IncludeProcessScript), includeOptions.IncludeProcessScript.ToString() },
            { nameof(includeOptions.IncludeParentScriptVersion), includeOptions.IncludeParentScriptVersion.ToString() }
        };
        return keyParams;
    }

    private static bool[][] GetParamCombinations(int paramCount)
    {
        var paramCombinations = Enumerable.Range(0, 1 << paramCount)
            .Select(i => new BitVector32(i))
            .Select(x => Enumerable.Range(0, paramCount).Select(j => x[1 << j]).ToArray())
            .ToArray();

        return paramCombinations;
    }
}
