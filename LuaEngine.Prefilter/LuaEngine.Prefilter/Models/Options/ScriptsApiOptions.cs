namespace LuaEngine.Prefilter.Models.Options;

/// <summary>
/// Представляет настройки Refit-клиента, необходимые для выполнения запросов к API Скриптов.
/// </summary>
public class ScriptsApiOptions : IRefitClientOptions
{
    /// <inheritdoc/>
    public string Host { get; set; } = null!;
}
