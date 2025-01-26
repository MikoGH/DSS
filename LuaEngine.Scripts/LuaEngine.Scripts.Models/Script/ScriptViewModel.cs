using System.ComponentModel;

namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление скрипта.
/// </summary>
[Description("Представление скрипта.")]
public class ScriptViewModel
{
    /// <summary>
    /// Идентификатор скрипта.
    /// </summary>
    [Description("Идентификатор скрипта.")]
    public Guid Id { get; set; }

    /// <summary>
    /// Название скрипта.
    /// </summary>
    [Description("Название скрипта.")]
    public string Name { get; set; }

    /// <summary>
    /// Описание скрипта.
    /// </summary>
    [Description("Описание скрипта.")]
    public string? Description { get; set; }

    /// <summary>
    /// Код скрипта.
    /// </summary>
    [Description("Код скрипта.")]
    public string ScriptCode { get; set; }
}
