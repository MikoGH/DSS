using LuaEngine.Scripts.Models.Enums;
using LuaEngine.Scripts.Models.PrefilterScript;
using LuaEngine.Scripts.Models.ProcessScript;
using LuaEngine.Scripts.Models.RuleScript;

namespace LuaEngine.Scripts.Models.ScriptVersion;

/// <summary>
/// Представление версии скрипта.
/// </summary>
public class ScriptVersionViewModel
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тип скрипта.
    /// </summary>
    public ScriptType Type { get; set; }

    /// <summary>
    /// Идентификатор скрипта-префильтра.
    /// </summary>
    public Guid? PrefilterScriptId { get; set; }
    public PrefilterScriptViewModel? PrefilterScript { get; set; }

    /// <summary>
    /// Идентификатор скрипта-правила.
    /// </summary>
    public Guid? RuleScriptId { get; set; }
    public RuleScriptViewModel? RuleScript { get; set; }

    /// <summary>
    /// Идентификатор скрипта-обработчика.
    /// </summary>
    public Guid? ProcessScriptId { get; set; }
    public ProcessScriptViewModel? ProcessScript { get; set; }

    /// <summary>
    /// Идентификатор родительской версии скрипта.
    /// </summary>
    public Guid? ParentId { get; set; }
    public ScriptVersionViewModel? Parent { get; set; }

    /// <summary>
    /// Код скрипта.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Версия.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    public Status Status { get; set; }
}
