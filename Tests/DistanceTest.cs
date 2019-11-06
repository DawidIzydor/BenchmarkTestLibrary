using System;
using Library;
using Xunit;

namespace Tests
{
    public class DistanceTest
    {
        private int[] testData(int N)
        {
            return new DataCreator().GetNInts(N);
        }

        [Fact]
        public void BaseTest()
        {
            int result100 = new FindDistance().BaseFind(testData(100));
            int result10000 = new FindDistance().BaseFind(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void BaseDividedTest()
        {
            int result100 = new FindDistance().BaseDividedFind(testData(100));
            int result10000 = new FindDistance().BaseDividedFind(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void FindWithDictTest()
        {
            int result100 = new FindDistance().FindWithDict(testData(100));
            int result10000 = new FindDistance().FindWithDict(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void FindWithDictAndLinqTest()
        {
            int result100 = new FindDistance().FindWithDictAndLinq(testData(100));
            int result10000 = new FindDistance().FindWithDictAndLinq(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void FindWithDictAndClassTest()
        {
            int result100 = new FindDistance().FindWithDictAndClass(testData(100));
            int result10000 = new FindDistance().FindWithDictAndClass(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void FindWithDictClassAndLinqTest()
        {
            int result100 = new FindDistance().FindWithDictClassAndLinq(testData(100));
            int result10000 = new FindDistance().FindWithDictClassAndLinq(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void FindWithArrayTest()
        {
            int result100 = new FindDistance().FindWithArray(testData(100));
            int result10000 = new FindDistance().FindWithArray(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void FindWithArrayNoLINQTest()
        {
            int result100 = new FindDistance().FindWithArrayNoLINQ(testData(100));
            int result10000 = new FindDistance().FindWithArrayNoLINQ(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void FindWithArray2Test()
        {
            int result100 = new FindDistance().FindWithArray2(testData(100));
            int result10000 = new FindDistance().FindWithArray2(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void SearchFirstAndLastIndexTest()
        {
            int result100 = new FindDistance().SearchFirstAndLastIndex(testData(100));
            int result10000 = new FindDistance().SearchFirstAndLastIndex(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }

        [Fact]
        public void SearchFirstAndLastIndex2Test()
        {
            int result100 = new FindDistance().SearchFirstAndLastIndex2(testData(100));
            int result10000 = new FindDistance().SearchFirstAndLastIndex2(testData(10000));

            Assert.Equal(91, result100);
            Assert.Equal(9992, result10000);
        }
    }
}