namespace LuaEngine.Scripts.WebApi.Constants;

public static class DbConstants
{
    /// <summary>
    /// Название таблицы скриптов.
    /// </summary>
    public const string ScriptTableName = "scripts";

    /// <summary>
    /// Название таблицы событий выполнения скриптов.
    /// </summary>
    public const string ScriptEventTableName = "script_events";


    /// <summary>
    /// Столбец Идентификатор.
    /// </summary>
    public const string IdColumn = "id";

    /// <summary>
    /// Столбец Название.
    /// </summary>
    public const string NameColumn = "name";

    /// <summary>
    /// Столбец Описание.
    /// </summary>
    public const string DescriptionColumn = "description";

    /// <summary>
    /// Столбец Код скрипта.
    /// </summary>
    public const string ScriptCodeColumn = "script_code";

    /// <summary>
    /// Столбец Идентификатор скрипта.
    /// </summary>
    public const string ScriptIdColumn = "script_id";


    /// <summary>
    /// Максимальная длина названия скрипта.
    /// </summary>
    public const int ScriptNameMaxLength = 30;

    /// <summary>
    /// Максимальная длина описания скрипта.
    /// </summary>
    public const int ScriptDescriptionMaxLength = 400;
}
