namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление фильтрации скриптов.
/// </summary>
public class ScriptFilterViewModel
{
    public IEnumerable<Guid> Ids { get; set; }
    public IEnumerable<string> Names { get; set; }
}
