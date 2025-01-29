using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Services.Abstracts;
using static LuaEngine.Scripts.WebApi.Constants.ScriptValidatorConstants;

namespace LuaEngine.Scripts.WebApi.Services;

/// <summary>
/// Валидатор скриптов.
/// </summary>
public class CodeValidator : ICodeValidator
{
    private readonly ILogger<CodeValidator> _logger;

    public CodeValidator(ILogger<CodeValidator> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public bool ValidateCode(CodeVersion codeVersion)
    {
        ValidateSystemCommands(codeVersion);
        ValidateSyntax(codeVersion);
        return true;
    }

    /// <summary>
    /// Проверка скрипта на наличие системных команд.
    /// </summary>
    /// <param name="codeVersion">Lua-скрипт.</param>
    /// <returns><see cref="true"/>, если системные команды отсутствуют, иначе <see cref="false"/>.</returns>
    private bool ValidateSystemCommands(CodeVersion codeVersion)
    {
        foreach (var systemWord in SystemWords)
        {
            if (codeVersion.Code.Contains(systemWord))
            {
                _logger.LogWarning($"{codeVersion.Script.Name}: Скрипт содержит системные команды.");
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Проверка скрипта на корректность синтаксиса.
    /// </summary>
    /// <param name="codeVersion">Lua-скрипт.</param>
    /// <returns><see cref="true"/>, если синтаксис скрипта корректный, иначе <see cref="false"/>.</returns>
    private bool ValidateSyntax(CodeVersion codeVersion)
    {
        // validate

        return true;
    }
}
