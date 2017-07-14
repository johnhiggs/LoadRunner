﻿using System;
using System.Collections.Generic;
using System.Linq;
using Viki.LoadRunner.Engine.Executor.Timer;

namespace Viki.LoadRunner.Engine.Executor.Context
{
    public class TestContext : ITestContextControl
    {
        private readonly ITimer _timer;

        #region Fields

        private readonly List<Checkpoint> _checkpoints = new List<Checkpoint>();

        public TestContext(ITimer timer, int threadId, object initialUserData)
        {
            if (timer == null)
                throw new ArgumentNullException(nameof(timer));
            
            ThreadId = threadId;
            _timer = timer;
            UserData = initialUserData;

            Reset(-1,-1);
        }

        #endregion

        public IReadOnlyList<Checkpoint> LoggedCheckpoints => _checkpoints;
        public TimeSpan IterationStarted { get; private set; }
        public TimeSpan IterationFinished { get; private set; }

        #region Internal methods

        public void Start()
        {
            IterationStarted = _timer.Value;
        }

        public void Stop()
        {
            IterationFinished = _timer.Value;
        }

        public void Reset(int threadIterationId, int globalIterationId)
        {
            GlobalIterationId = globalIterationId;
            ThreadIterationId = threadIterationId;

            _checkpoints.Clear();

            IterationStarted = TimeSpan.MaxValue;
            IterationFinished = TimeSpan.MinValue;
        }

        public void SetError(Exception error)
        {
            _checkpoints[_checkpoints.Count - 1].Error = error;
        }

        #endregion

        #region ITestContext

        public void Checkpoint(string checkpointName = null)
        {
            if (checkpointName == null)
                checkpointName = $"Checkpoint #{_checkpoints.Count + 1}";

            Checkpoint newCheckpoint = new Checkpoint(checkpointName, IterationElapsedTime);
            _checkpoints.Add(newCheckpoint);  
        }

        public IEnumerable<KeyValuePair<string, Exception>> GetErrors()
        {
            return
                _checkpoints
                    .Where(c => c.Error != null)
                    .Select(c => new KeyValuePair<string, Exception>(c.Name, c.Error));
        }

        public TimeSpan IterationElapsedTime
        {
            get
            {
                if (IterationFinished != TimeSpan.MinValue)
                    return IterationFinished - IterationStarted;
                if (IterationStarted != TimeSpan.MaxValue)
                    return _timer.Value - IterationStarted;

                return TimeSpan.Zero;
            }
        }

        public int GlobalIterationId { get; private set; }
        public int ThreadId { get; }
        public int ThreadIterationId { get; private set; }
        public object UserData { get; set; }

        #endregion
    }
}