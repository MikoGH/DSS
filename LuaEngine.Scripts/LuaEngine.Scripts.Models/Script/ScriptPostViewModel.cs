namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление добавления скрипта.
/// </summary>
public class ScriptPostViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string ScriptCode { get; set; }
}
