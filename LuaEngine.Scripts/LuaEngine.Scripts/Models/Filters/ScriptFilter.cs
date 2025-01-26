using Monq.Core.MvcExtensions.Filters;

namespace LuaEngine.Scripts.WebApi.Models.Filters;

/// <summary>
/// Фильтр скриптов.
/// </summary>
public class ScriptFilter
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    [FilteredBy(nameof(Script.Id))]
    public IEnumerable<Guid> Ids { get; set; }

    /// <summary>
    /// Названия.
    /// </summary>
    [FilteredBy(nameof(Script.Name))]
    public IEnumerable<string> Names { get; set; }
}
