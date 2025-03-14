﻿using LuaEngine.Scripts.Models.Enums;
using Monq.Core.MvcExtensions.Filters;

namespace LuaEngine.Scripts.WebApi.Models.Filters;

/// <summary>
/// Фильтр версий скриптов.
/// </summary>
public class ScriptVersionFilter
{
    /// <summary>
    /// Идентификаторы.
    /// </summary>
    [FilteredBy(nameof(ScriptVersion.Id))]
    public IEnumerable<Guid>? Ids { get; set; }

    /// <summary>
    /// Типы скриптов.
    /// </summary>
    [FilteredBy(nameof(ScriptVersion.Type))]
    public IEnumerable<ScriptType>? Types { get; set; }

    /// <summary>
    /// Идентификатор скриптов-обработчиков.
    /// </summary>
    [FilteredBy(nameof(ScriptVersion.ProcessScriptId))]
    public IEnumerable<Guid>? ProcessScriptIds { get; set; }

    /// <summary>
    /// Идентификаторы скриптов-правил.
    /// </summary>
    [FilteredBy(nameof(ScriptVersion.RuleScriptId))]
    public IEnumerable<Guid>? RuleScriptId { get; set; }

    /// <summary>
    /// Идентификаторы родительских версий скриптов.
    /// </summary>
    [FilteredBy(nameof(ScriptVersion.ParentId))]
    public IEnumerable<Guid>? ParentIds { get; set; }

    /// <summary>
    /// Версии.
    /// </summary>
    [FilteredBy(nameof(ScriptVersion.Version))]
    public IEnumerable<int>? Versions { get; set; }

    /// <summary>
    /// Статусы.
    /// </summary>
    [FilteredBy(nameof(ScriptVersion.Status))]
    public IEnumerable<Status>? Status { get; set; }
}
