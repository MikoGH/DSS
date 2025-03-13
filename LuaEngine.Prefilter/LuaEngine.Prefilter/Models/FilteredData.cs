namespace LuaEngine.LuaOrchestrator.Models;

/// <summary>
/// Данные после фильтрации.
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
