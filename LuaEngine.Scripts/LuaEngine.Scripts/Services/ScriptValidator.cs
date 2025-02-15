using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using static LuaEngine.Scripts.WebApi.Constants.ScriptValidatorConstants;

namespace LuaEngine.Scripts.WebApi.Services;

/// <summary>
/// Валидатор скриптов.
/// </summary>
public class ScriptValidator : IScriptValidator
{
    private readonly ILogger<ScriptValidator> _logger;

    public ScriptValidator(ILogger<ScriptValidator> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public bool ValidateScript(ScriptVersion scriptVersion)
    {
        ValidateSystemCommands(scriptVersion);
        ValidateSyntax(scriptVersion);
        return true;
    }

    /// <summary>
    /// Проверка скрипта на наличие системных команд.
    /// </summary>
    /// <param name="scriptVersion">Lua-скрипт.</param>
    /// <returns><see langword="true"/>, если системные команды отсутствуют, иначе <see langword="false"/>.</returns>
    private bool ValidateSystemCommands(ScriptVersion scriptVersion)
    {
        foreach (var systemWord in SystemWords)
        {
            if (scriptVersion.Code.Contains(systemWord))
            {
                _logger.LogWarning($"{scriptVersion.ProcessScript.Name}: Скрипт содержит системные команды.");
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Проверка скрипта на корректность синтаксиса.
    /// </summary>
    /// <param name="scriptVersion">Lua-скрипт.</param>
    /// <returns><see langword="true"/>, если синтаксис скрипта корректный, иначе <see langword="false"/>.</returns>
    private bool ValidateSyntax(ScriptVersion scriptVersion)
    {
        // validate

        return true;
    }
}
