namespace LuaEngine.LuaAgent.Services.Abstractions;

/// <summary>
/// Интерфейс сервиса выполнения скриптов.
/// </summary>
public interface IScriptExecutorService
{
    /// <summary>
    /// Выполнить скрипт для заданного объекта.
    /// </summary>
    /// <param name="scriptCode">Код скрипта.</param>
    /// <param name="model">Объект.</param>
    /// <returns>Обработанный объект.</returns>
    public object ExecuteScript(string scriptCode, object model);
}
