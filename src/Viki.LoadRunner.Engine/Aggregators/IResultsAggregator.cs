﻿using System;
using Viki.LoadRunner.Engine.Executor.Result;

namespace Viki.LoadRunner.Engine.Aggregators
{
    /// <summary>
    /// This interface defines handling of raw results received from ongoing test.
    /// </summary>
    /// <remarks>Aggregators are expected to contain their errors.
    /// Thrown exceptions will break test execution.</remarks>
    public interface IResultsAggregator
    {
        /// <summary>
        /// Signals aggregator, that new test execution is about to begin
        /// Aggregator can reset stats here if needed.
        /// </summary>
        void Begin();

        /// <summary>
        /// Results from all running threads will be poured into this one.
        /// </summary>
        void TestContextResultReceived(IResult result);

        /// <summary>
        /// Signals aggregator, that test execion has ended and all meassurements have been delivered.
        /// </summary>
        void End();
    }
}