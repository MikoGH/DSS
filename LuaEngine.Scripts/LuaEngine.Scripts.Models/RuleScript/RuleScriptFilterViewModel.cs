namespace LuaEngine.Scripts.Models.RuleScript;

/// <summary>
/// Представление фильтра скриптов-правил.
/// </summary>
public class RuleScriptFilterViewModel
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Идентификаторы источников.
    /// </summary>
    public IEnumerable<Guid>? SourceIds { get; set; }

    /// <summary>
    /// Идентификаторы скриптов-обработчиков.
    /// </summary>
    public IEnumerable<Guid>? ProcessScriptIds { get; set; }

    /// <summary>
    /// Приоритеты.
    /// </summary>
    public IEnumerable<int>? Priorities { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    public IEnumerable<bool>? Enabled { get; set; }
}
