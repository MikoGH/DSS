using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

public interface IRuleService
{
    /// <summary>
    /// Добавить правило.
    /// </summary>
    /// <param name="script">Lua-скрипт.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Правило.</returns>
    public Task<Rule?> AddAsync(Script script, CancellationToken token);

    /// <summary>
    /// Обновить правило.
    /// </summary>
    /// <param name="script">Lua-скрипт.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Правило.</returns>
    public Task<Rule?> UpdateAsync(Script script, CancellationToken token);
}
