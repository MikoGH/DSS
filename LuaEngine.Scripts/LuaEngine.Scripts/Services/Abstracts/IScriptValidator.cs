using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

/// <summary>
/// Интерфейс валидатора скриптов.
/// </summary>
public interface IScriptValidator
{
    /// <summary>
    /// Проверить валидность скрипта.
    /// </summary>
    /// <param name="codeVersion">Версия скрипта.</param>
    /// <returns>true, если код валидный, иначе false.</returns>
    public bool ValidateScript(ScriptVersion codeVersion);
}
