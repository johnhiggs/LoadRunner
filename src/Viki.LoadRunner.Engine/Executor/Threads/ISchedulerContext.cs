using Viki.LoadRunner.Engine.Executor.Context;
using Viki.LoadRunner.Engine.Executor.Timer;

namespace Viki.LoadRunner.Engine.Executor.Threads
{
    public interface ISchedulerContext
    {
        IThreadPoolStats ThreadPool { get; }
        ITimer Timer { get; }
        IIterationMetadata<object> Iteration { get; }
    }
}