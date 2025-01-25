namespace LuaEngine.LuaOrchestrator.Models.Options;

/// <summary>
/// Настройки RabbitMQ.
/// </summary>
public class RabbitMqOptions
{
    /// <summary>
    /// Ожидать установления соединения с RabbitMq при старте приложения.
    /// </summary>
    public bool WaitUntilStarted { get; set; }

    /// <summary>
    /// Таймаут при первичном соединении.
    /// </summary>
    public TimeSpan StartTimeout { get; set; }

    /// <summary>
    /// Таймаут при отправки сообщения в очередь.
    /// </summary>
    public TimeSpan RequestedConnectionTimeout { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Хост.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Виртуальный хост.
    /// </summary>
    public string VirtualHost { get; set; }

    /// <summary>
    /// Порт.
    /// </summary>
    public ushort Port { get; set; }
}
