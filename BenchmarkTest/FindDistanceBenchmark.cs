using System;
using BenchmarkDotNet.Attributes;
using Library;

namespace BenchmarkTest
{
    public class FindDistanceBenchmark
    {
        private int[] _data;
        [Params(100000)] public int N { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _data = new int[N];

            Random rand = new Random(42);
            for (int i = 0; i < N; i++)
            {
                _data[i] = rand.Next(0, 10);
            }
        }

        //[Benchmark]
        //public int Base() => new FindDistance().BaseFind(_data);


        //[Benchmark]
        //public int BaseDivided() => new FindDistance().BaseDividedFind(_data);


        [Benchmark]
        public int FindWithDict() => new FindDistance().FindWithDict(_data);

        [Benchmark]
        public int FindWithDictAndLinq() => new FindDistance().FindWithDictAndLinq(_data);

        [Benchmark]
        public int FindWithDictAndClass() => new FindDistance().FindWithDictAndClass(_data);

        [Benchmark]
        public int FindWithDictClassAndLinq() => new FindDistance().FindWithDictClassAndLinq(_data);

        [Benchmark]
        public int FindWithArray() => new FindDistance().FindWithArray(_data);

        [Benchmark]
        public int FindWithArray2() => new FindDistance().FindWithArray2(_data);

        [Benchmark]
        public int FindWithArrayNoLINQ() => new FindDistance().FindWithArrayNoLINQ(_data);


        [Benchmark]
        public int SearchFirstAndLastIndex() => new FindDistance().SearchFirstAndLastIndex(_data);

        [Benchmark]
        public int SearchFirstAndLastIndex2() => new FindDistance().SearchFirstAndLastIndex2(_data);

        [Benchmark]
        public int SearchFirstAndLastIndex3() => new FindDistance().SearchFirstAndLastIndex3(_data);
    }
}