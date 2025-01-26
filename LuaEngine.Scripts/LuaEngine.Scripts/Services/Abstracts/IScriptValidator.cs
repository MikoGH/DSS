using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

/// <summary>
/// Интерфейс валидатора скриптов.
/// </summary>
public interface IScriptValidator
{
    public bool ValidateScript(Script script);
}
