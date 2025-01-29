namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление скрипта.
/// </summary>
public class ScriptViewModel
{
    /// <summary>
    /// Идентификатор скрипта.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор источника.
    /// </summary>
    public Guid SourceId { get; set; }

    /// <summary>
    /// Название скрипта.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание скрипта.
    /// </summary>
    public string? Description { get; set; }
}
