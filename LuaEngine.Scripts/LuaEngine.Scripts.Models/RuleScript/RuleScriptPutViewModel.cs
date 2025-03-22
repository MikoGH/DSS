namespace LuaEngine.Scripts.Models.RuleScript;

/// <summary>
/// Представление обновления скрипта-правила.
/// </summary>
public class RuleScriptPutViewModel
{
    /// <summary>
    /// Идентификатор источника.
    /// </summary>
    public Guid SourceId { get; set; }

    /// <summary>
    /// Идентификатор скрипта-обработчика.
    /// </summary>
    public Guid ProcessScriptId { get; set; }

    /// <summary>
    /// Приоритет.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    public bool Enabled { get; set; }
}
