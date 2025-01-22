using BenchmarkDotNet.Attributes;

namespace CSharpLang130_Performance;

[MemoryDiagnoser]
public class SystemThreadingLockPerformance
{
    readonly object _legacyLock = new();
    readonly Lock _lock = new();
    readonly object _lockLock;

    public SystemThreadingLockPerformance()
    {
        // Disable the warning since we are intending to test performance
        // of converting to a monitor-based lock

#pragma warning disable CS9216 // A value of type 'System.Threading.Lock' converted to a different type will use likely unintended monitor-based locking in 'lock' statement.
        _lockLock = _lock;
#pragma warning restore CS9216 // A value of type 'System.Threading.Lock' converted to a different type will use likely unintended monitor-based locking in 'lock' statement.
    }

    [Benchmark]
    public void LegacyLock()
    {
        lock (_legacyLock)
        {
        }
    }

    [Benchmark]
    public void Lock()
    {
        lock (_lock)
        {
        }
    }

    [Benchmark]
    public void MisuseOfLock()
    {
        lock (_lockLock)
        {
        }
    }
}