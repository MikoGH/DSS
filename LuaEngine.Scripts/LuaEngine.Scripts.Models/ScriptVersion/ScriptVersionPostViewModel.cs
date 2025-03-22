using LuaEngine.Scripts.Models.Enums;

namespace LuaEngine.Scripts.Models.ScriptVersion;

/// <summary>
/// Представление добавления версии скрипта.
/// </summary>
public class ScriptVersionPostViewModel
{
    /// <summary>
    /// Тип скрипта.
    /// </summary>
    public ScriptType Type { get; set; }

    /// <summary>
    /// Идентификатор скрипта-префильтра.
    /// </summary>
    public Guid? PrefilterScriptId { get; set; }

    /// <summary>
    /// Идентификатор скрипта-правила.
    /// </summary>
    public Guid? RuleScriptId { get; set; }

    /// <summary>
    /// Идентификатор скрипта-обработчика.
    /// </summary>
    public Guid? ProcessScriptId { get; set; }

    /// <summary>
    /// Идентификатор родительской версии скрипта.
    /// </summary>
    public Guid? ParentId { get; set; }

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
