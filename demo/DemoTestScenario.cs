﻿using System;
using System.Diagnostics;
using System.Threading;
using Viki.LoadRunner.Engine;
using Viki.LoadRunner.Engine.Executor.Context;

namespace LoadRunner.Demo
{
    // Extended information
    // https://github.com/Vycka/LoadRunner/wiki/ILoadTestScenario


    // To create scenario, either implement ILoadTestScenario or extend LoadTestBase
    //
    [DebuggerStepThrough] // this flag prevents debugger from catching exceptions and breaking code execution.
    public class DemoTestScenario : ILoadTestScenario
    {
        private static readonly Random Random = new Random(42);

        public void ScenarioSetup(ITestContext testContext)
        {
            //Debug.WriteLine("ScenarioSetup Executes on thread creation");
            //Debug.WriteLine("Exceptions here are not handled and will stop the execution!");

            Console.WriteLine($"Created Thread {testContext.ThreadId}");
        }

        public void IterationSetup(ITestContext testContext)
        {
            //Debug.WriteLine("IterationSetup is executed before each ExecuteScenario call");

            if (Random.Next(100) % 50 == 0)
                throw new Exception("2% error chance for testing");
        }

        public void ExecuteScenario(ITestContext testContext)
        {
            //Debug.WriteLine(
            //    "ExecuteScenario defines single iteration for load test scenario, " +
            //    "It is called after each successful IterationSetup call. " +
            //    "Execution time is measured only for this function" +
            //    "You can use testContext.Checkpoint() function to mark points, " +
            //    "where time should be measured"
            //);

            Thread.Sleep(Random.Next(200));
            Thread.Sleep(Random.Next(800));

            if (Random.Next(100) % 10 == 0)
                throw new Exception("10% error chance for testing");
        }


        public void IterationTearDown(ITestContext testContext)
        {
            //Debug.WriteLine("IterationTearDown is executed each time after ExecuteScenario iteration is finished.");
            //Debug.WriteLine("It is also executed even when IterationSetup or ExecuteScenario fails");

            if (Random.Next(100) % 25 == 0)
                throw new Exception("4% error chance for testing");
        }

        public void ScenarioTearDown(ITestContext testContext)
        {
            //Debug.WriteLine("ScenarioTearDown Executes once LoadTest execution is over");
            //Debug.WriteLine("Exceptions here are not handled and will stop the execution!");
        }
    }
}
