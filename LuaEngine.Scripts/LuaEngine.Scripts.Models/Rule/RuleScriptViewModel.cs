﻿using LuaEngine.Scripts.Models.Script;

namespace LuaEngine.Scripts.Models.Rule;

/// <summary>
/// Представление скрипта-правила.
/// </summary>
public class RuleScriptViewModel
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор источника.
    /// </summary>
    public Guid SourceId { get; set; }

    /// <summary>
    /// Идентификатор скрипта-обработчика.
    /// </summary>
    public Guid? ProcessScriptId { get; set; }
    public ProcessScriptViewModel? ProcessScript { get; set; }

    /// <summary>
    /// Приоритет.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Является префильтром.
    /// </summary>
    public bool Prefilter { get; set; }

    /// <summary>
    /// Активно.
    /// </summary>
    public bool Enabled { get; set; }
}
