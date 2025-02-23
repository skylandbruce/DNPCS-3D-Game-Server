namespace UtilityDLL.UNIQUE;

public sealed class UniqueLongGenerator
{
    private static readonly Lazy<UniqueLongGenerator> lazy =
        new Lazy<UniqueLongGenerator>(() => new UniqueLongGenerator());

    public static UniqueLongGenerator Instance { get { return lazy.Value; } }

    private long _currentValue;
    private readonly object _lock = new object();

    private UniqueLongGenerator()
    {
        _currentValue = 0;
    }

    public long GetNextUniqueLong()
    {
        lock (_lock)
        {
            _currentValue++;
            return _currentValue;
        }
    }
}