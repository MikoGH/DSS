namespace LuaEngine.Scripts.Models.ProcessScript;

/// <summary>
/// Представление добавления скрипта-обработчика.
/// </summary>
public class ProcessScriptPostViewModel
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
