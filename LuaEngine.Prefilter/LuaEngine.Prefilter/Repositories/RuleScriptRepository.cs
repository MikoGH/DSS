using LuaEngine.Prefilter.Repositories.Abstractions;
using LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;
using LuaEngine.Scripts.Models.Rule;
using Monq.Core.Paging.Models;
using System.Net;

namespace LuaEngine.Prefilter.Repositories;

public class RuleScriptRepository : IRuleScriptRepository
{
    private readonly IRuleScriptClient _client;
    private readonly IHttpContextAccessor _contextAccessor;

    public RuleScriptRepository(IRuleScriptClient client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RuleScriptViewModel>> GetAllAsync(PagingModel pagingModel, RuleScriptFilterViewModel filterViewModel, CancellationToken token)
    {
        var response = await _client.GetAllAsync(pagingModel, filterViewModel, token);

        // TODO: в константы.
        if (!response.IsSuccessStatusCode)
            throw new Exception("Ошибка сервиса скриптов при получении коллекции правил: {0}", response.Error);

        if (response.Content is null)
            throw new Exception("Ошибка сервиса скриптов при получении коллекции правил: {0}", response.Error);

        // TODO: установить заголовки.

        return response.Content!;
    }

    /// <inheritdoc/>
    public async Task<RuleScriptViewModel?> GetAsync(Guid id, CancellationToken token)
    {
        var response = await _client.GetAsync(id, token);

        // TODO: в константы.
        if (!response.IsSuccessStatusCode)
            throw new Exception("Ошибка сервиса скриптов при получении правила: {0}", response.Error);

        if (response.Content is null)
            throw new Exception("Ошибка сервиса скриптов при получении правила: {0}", response.Error);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new Exception("Ошибка сервиса скриптов при получении правила - правило не найдено: {0}", response.Error);

        return response.Content!;
    }
}
