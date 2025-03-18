using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models.IncludeOptions;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

/// <summary>
/// Интерфейс сервиса версий скриптов.
/// </summary>
public interface IScriptVersionService
{
    /// <summary>
    /// Получить коллекцию версий скриптов.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="includeOptions">Опции включения сущностей.</param>
    /// <param name="filter">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция версий скриптов.</returns>
    public Task<IEnumerable<ScriptVersion>> GetAllAsync(PagingModel pagingModel, ScriptVersionIncludeOptions includeOptions, ScriptVersionFilter filter, CancellationToken token);

    /// <summary>
    /// Получить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="includeOptions">Опции включения сущностей.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    public Task<ScriptVersion?> GetAsync(Guid id, ScriptVersionIncludeOptions includeOptions, CancellationToken token);

    /// <summary>
    /// Добавить версию скрипта.
    /// </summary>
    /// <param name="ruleScript">Версия скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    public Task<ScriptVersion?> AddAsync(ScriptVersion ruleScript, CancellationToken token);

    /// <summary>
    /// Обновить версию скрипта.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="ruleScript">Версия скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Версия скрипта.</returns>
    public Task<ScriptVersion?> UpdateAsync(Guid id, ScriptVersion ruleScript, CancellationToken token);

    /// <summary>
    /// Удалить версию скрипта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken token);
}
