namespace LuaEngine.Scripts.Models.PrefilterScript;

/// <summary>
/// Представление обновления скрипта-префильтра.
/// </summary>
public class PrefilterScriptPutViewModel
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
