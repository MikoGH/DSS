namespace LuaEngine.Scripts.Tests.Helpers;

public sealed class Result<T>
{
    public static Result<T> Empty = new();
    public static implicit operator Result<T>(T value) => new(value);

    public T Value { get; }
    public bool HasValue { get; }
    public Result(T value)
    {
        Value = value;
        HasValue = value is not null;
    }

    private Result()
    {
        Value = default;
        HasValue = false;
    }
}
