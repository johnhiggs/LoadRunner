﻿namespace Viki.LoadRunner.Engine.Strategies.Speed
{
    // TODO: Rework it using new IThreadingStrategy
    /// <summary>
    /// WiP Strategy, not yet fully finallized.
    /// </summary>
    //public class ClockedListOfSpeed
    //{
    //    private readonly DateTime _initialTime;
    //    private readonly TimeSpan _minSpeed;

    //    public Func<DateTime> ClockSelector = () => DateTime.UtcNow;

        //public ClockedListOfSpeed(double minIterationsPerSec, DateTime initialTime, TimeSpan period, params double[] iterationPerSecValues) 
        //    : base(period, iterationPerSecValues)
        //{
        //    _initialTime = initialTime.ToUniversalTime();

        //    _minSpeed = TimeSpan.FromTicks((long)(OneSecond.Ticks / minIterationsPerSec));
        //}

        //public override TimeSpan GetDelayBetweenIterations(TimeSpan testExecutionTime)
        //{
        //    TimeSpan fakeExecutionTime = ClockSelector() - _initialTime;

        //    if (fakeExecutionTime > TimeSpan.Zero)
        //        return base.GetIndex(fakeExecutionTime);
        //    else
        //        return _minSpeed;

        //}
    //}
}