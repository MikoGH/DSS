namespace LuaEngine.LuaOrchestrator.Models;

/// <summary>
/// Данные из внешнего источника для обработки.
/// </summary>
public class RawData
{
    /// <summary>
    /// Источник.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Данные.
    /// </summary>
    public string JsonData { get; set; }
}
