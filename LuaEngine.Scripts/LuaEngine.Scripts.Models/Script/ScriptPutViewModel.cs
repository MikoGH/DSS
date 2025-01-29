namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление обновления скрипта.
/// </summary>
public class ScriptPutViewModel
{
    /// <summary>
    /// Идентификатор источника.
    /// </summary>
    public Guid SourceId { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
}
