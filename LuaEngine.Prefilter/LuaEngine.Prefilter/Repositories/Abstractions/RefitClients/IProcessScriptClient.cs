using LuaEngine.Scripts.Models.Script;
using Monq.Core.Paging.Models;
using Refit;

namespace LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;

public interface IProcessScriptClient
{
    /// <summary>
    /// Получить коллекцию скриптов-обработчиков.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-обработчиков.</returns>
    [Post("/scripts-api/processscript/filter")]
    Task<ApiResponse<IEnumerable<ProcessScriptViewModel>>> GetAllAsync(
        [Query] PagingModel pagingModel,
        [Body] ProcessScriptFilterViewModel filterViewModel,
        CancellationToken token);

    /// <summary>
    /// Получить скрипт-обработчик по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-обработчик.</returns>
    [Get("/scripts-api/processscript/{id}")]
    Task<ApiResponse<ProcessScriptViewModel?>> GetAsync([AliasAs("id")] Guid id, CancellationToken token);
}
