using Monq.Core.MvcExtensions.Filters;

namespace LuaEngine.Scripts.WebApi.Models.Filters;

/// <summary>
/// Фильтр скриптов-правил.
/// </summary>
public class RuleScriptFilter
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    [FilteredBy(nameof(RuleScript.Id))]
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Идентификаторы источников.
    /// </summary>
    [FilteredBy(nameof(RuleScript.SourceId))]
    public IEnumerable<Guid>? SourceIds { get; set; }

    /// <summary>
    /// Идентификаторы скриптов-обработчиков.
    /// </summary>
    [FilteredBy(nameof(RuleScript.ProcessScriptId))]
    public IEnumerable<Guid?>? ProcessScriptIds { get; set; }

    /// <summary>
    /// Приоритеты.
    /// </summary>
    [FilteredBy(nameof(RuleScript.Priority))]
    public IEnumerable<int>? Priorities { get; set; }

    /// <summary>
    /// Является префильтром.
    /// </summary>
    [FilteredBy(nameof(RuleScript.Prefilter))]
    public IEnumerable<bool>? Prefilter { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    [FilteredBy(nameof(RuleScript.Enabled))]
    public IEnumerable<bool>? Enabled { get; set; }
}
