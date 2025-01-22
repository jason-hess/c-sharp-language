using BenchmarkDotNet.Running;
using CSharpLang130_Performance;

var _ = BenchmarkRunner.Run<SystemThreadingLockPerformance>();
