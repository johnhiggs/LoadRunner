### Generic performance testing library for executing load-tests written in .NET c# ###
* NuGet: `Install-Package Viki.LoadRunner`

## *Quick Intro* [for v0.7.*]
* Take a look at [LoadRunner.Demo](/demo) project and follow commented code :)
  - [DemoTestScenario.cs](/demo/DemoTestScenario.cs) - Setup your Load-test scenario for single thread
  - [AggregationSetup.cs](/demo/AggregationSetup.cs) - Setup what data will be saved/meassured
  - [ParametersSetup.cs](/demo/ParametersSetup.cs) - Setup execution parameters (threads, duration, speed, etc...)
  - [QuickIntroLoadTest.cs](/demo/QuickIntroLoadTest.cs) - Put it all together
  - [Program.cs](/demo/Program.cs) - Run it
  - [RawDataAggregationDemo.cs](/demo/RawDataAggregationDemo.cs) - Advanced RnD feature to checkout.
* [DemoResults.xlsx](/demo/DemoResults.xlsx) - import results to excel and do some charting :)
![](https://raw.githubusercontent.com/Vycka/LoadRunner/master/diagrams/Architecture.png)
* [Future](../../wiki/TODOs)

### v0.8 is kinda in RC stage:
 * Apologies for the delay in making v0.8 final, need to solve late-found fundamental issue with scheduler before locking in interfaces. 
https://github.com/Vycka/LoadRunner/tree/v0.8

***Sorry for poor documentation, I don't have much time to work on it ATM, but if having any questions/suggestions/etc, feel free to contact me.***

[![Analytics](https://ga-beacon.appspot.com/UA-71045586-1/LoadRunner/readme?pixel)](https://github.com/Vycka/LoadRunner)
