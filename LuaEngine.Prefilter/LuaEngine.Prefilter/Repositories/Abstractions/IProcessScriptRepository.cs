using LuaEngine.Scripts.Models.Script;
using Monq.Core.Paging.Models;

namespace LuaEngine.Prefilter.Repositories.Abstractions;

public interface IProcessScriptRepository
{
    /// <summary>
    /// Получить коллекцию скриптов-обработчиков.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-обработчиков.</returns>
    Task<IEnumerable<ProcessScriptViewModel>> GetAllAsync(
        PagingModel pagingModel,
        ProcessScriptFilterViewModel filterViewModel,
        CancellationToken token);

    /// <summary>
    /// Получить скрипт-обработчик по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-обработчик.</returns>
    Task<ProcessScriptViewModel?> GetAsync(Guid id, CancellationToken token);
}
