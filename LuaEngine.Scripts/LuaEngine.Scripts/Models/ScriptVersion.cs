using LuaEngine.Scripts.Models.Enums;

namespace LuaEngine.Scripts.WebApi.Models;

/// <summary>
/// Версия скрипта.
/// </summary>
public class ScriptVersion
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
    public PrefilterScript? PrefilterScript { get; set; }

    /// <summary>
    /// Идентификатор скрипта-правила.
    /// </summary>
    public Guid? RuleScriptId { get; set; }
    public RuleScript? RuleScript { get; set; }

    /// <summary>
    /// Идентификатор скрипта-обработчика.
    /// </summary>
    public Guid? ProcessScriptId { get; set; }
    public ProcessScript? ProcessScript { get; set; }

    /// <summary>
    /// Идентификатор родительской версии скрипта.
    /// </summary>
    public Guid? ParentId { get; set; }
    public ScriptVersion? Parent { get; set; }

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
