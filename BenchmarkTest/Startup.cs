using System;
using BenchmarkDotNet.Running;
using BenchmarkTest;
using Library;

namespace BenchmarksLibrary
{
    internal class Startup
    {
        public static void Main()
        {
            BenchmarkRunner.Run<FindDistanceBenchmark>();

            //var data = new DataCreator().GetNInts(10000000);
            //for (int i = 0; i < 100; i++)
            //{
            //    new FindDistance().SearchFirstAndLastIndex2(data);
            //}
        }
    }
}