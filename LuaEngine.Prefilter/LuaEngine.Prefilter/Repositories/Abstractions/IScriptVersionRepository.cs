using LuaEngine.Scripts.Models.ScriptVersion;
using Monq.Core.Paging.Models;

namespace LuaEngine.Prefilter.Repositories.Abstractions;

/// <summary>
/// Интерфейс репозитория версий скриптов.
/// </summary>
public interface IScriptVersionRepository
{
    /// <summary>
    /// Получить коллекцию версий скриптов.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="includeViewModel">Опции включения сущностей.</param>
    /// <param name="filterViewModel">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция версий скриптов.</returns>
    Task<IEnumerable<ScriptVersionViewModel>> GetAllAsync(
        PagingModel pagingModel,
        ScriptVersionIncludeViewModel includeViewModel,
        ScriptVersionFilterViewModel filterViewModel,
        CancellationToken token);

    /// <summary>
    /// Получить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="includeViewModel">Опции включения сущностей.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    Task<ScriptVersionViewModel?> GetAsync(Guid id, ScriptVersionIncludeViewModel includeViewModel, CancellationToken token);
}
