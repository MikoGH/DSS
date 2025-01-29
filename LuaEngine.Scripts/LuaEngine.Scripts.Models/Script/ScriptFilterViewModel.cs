﻿namespace LuaEngine.Scripts.Models.Script;

/// <summary>
/// Представление фильтрации скриптов.
/// </summary>
public class ScriptFilterViewModel
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Идентификаторы источников.
    /// </summary>
    public IEnumerable<Guid>? SourceIds { get; set; }

    /// <summary>
    /// Наименования.
    /// </summary>
    public IEnumerable<string>? Names { get; set; }
}
