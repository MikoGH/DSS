using Monq.Core.MvcExtensions.Filters;

namespace LuaEngine.Scripts.WebApi.Models.Filters;

/// <summary>
/// Фильтр скриптов-префильтров.
/// </summary>
public class PrefilterScriptFilter
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    [FilteredBy(nameof(PrefilterScript.Id))]
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Идентификаторы источников.
    /// </summary>
    [FilteredBy(nameof(PrefilterScript.SourceId))]
    public IEnumerable<Guid>? SourceIds { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    [FilteredBy(nameof(PrefilterScript.Enabled))]
    public IEnumerable<bool>? Enabled { get; set; }
}
