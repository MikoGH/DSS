namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление обновления скрипта-обработчика.
/// </summary>
public class ProcessScriptPutViewModel
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
