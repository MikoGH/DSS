using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using Monq.Core.Paging.Models;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

/// <summary>
/// Интерфейс сервиса скриптов-правил.
/// </summary>
public interface IRuleScriptService
{
    /// <summary>
    /// Получить коллекцию скриптов-правил.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filter">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-правил.</returns>
    public Task<IEnumerable<RuleScript>> GetAllAsync(PagingModel pagingModel, RuleScriptFilter filter, CancellationToken token);

    /// <summary>
    /// Получить скрипт-правило по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    public Task<RuleScript?> GetAsync(Guid id, CancellationToken token);

    /// <summary>
    /// Добавить скрипт-правило.
    /// </summary>
    /// <param name="ruleScript">Скрипт-правило.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    public Task<RuleScript?> AddAsync(RuleScript ruleScript, CancellationToken token);

    /// <summary>
    /// Обновить скрипт-правило.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="ruleScript">Скрипт-правило.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    public Task<RuleScript?> UpdateAsync(Guid id, RuleScript ruleScript, CancellationToken token);

    /// <summary>
    /// Удалить скрипт-правило по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken token);
}
