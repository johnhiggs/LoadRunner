using Viki.LoadRunner.Engine.Executor.Threads.Interfaces;

namespace Viki.LoadRunner.Engine.Executor.Threads.Factory.Interfaces
{
    public interface IWorkerThreadFactory
    {
        IWorkerThread Create();
    }
}