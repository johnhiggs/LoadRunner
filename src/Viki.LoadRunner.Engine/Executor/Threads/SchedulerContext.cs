using System;
using Viki.LoadRunner.Engine.Executor.Context;
using Viki.LoadRunner.Engine.Executor.Timer;

namespace Viki.LoadRunner.Engine.Executor.Threads
{
    public class SchedulerContext : ISchedulerContext
    {
        public SchedulerContext(IThreadPoolStats threadPool, ITimer timer, IIterationMetadata<object> iteration)
        {
            if (threadPool == null)
                throw new ArgumentNullException(nameof(threadPool));
            if (timer == null)
                throw new ArgumentNullException(nameof(timer));
            if (iteration == null)
                throw new ArgumentNullException(nameof(iteration));

            ThreadPool = threadPool;
            Timer = timer;
            Iteration = iteration;
        }

        public IThreadPoolStats ThreadPool { get; }
        public ITimer Timer { get; }
        public IIterationMetadata<object> Iteration { get; }
    }
}