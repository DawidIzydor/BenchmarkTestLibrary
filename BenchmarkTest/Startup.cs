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
        }
    }
}