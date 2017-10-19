﻿using System;
using Viki.LoadRunner.Engine.Core.Scheduler.Interfaces;
using Viki.LoadRunner.Engine.Core.State.Interfaces;
using Viki.LoadRunner.Engine.Strategies.Custom.Strategies.Interfaces;

namespace Viki.LoadRunner.Engine.Strategies.Custom.Strategies.Speed
{
    public class LimitWorkingThreads : ISpeedStrategy
    {
        /// <summary>
        /// Count of worker-threads to allow executing iterations
        /// </summary>
        public int WorkingThreads;

        /// <summary>
        /// How much time to wait before thread will try to enqueue for iteration again
        /// </summary>
        public TimeSpan DelayInterval = TimeSpan.FromMilliseconds(100);

        public LimitWorkingThreads(int workingThreads)
        {
            WorkingThreads = workingThreads;
        }

        public void Setup(ITestState state)
        {
        }

        public void Next(IIterationState state, ISchedule scheduler)
        {
            int includeSelf = scheduler.Action == ScheduleAction.Execute ? 1 : 0; 
            int workingThreads = state.ThreadPool.CreatedThreadCount - state.ThreadPool.IdleThreadCount - includeSelf;
            if (workingThreads < WorkingThreads)
            {
                scheduler.Execute();
            }
            else
            {
                scheduler.Idle(DelayInterval);
            }
        }

        public void HeartBeat(ITestState state)
        {
        }
    }
}