using LuaEngine.Scripts.Models.Rule;
using Monq.Core.Paging.Models;

namespace LuaEngine.Prefilter.Repositories.Abstractions;

public interface IRuleScriptRepository
{
    /// <summary>
    /// Получить коллекцию скриптов-правил.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-правил.</returns>
    Task<IEnumerable<RuleScriptViewModel>> GetAllAsync(
        PagingModel pagingModel,
        RuleScriptFilterViewModel filterViewModel,
        CancellationToken token);

    /// <summary>
    /// Получить скрипт-правило по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    Task<RuleScriptViewModel?> GetAsync(Guid id, CancellationToken token);
}
