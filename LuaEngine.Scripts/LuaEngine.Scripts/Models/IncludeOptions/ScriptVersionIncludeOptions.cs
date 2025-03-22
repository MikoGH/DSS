namespace LuaEngine.Scripts.WebApi.Models.IncludeOptions;

/// <summary>
/// Опции включения сущностей версии скрипта.
/// </summary>
public class ScriptVersionIncludeOptions
{
    /// <summary>
    /// Включить скрипт-префильтр.
    /// </summary>
    public bool IncludePrefilterScript { get; set; }

    /// <summary>
    /// Включить скрипт-правило.
    /// </summary>
    public bool IncludeRuleScript { get; set; }

    /// <summary>
    /// Включить скрипт-обработчик.
    /// </summary>
    public bool IncludeProcessScript { get; set; }

    /// <summary>
    /// Включить родительскую версию скрипта.
    /// </summary>
    public bool IncludeParentScriptVersion { get; set; }
}
