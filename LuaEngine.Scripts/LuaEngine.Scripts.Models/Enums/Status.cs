namespace LuaEngine.Scripts.Models.Enums;

/// <summary>
/// Статус скрипта.
/// </summary>
public enum Status : byte
{
    /// <summary>
    /// Исполняемый.
    /// </summary>
    Executable = 0,

    /// <summary>
    /// Черновик.
    /// </summary>
    Template = 1,

    /// <summary>
    /// Ошибка.
    /// </summary>
    Error = 2,

    /// <summary>
    /// Архивный.
    /// </summary>
    Archived = 3
}
