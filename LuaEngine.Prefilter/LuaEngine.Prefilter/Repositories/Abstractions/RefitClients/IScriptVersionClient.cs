using LuaEngine.Scripts.Models.ScriptVersion;
using Monq.Core.Paging.Models;
using Refit;

namespace LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;

public interface IScriptVersionClient
{
    /// <summary>
    /// Получить коллекцию версий скриптов.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="includeViewModel">Опции включения сущностей.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция версий скриптов.</returns>
    [Post("/scripts-api/scriptversion/filter")]
    Task<ApiResponse<IEnumerable<ScriptVersionViewModel>>> GetAllAsync(
        [Query] PagingModel pagingModel,
        [Query] ScriptVersionIncludeViewModel includeViewModel,
        [Body] ScriptVersionFilterViewModel filterViewModel,
        CancellationToken token);

    /// <summary>
    /// Получить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="includeViewModel">Опции включения сущностей.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    [Get("/scripts-api/scriptversion/{id}")]
    Task<ApiResponse<ScriptVersionViewModel?>> GetAsync(
        [AliasAs("id")] Guid id,
        [Query] ScriptVersionIncludeViewModel includeViewModel,
        CancellationToken token);
}
