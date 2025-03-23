using LuaEngine.Scripts.Models.RuleScript;
using Monq.Core.Paging.Models;
using Refit;

namespace LuaEngine.Prefilter.Repositories.Abstractions.RefitClients;

public interface IRuleScriptClient
{
    /// <summary>
    /// Получить коллекцию скриптов-правил.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-правил.</returns>
    [Post("/scripts-api/rulescript/filter")]
    Task<ApiResponse<IEnumerable<RuleScriptViewModel>>> GetAllAsync(
        [Query] PagingModel pagingModel,
        [Body] RuleScriptFilterViewModel filterViewModel,
        CancellationToken token);

    /// <summary>
    /// Получить скрипт-правило по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    [Get("/scripts-api/rulescript/{id}")]
    Task<ApiResponse<RuleScriptViewModel?>> GetAsync([AliasAs("id")] Guid id, CancellationToken token);
}
