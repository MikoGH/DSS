using LuaEngine.Prefilter.Repositories.Abstractions;
using LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;
using LuaEngine.Scripts.Models.Script;
using Monq.Core.Paging.Models;
using System.Net;

namespace LuaEngine.Prefilter.Repositories;

public class ProcessScriptRepository : IProcessScriptRepository
{
    private readonly IProcessScriptClient _client;
    private readonly IHttpContextAccessor _contextAccessor;

    public ProcessScriptRepository(IProcessScriptClient client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProcessScriptViewModel>> GetAllAsync(PagingModel pagingModel, ProcessScriptFilterViewModel filterViewModel, CancellationToken token)
    {
        var response = await _client.GetAllAsync(pagingModel, filterViewModel, token);

        // TODO: в константы.
        if (!response.IsSuccessStatusCode)
            throw new Exception("Ошибка сервиса скриптов при получении коллекции скриптов-обработчиков: {0}", response.Error);

        if (response.Content is null)
            throw new Exception("Ошибка сервиса скриптов при получении коллекции скриптов-обработчиков: {0}", response.Error);

        // TODO: установить заголовки.

        return response.Content!;
    }

    /// <inheritdoc/>
    public async Task<ProcessScriptViewModel?> GetAsync(Guid id, CancellationToken token)
    {
        var response = await _client.GetAsync(id, token);

        // TODO: в константы.
        if (!response.IsSuccessStatusCode)
            throw new Exception("Ошибка сервиса скриптов при получении скрипта-обработчика: {0}", response.Error);

        if (response.Content is null)
            throw new Exception("Ошибка сервиса скриптов при получении скрипта-обработчика: {0}", response.Error);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new Exception("Ошибка сервиса скриптов при получении скрипта-обработчика - скрипт не найден: {0}", response.Error);

        return response.Content!;
    }
}
