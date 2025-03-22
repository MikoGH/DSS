namespace LuaEngine.Scripts.Models.PrefilterScript;

/// <summary>
/// Предстваление фильтра скриптов-префильтров.
/// </summary>
public class PrefilterScriptFilterViewModel
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Идентификаторы источников.
    /// </summary>
    public IEnumerable<Guid>? SourceIds { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    public IEnumerable<bool>? Enabled { get; set; }
}
