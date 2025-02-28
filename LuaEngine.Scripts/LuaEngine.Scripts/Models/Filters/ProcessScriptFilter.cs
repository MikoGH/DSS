using Monq.Core.MvcExtensions.Filters;

namespace LuaEngine.Scripts.WebApi.Models.Filters;

/// <summary>
/// Фильтр скриптов-обработчиков.
/// </summary>
public class ProcessScriptFilter
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    [FilteredBy(nameof(ProcessScript.Id))]
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Идентификаторы источников.
    /// </summary>
    [FilteredBy(nameof(ProcessScript.SourceId))]
    public IEnumerable<Guid>? SourceIds { get; set; }

    /// <summary>
    /// Наименования.
    /// </summary>
    [FilteredBy(nameof(ProcessScript.Name))]
    public IEnumerable<string>? Names { get; set; }
}
