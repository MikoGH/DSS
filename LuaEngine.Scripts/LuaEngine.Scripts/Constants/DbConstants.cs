namespace LuaEngine.Scripts.WebApi.Constants;

/// <summary>
/// Константы базы данных.
/// </summary>
public static class DbConstants
{
    /// <summary>
    /// Название таблицы скриптов-префильтров.
    /// </summary>
    public const string PrefilterScriptTableName = "prefilter_scripts";

    /// <summary>
    /// Название таблицы скриптов-правил.
    /// </summary>
    public const string RuleScriptTableName = "rule_scripts";

    /// <summary>
    /// Название таблицы скриптов-обработчиков.
    /// </summary>
    public const string ProcessScriptTableName = "process_scripts";

    /// <summary>
    /// Название таблицы версий скриптов.
    /// </summary>
    public const string ScriptVersionTableName = "script_versions";


    /// <summary>
    /// Столбец Идентификатор.
    /// </summary>
    public const string IdColumn = "id";

    /// <summary>
    /// Столбец Идентификатор источника.
    /// </summary>
    public const string SourceIdColumn = "source_id";

    /// <summary>
    /// Столбец Название.
    /// </summary>
    public const string NameColumn = "name";

    /// <summary>
    /// Столбец Описание.
    /// </summary>
    public const string DescriptionColumn = "description";

    /// <summary>
    /// Столбец Идентификатор скрипта-обработчика.
    /// </summary>
    public const string ProcessScriptIdColumn = "process_script_id";

    /// <summary>
    /// Столбец Приоритет.
    /// </summary>
    public const string PriorityColumn = "priority";

    /// <summary>
    /// Столбец Активно.
    /// </summary>
    public const string EnabledColumn = "enabled";

    /// <summary>
    /// Столбец Префильтр.
    /// </summary>
    public const string PrefilterColumn = "prefilter";

    /// <summary>
    /// Столбец Тип.
    /// </summary>
    public const string TypeColumn = "type";

    /// <summary>
    /// Столбец Код.
    /// </summary>
    public const string CodeColumn = "code";

    /// <summary>
    /// Столбец Идентификатор скрипта-префильтра.
    /// </summary>
    public const string PrefilterScriptIdColumn = "prefilter_script_id";

    /// <summary>
    /// Столбец Идентификатор скрипта-правила.
    /// </summary>
    public const string RuleScriptIdColumn = "rule_script_id";

    /// <summary>
    /// Столбец Идентификатор родителя.
    /// </summary>
    public const string ParentIdColumn = "parent_id";

    /// <summary>
    /// Столбец Версия.
    /// </summary>
    public const string VersionColumn = "version";

    /// <summary>
    /// Столбец Статус.
    /// </summary>
    public const string StatusColumn = "status";


    /// <summary>
    /// Максимальная длина названия скрипта.
    /// </summary>
    public const int ScriptNameMaxLength = 64;

    /// <summary>
    /// Максимальная длина описания скрипта.
    /// </summary>
    public const int ScriptDescriptionMaxLength = 1024;
}
