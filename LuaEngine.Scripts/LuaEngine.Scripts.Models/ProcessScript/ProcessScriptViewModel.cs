namespace LuaEngine.Scripts.Models.ProcessScript;

/// <summary>
/// Представление скрипта-обработчика.
/// </summary>
public class ProcessScriptViewModel
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
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
}
