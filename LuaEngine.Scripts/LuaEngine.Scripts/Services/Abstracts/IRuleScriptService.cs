using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

/// <summary>
/// Интерфейс сервиса скриптов-правил.
/// </summary>
public interface IRuleScriptService
{
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
    /// <param name="ruleScript">Скрипт-правило.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-правило.</returns>
    public Task<RuleScript?> UpdateAsync(RuleScript ruleScript, CancellationToken token);
}
