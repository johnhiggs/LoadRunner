﻿using System;
using Viki.LoadRunner.Engine.Core.Factory.Interfaces;
using Viki.LoadRunner.Engine.Core.Scenario.Interfaces;
using Viki.LoadRunner.Engine.Core.Worker;
using Viki.LoadRunner.Engine.Core.Worker.Interfaces;

namespace Viki.LoadRunner.Engine.Core.Factory
{
    public class ScenarioHandlerFactory : IScenarioHandlerFactory
    {
        private readonly Type _scenarioType;
        private readonly IUniqueIdFactory<int> _globalIdFactory;

        public ScenarioHandlerFactory(Type scenarioType, IUniqueIdFactory<int> globalIdFactory)
        {
            if (scenarioType == null)
                throw new ArgumentNullException(nameof(scenarioType));
            if (globalIdFactory == null)
                throw new ArgumentNullException(nameof(globalIdFactory));

            _scenarioType = scenarioType;
            _globalIdFactory = globalIdFactory;
        }

        public IScenarioHandler Create(IIterationControl iterationContext)
        {
            IScenario scenarioInstance = (IScenario)Activator.CreateInstance(_scenarioType);
            IScenarioHandler scenarioHandler = new ScenarioHandler(_globalIdFactory, scenarioInstance, iterationContext);

            return scenarioHandler;
        }
    }
}