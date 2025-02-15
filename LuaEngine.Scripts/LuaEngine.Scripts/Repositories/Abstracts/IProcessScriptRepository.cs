using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using Monq.Core.Paging.Models;

namespace LuaEngine.Scripts.WebApi.Repositories.Abstracts;

/// <summary>
/// Интерфейс репозитория скриптов.
/// </summary>
public interface IProcessScriptRepository
{
    /// <summary>
    /// Получить коллекцию скриптов-обработчиков.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filter">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-обработчиков.</returns>
    public Task<IEnumerable<ProcessScript>> GetAllAsync(PagingModel pagingModel, ProcessScriptFilter filter, CancellationToken token);

    /// <summary>
    /// Получить скрипт-обработчик по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>скрипт-обработчик.</returns>
    public Task<ProcessScript?> GetAsync(Guid id, CancellationToken token);

    /// <summary>
    /// Добавить скрипт-обработчик.
    /// </summary>
    /// <param name="script">Скрипт-обработчик.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-обработчик.</returns>
    public Task<ProcessScript?> AddAsync(ProcessScript script, CancellationToken token);

    /// <summary>
    /// Изменить скрипт-обработчик по идентификатору.
    /// </summary>
    /// <param name="script">Скрипт-обработчик.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-обработчик.</returns>
    public Task<ProcessScript?> UpdateAsync(Guid id, ProcessScript script, CancellationToken token);

    /// <summary>
    /// Удалить скрипт-обработчик по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken token);
}
