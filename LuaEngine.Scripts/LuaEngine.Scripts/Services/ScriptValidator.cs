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
    public bool ValidateScript(Script script)
    {
        ValidateSystemCommands(script);
        ValidateSyntax(script);
        return true;
    }

    /// <summary>
    /// Проверка скрипта на наличие системных команд.
    /// </summary>
    /// <param name="script">Lua-скрипт.</param>
    /// <returns><see cref="true"/>, если системные команды отсутствуют, иначе <see cref="false"/>.</returns>
    private bool ValidateSystemCommands(Script script)
    {
        foreach (var systemWord in SystemWords)
        {
            if (script.ScriptCode.Contains(systemWord))
            {
                _logger.LogWarning($"{script.Name}: Скрипт содержит системные команды.");
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Проверка скрипта на корректность синтаксиса.
    /// </summary>
    /// <param name="script">Lua-скрипт.</param>
    /// <returns><see cref="true"/>, если синтаксис скрипта корректный, иначе <see cref="false"/>.</returns>
    private bool ValidateSyntax(Script script)
    {
        // validate

        return true;
    }
}
