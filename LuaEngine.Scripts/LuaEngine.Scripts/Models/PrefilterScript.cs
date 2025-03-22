namespace LuaEngine.Scripts.WebApi.Models;

/// <summary>
/// Скрипт-префильтр.
/// </summary>
public class PrefilterScript
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
    /// Активно.
    /// </summary>
    public bool Enabled { get; set; }
}
