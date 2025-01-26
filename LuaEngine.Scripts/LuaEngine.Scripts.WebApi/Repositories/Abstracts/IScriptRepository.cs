using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using Monq.Core.Paging.Models;

namespace LuaEngine.Scripts.WebApi.Repositories.Abstracts;

/// <summary>
/// Интерфейс репозитория скриптов.
/// </summary>
public interface IScriptRepository
{
    /// <summary>
    /// Получить коллекцию Lua-скриптов.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filter">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция Lua-скриптов.</returns>
    public Task<IEnumerable<Script>> GetAllAsync(PagingModel pagingModel, ScriptFilter filter, CancellationToken token);

    /// <summary>
    /// Получить Lua-скрипт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Lua-скрипт.</returns>
    public Task<Script?> GetAsync(Guid id, CancellationToken token);

    /// <summary>
    /// Добавить Lua-скрипт.
    /// </summary>
    /// <param name="script">Lua-скрипт.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Lua-скрипт.</returns>
    public Task<Script?> AddAsync(Script script, CancellationToken token);

    /// <summary>
    /// Изменить Lua-скрипт по идентификатору.
    /// </summary>
    /// <param name="script">Lua-скрипт.</param>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Lua-скрипт.</returns>
    public Task<Script?> UpdateAsync(Guid id, Script script, CancellationToken token);

    /// <summary>
    /// Удалить Lua-скрипт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see cref="true"/>, если скрипт удалён успешно, иначе <see cref="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken token);
}
