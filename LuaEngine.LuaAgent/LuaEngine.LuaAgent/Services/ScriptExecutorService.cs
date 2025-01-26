using LuaEngine.LuaAgent.Services.Abstractions;

namespace LuaEngine.LuaAgent.Services;

/// <summary>
/// Сервис выполнения скриптов.
/// </summary>
public class ScriptExecutorService : IScriptExecutorService
{
    /// <summary>
    /// Выполнить скрипт для заданного объекта.
    /// </summary>
    /// <param name="scriptCode">Код скрипта.</param>
    /// <param name="model">Объект.</param>
    /// <returns>Обработанный объект.</returns>
    public object ExecuteScript(string scriptCode, object model)
    {
        throw new NotImplementedException();
    }
}
