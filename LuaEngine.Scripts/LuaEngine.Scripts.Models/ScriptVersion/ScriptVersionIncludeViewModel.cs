namespace LuaEngine.Scripts.Models.ScriptVersion;

/// <summary>
/// Представление опций включения сущностей версии скрипта.
/// </summary>
public class ScriptVersionIncludeViewModel
{
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
