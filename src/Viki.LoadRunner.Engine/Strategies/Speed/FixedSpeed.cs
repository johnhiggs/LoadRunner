﻿using System;
using System.Threading;
using Viki.LoadRunner.Engine.Executor.Threads;

namespace Viki.LoadRunner.Engine.Strategies.Speed
{
    public class FixedSpeed : ISpeedStrategy
    {
        protected long ScheduleAheadTicks = TimeSpan.TicksPerSecond;

        private long _delayTicks;
        private long _next;

        public FixedSpeed(double maxIterationsPerSec)
        {
            SetSpeed(maxIterationsPerSec, TimeSpan.Zero);
        }
        
        protected void SetSpeed(double maxIterationsPerSec, TimeSpan timerValue)
        {
            long delayTicks = (long)(TimeSpan.TicksPerSecond / maxIterationsPerSec) + 1;

            SetDelay(delayTicks, timerValue);
        }

        protected void SetDelay(long delayBetweenIterationsTicks, TimeSpan timerValue)
        {
            _delayTicks = delayBetweenIterationsTicks;

            Interlocked.Exchange(ref _next, timerValue.Ticks - _delayTicks);
        }

        public void Next(ISchedulerContext context, ISchedule schedule)
        {
            long timerTicks = context.Timer.Value.Ticks;
            long delta = timerTicks + ScheduleAheadTicks - _next;
            if (delta >= 0)
            {
                long current = Interlocked.Add(ref _next, _delayTicks);
                schedule.ExecuteAt(TimeSpan.FromTicks(current));
            }
            else
            {
                schedule.Idle(TimeSpan.FromTicks(Math.Abs(delta) + TimeSpan.TicksPerMillisecond));
            }
        }

        public void HeartBeat(IThreadPoolContext context)
        {
            // Catch up _next if lagging behind timeline
            long deltaLag = context.Timer.Value.Ticks - _next;
            long threshold = 2 * _delayTicks;
            if (deltaLag > threshold)
            {
                Interlocked.Add(ref _next, deltaLag - _delayTicks);
            }
        }
    }
}