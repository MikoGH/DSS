namespace LuaEngine.Scripts.Models.Enums;

/// <summary>
/// Тип скрипта.
/// </summary>
public enum ScriptType : byte
{
    /// <summary>
    /// Скрипт-обработчик.
    /// </summary>
    Process = 0,

    /// <summary>
    /// Скрипт-правило.
    /// </summary>
    Rule = 1
}
