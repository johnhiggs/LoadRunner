﻿using System.Threading;

namespace Viki.LoadRunner.Engine.Executor.Threads
{
    public class IdFactory : IUniqueIdFactory<int>
    {
        private int _id;

        public int Next()
        {
            int result = Interlocked.Increment(ref _id);

            return result;
        }
    }
}