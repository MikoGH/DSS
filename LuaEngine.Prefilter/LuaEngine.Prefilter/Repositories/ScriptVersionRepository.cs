using LuaEngine.Prefilter.Repositories.Abstractions;
using LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;
using LuaEngine.Scripts.Models.ScriptVersion;
using Monq.Core.Paging.Models;
using System.Net;

namespace LuaEngine.Prefilter.Repositories;

/// <summary>
/// Репозиторий версий скриптов.
/// </summary>
public class ScriptVersionRepository : IScriptVersionRepository
{
    private readonly IScriptVersionClient _client;
    private readonly IHttpContextAccessor _contextAccessor;

    public ScriptVersionRepository(IScriptVersionClient client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ScriptVersionViewModel>> GetAllAsync(PagingModel pagingModel, ScriptVersionIncludeViewModel includeViewModel, ScriptVersionFilterViewModel filterViewModel, CancellationToken token)
    {
        var response = await _client.GetAllAsync(pagingModel, includeViewModel, filterViewModel, token);

        // TODO: в константы.
        if (!response.IsSuccessStatusCode)
            throw new Exception("Ошибка сервиса скриптов при получении коллекции версий скриптов: {0}", response.Error);

        if (response.Content is null)
            throw new Exception("Ошибка сервиса скриптов при получении коллекции версий скриптов: {0}", response.Error);

        // TODO: установить заголовки.

        return response.Content!;
    }

    /// <inheritdoc/>
    public async Task<ScriptVersionViewModel?> GetAsync(Guid id, ScriptVersionIncludeViewModel includeViewModel, CancellationToken token)
    {
        var response = await _client.GetAsync(id, includeViewModel, token);

        // TODO: в константы.
        if (!response.IsSuccessStatusCode)
            throw new Exception("Ошибка сервиса скриптов при получении версии скрипта: {0}", response.Error);

        if (response.Content is null)
            throw new Exception("Ошибка сервиса скриптов при получении версии скрипта: {0}", response.Error);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new Exception("Ошибка сервиса скриптов при получении версии скрипта - скрипт не найден: {0}", response.Error);

        return response.Content!;
    }
}
