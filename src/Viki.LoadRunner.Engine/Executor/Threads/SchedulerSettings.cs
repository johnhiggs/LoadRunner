using Viki.LoadRunner.Engine.Executor.Context;
using Viki.LoadRunner.Engine.Strategies;

namespace Viki.LoadRunner.Engine.Executor.Threads
{
    public class SchedulerSettings
    {
        public ISpeedStrategy Speed { get; set; }
        public IThreadPoolCounter Counter { get; set; }
        
        //public IIterationMetadata<object> { get }
    }
}