namespace LuaEngine.Scripts.Models.PrefilterScript;

/// <summary>
/// Представление скрипта-префильтра.
/// </summary>
public class PrefilterScriptViewModel
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор источника.
    /// </summary>
    public Guid SourceId { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    public bool Enabled { get; set; }
}
