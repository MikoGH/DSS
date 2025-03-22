using LuaEngine.Scripts.Models.Enums;

namespace LuaEngine.Scripts.Models.ScriptVersion;

/// <summary>
/// Представление фильтрации версии скрипта.
/// </summary>
public class ScriptVersionFilterViewModel
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Типы скриптов.
    /// </summary>
    public IEnumerable<ScriptType>? Types { get; set; }

    /// <summary>
    /// Идентификаторы скриптов-префильтров.
    /// </summary>
    public IEnumerable<Guid?>? PrefilterScriptIds { get; set; }

    /// <summary>
    /// Идентификаторы скриптов-правил.
    /// </summary>
    public IEnumerable<Guid?>? RuleScriptIds { get; set; }

    /// <summary>
    /// Идентификаторы скриптов-обработчиков.
    /// </summary>
    public IEnumerable<Guid?>? ProcessScriptIds { get; set; }

    /// <summary>
    /// Идентификаторы родительских версий скриптов.
    /// </summary>
    public IEnumerable<Guid?>? ParentIds { get; set; }

    /// <summary>
    /// Версии.
    /// </summary>
    public IEnumerable<int>? Versions { get; set; }

    /// <summary>
    /// Статусы.
    /// </summary>
    public IEnumerable<Status>? Statuses { get; set; }

    /// <summary>
    /// Идентификаторы источников.
    /// </summary>
    public IEnumerable<Guid>? SourceIds { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    public IEnumerable<bool>? Enabled { get; set; }
}
