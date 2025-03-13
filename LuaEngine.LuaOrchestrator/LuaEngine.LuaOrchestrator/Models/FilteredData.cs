namespace LuaEngine.LuaOrchestrator.Models;

/// <summary>
/// Данные из префильтра для обработки.
/// </summary>
public class FilteredData
{
    /// <summary>
    /// Скрипт для обработки.
    /// </summary>
    public string Script { get; set; }

    /// <summary>
    /// Данные.
    /// </summary>
    public string Body { get; set; }
}
