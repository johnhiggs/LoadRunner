﻿using System;
using Viki.LoadRunner.Engine.Aggregators;
using Viki.LoadRunner.Engine.Executor.Context;
using Viki.LoadRunner.Engine.Executor.Result;
using Viki.LoadRunner.Engine.Executor.Timer;

namespace Viki.LoadRunner.Engine.Executor.Threads
{
    public class ScenarioInstance
    {
        private readonly IUniqueIdFactory<int> _idFactory;
        private readonly ILoadTestScenario _scenario;

        public bool Initialized { get; private set; }

        private int _threadIterationId = 0;

        private TestContext _context;
        public ITestContext Context => _context;

        public ScenarioInstance(ITimer timer, IUniqueIdFactory<int> idFactory, Type scenarioType, int threadId, object initialUserData)
        {
            if (timer == null)
                throw new ArgumentNullException(nameof(timer));
            if (idFactory == null)
                throw new ArgumentNullException(nameof(idFactory));
            if (aggregator == null)
                throw new ArgumentNullException(nameof(aggregator));

            _scenario = (ILoadTestScenario) Activator.CreateInstance(scenarioType);
            _context = new TestContext(timer, threadId, initialUserData);
            _idFactory = idFactory;
        }

        public void ExecuteScenarioSetup()
        {
            _context.Reset(-1, -1);
            _scenario.ScenarioSetup(_context);
            Initialized = true;
        }

        /// <summary>
        /// Prepares context for next iteration
        /// </summary>
        public void Prepare()
        {
            _context.Reset(_threadIterationId++, _idFactory.Next());
        }

        public void Execute()
        {
            _context.Checkpoint(Checkpoint.Names.Setup);
            bool setupSuccess = ExecuteWithExceptionHandling(() => _scenario.IterationSetup(_context));

            if (setupSuccess)
            {
                _context.Checkpoint(Checkpoint.Names.IterationStart);

                _context.Start();
                bool iterationSuccess = ExecuteWithExceptionHandling(() => _scenario.ExecuteScenario(_context));
                _context.Stop();

                if (iterationSuccess)
                {
                    _context.Checkpoint(Checkpoint.Names.IterationEnd);
                }
            }
            else
            {
                _context.Start();
                _context.Stop();
            }

            _context.Checkpoint(Checkpoint.Names.TearDown);
            ExecuteWithExceptionHandling(() => _scenario.IterationTearDown(_context));
        }

        private bool ExecuteWithExceptionHandling(Action action)
        {
            bool result = false;

            try
            {
                action.Invoke();
                result = true;
            }
            catch (Exception ex)
            {
                _context.SetError(ex);
            }

            return result;
        }
    }
}