﻿using System;
using Viki.LoadRunner.Engine.Aggregators.Interfaces;
using Viki.LoadRunner.Engine.Executor.Collector;
using Viki.LoadRunner.Engine.Executor.Collector.Interfaces;
using Viki.LoadRunner.Engine.Executor.Counter.Interfaces;
using Viki.LoadRunner.Engine.Executor.Factory.Interfaces;
using Viki.LoadRunner.Engine.Executor.Scenario.Interfaces;
using Viki.LoadRunner.Engine.Executor.Scheduler;
using Viki.LoadRunner.Engine.Executor.Scheduler.Interfaces;
using Viki.LoadRunner.Engine.Executor.State;
using Viki.LoadRunner.Engine.Executor.State.Interfaces;
using Viki.LoadRunner.Engine.Executor.Timer.Interfaces;
using Viki.LoadRunner.Engine.Executor.Worker;
using Viki.LoadRunner.Engine.Executor.Worker.Interfaces;
using Viki.LoadRunner.Engine.Strategies.Interfaces;

namespace Viki.LoadRunner.Engine.Executor.Factory
{
    // TODO: Split into smaller factories

    public class ScenarioThreadFactory : IWorkerThreadFactory
    {
        private readonly Type _scenarioType;
        private readonly ITimer _timer;
        private readonly ISpeedStrategy _speedStrategy;
        private readonly IThreadPoolCounter _counter;
        private readonly IResultsAggregator _aggregator;

        private readonly IterationContextFactory _iterationContextFactory;
        private readonly IUniqueIdFactory<int> _globalIdFactory;
        private readonly IErrorHandler _errorHandler;
        private readonly IPrewait _prewait;

        public ScenarioThreadFactory(Type scenarioType, ITimer timer, ISpeedStrategy speedStrategy, IThreadPoolCounter counter, object initialUserData, IResultsAggregator aggregator, IUniqueIdFactory<int> globalIdFactory, IErrorHandler errorHandler)
        {
            if (scenarioType == null)
                throw new ArgumentNullException(nameof(scenarioType));
            if (timer == null)
                throw new ArgumentNullException(nameof(timer));
            if (speedStrategy == null)
                throw new ArgumentNullException(nameof(speedStrategy));
            if (counter == null)
                throw new ArgumentNullException(nameof(counter));
            if (aggregator == null)
                throw new ArgumentNullException(nameof(aggregator));
            if (globalIdFactory == null)
                throw new ArgumentNullException(nameof(globalIdFactory));
            if (errorHandler == null)
                throw new ArgumentNullException(nameof(errorHandler));

            _scenarioType = scenarioType;
            _timer = timer;
            _speedStrategy = speedStrategy;
            _counter = counter;
            _aggregator = aggregator;
            _errorHandler = errorHandler;

            _iterationContextFactory = new IterationContextFactory(_timer, initialUserData);
            _globalIdFactory = globalIdFactory;
            
            _prewait = new TimerBasedPrewait(timer);
        }

        public IWorkerThread Create()
        {
            // Scenario handler
            IScenario scenarioInstance = (IScenario)Activator.CreateInstance(_scenarioType);
            IIterationControl iteration = _iterationContextFactory.Create();

            IScenarioHandler scenarioHandler = new ScenarioHandler(_globalIdFactory, scenarioInstance, iteration);


            // Scheduler for ISpeedStrategy
            IIterationState iterationState = new IterationState(_timer, iteration, _counter);
            SpeedStrategyHandler strategyHandler = new SpeedStrategyHandler(_speedStrategy, iterationState);

            IScheduler scheduler = new Scheduler.Scheduler(strategyHandler, _counter, _timer);


            // Data collector for results aggregation
            IDataCollector collector = new DataCollector(iteration, _aggregator, _counter);


            // Put it all together to create IWork.
            IWork scenarioWork = new ScenarioWork(scheduler, scenarioHandler, collector);



            IWorkerThread thread = new WorkerThread(scenarioWork, _prewait);
            _errorHandler.Register(thread);

            return thread;
        }
    }

    // TODO: This needs to go up to the ILoadRunnerSettings
}