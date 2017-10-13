﻿using Viki.LoadRunner.Engine.Executor.Context;
using Viki.LoadRunner.Engine.Executor.Context.Interfaces;
using Viki.LoadRunner.Engine.Executor.Threads.Interfaces;

#pragma warning disable 1591

namespace Viki.LoadRunner.Engine.Executor.Threads
{
    public class IterationSmthState : IIterationSmthState
    {
        public IIterationId Iteration { get; set; }
        public IThreadPoolStats ThreadPool { get; set; }
    }
}