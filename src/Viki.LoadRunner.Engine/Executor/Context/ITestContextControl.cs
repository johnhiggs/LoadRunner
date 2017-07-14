using System;

namespace Viki.LoadRunner.Engine.Executor.Context
{
    public interface ITestContextControl : ITestContext
    {
        void Start();
        void Stop();
        void Reset(int threadIterationId, int globalIterationId);
        void SetError(Exception ex);
    }
}