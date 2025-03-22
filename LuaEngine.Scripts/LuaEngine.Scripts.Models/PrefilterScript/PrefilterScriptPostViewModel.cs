namespace LuaEngine.Scripts.Models.PrefilterScript;

/// <summary>
/// Представление добавления скрипта-префильтра.
/// </summary>
public class PrefilterScriptPostViewModel
{
    /// <summary>
    /// Идентификатор источника.
    /// </summary>
    public Guid SourceId { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    public bool Enabled { get; set; }
}
