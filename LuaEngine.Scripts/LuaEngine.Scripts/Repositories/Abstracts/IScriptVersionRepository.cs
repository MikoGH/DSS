using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using Monq.Core.Paging.Models;

namespace LuaEngine.Scripts.WebApi.Repositories.Abstracts;

/// <summary>
/// Интерфейс репозитория версий скриптов.
/// </summary>
public interface IScriptVersionRepository
{
    /// <summary>
    /// Получить коллекцию версий скриптов.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filter">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция версий скриптов.</returns>
    public Task<IEnumerable<ScriptVersion>> GetAllAsync(PagingModel pagingModel, ScriptVersionFilter filter, CancellationToken token);

    /// <summary>
    /// Получить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    public Task<ScriptVersion?> GetAsync(Guid id, CancellationToken token);

    /// <summary>
    /// Добавить версию скрипта.
    /// </summary>
    /// <param name="script">Версия скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    public Task<ScriptVersion?> AddAsync(ScriptVersion script, CancellationToken token);

    /// <summary>
    /// Изменить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="script">Версия скрипта.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    public Task<ScriptVersion?> UpdateAsync(Guid id, ScriptVersion script, CancellationToken token);

    /// <summary>
    /// Удалить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken token);
}
