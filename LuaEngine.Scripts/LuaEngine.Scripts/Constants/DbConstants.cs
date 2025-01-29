namespace LuaEngine.Scripts.WebApi.Constants;

/// <summary>
/// Константы базы данных.
/// </summary>
public static class DbConstants
{
    /// <summary>
    /// Название таблицы скриптов.
    /// </summary>
    public const string ScriptTableName = "scripts";

    /// <summary>
    /// Название таблицы правил.
    /// </summary>
    public const string RuleTableName = "rules";

    /// <summary>
    /// Название таблицы версий кода.
    /// </summary>
    public const string CodeVersionTableName = "code_versions";


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
    /// Столбец Идентификатор скрипта.
    /// </summary>
    public const string ScriptIdColumn = "script_id";

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
    /// Столбец Идентификатор правила.
    /// </summary>
    public const string RuleIdColumn = "rule_id";

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

    /// <summary>
    /// Максимальная длина статуса версии кода.
    /// </summary>
    public const int CodeVersionStatusMaxLength = 16;

    /// <summary>
    /// Максимальная длина типа версии кода.
    /// </summary>
    public const int CodeVersionTypeMaxLength = 16;
}
