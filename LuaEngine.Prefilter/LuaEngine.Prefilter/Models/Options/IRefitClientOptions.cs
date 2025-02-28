namespace LuaEngine.Prefilter.Models.Options;

/// <summary>
/// Контракт настроек Refit-клиента.
/// </summary>
public interface IRefitClientOptions
{
    /// <summary>
    /// Хост, к которому будет обращаться Refit-клиент.
    /// </summary>
    string Host { get; set; }
}
