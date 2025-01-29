using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

/// <summary>
/// Интерфейс валидатора скриптов.
/// </summary>
public interface ICodeValidator
{
    /// <summary>
    /// Проверить валидность кода.
    /// </summary>
    /// <param name="codeVersion">Версия кода.</param>
    /// <returns>true, если код валидный, иначе false.</returns>
    public bool ValidateCode(CodeVersion codeVersion);
}
