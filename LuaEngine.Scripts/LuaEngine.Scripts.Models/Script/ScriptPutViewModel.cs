namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление обновления скрипта.
/// </summary>
public class ScriptPutViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string ScriptCode { get; set; }
}
