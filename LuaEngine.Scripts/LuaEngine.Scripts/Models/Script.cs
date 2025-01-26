namespace LuaEngine.Scripts.WebApi.Models;

/// <summary>
/// Скрипт.
/// </summary>
public class Script
{
    /// <summary>
    /// Идентификатор скрипта.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название скрипта.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание скрипта.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Код скрипта.
    /// </summary>
    public string ScriptCode { get; set; }
}
