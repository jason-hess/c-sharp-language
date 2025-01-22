using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var _ = BenchmarkRunner.Run<SystemThreadingLockPerformance>();

[MemoryDiagnoser]
public class SystemThreadingLockPerformance
{
    readonly object _legacyLock = new();
    readonly Lock _lock = new();
    readonly object _lockLock;

    public SystemThreadingLockPerformance()
    {
        _lockLock = _lock;
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