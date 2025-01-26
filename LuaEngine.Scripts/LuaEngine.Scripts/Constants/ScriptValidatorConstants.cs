namespace LuaEngine.Scripts.WebApi.Constants;

/// <summary>
/// Константы валидатора скриптов.
/// </summary>
public static class ScriptValidatorConstants
{
    /// <summary>
    /// Системные команды.
    /// </summary>
    public static readonly HashSet<string> SystemWords = new HashSet<string>()
    {
        "os", "system"
    };
}
