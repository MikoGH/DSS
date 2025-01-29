namespace LuaEngine.Scripts.WebApi.Models;

/// <summary>
/// Версия кода.
/// </summary>
public class CodeVersion
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тип кода.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Идентификатор скрипта.
    /// </summary>
    public Guid? ScriptId { get; set; }
    public Script? Script { get; set; }

    /// <summary>
    /// Идентификатор правила.
    /// </summary>
    public Guid? RuleId { get; set; }
    public Rule? Rule { get; set; }

    /// <summary>
    /// Идентификатор родительского кода.
    /// </summary>
    public Guid? ParentId { get; set; }
    public CodeVersion? Parent { get; set; }

    /// <summary>
    /// Код.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Версия.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    public string Status { get; set; }
}
