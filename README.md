# Benchmark.NET example with tests

This is an example library for use Benchmark.NET and xUnit.

## FindDistance

Finds the largest distance between two equal numbers. For example for sequence {1, 2, 3, 2, 2, 1, 3} the answer is 5.

### BaseFind 

Most basic two loops implementation. Easiest to implement, longest to run.

### BaseDividedFind

A little bit quicker version that starts the base one. The inner loop starts from i+1 element instead of from 0.

### FindWithDict

Implementation that uses generic dictionary with tuple. If a number is found for the first time it adds a tuple with the index it was found. The next occurences of the same number calculates the distance between first index and current index. Then a maximum distance value is found from the dictionary. 

### FindWithDictAndLinq

Similar to the previous one but instead of searching maximum value with foreach LINQ is used

### FindWithDictAndClass

Similar to FindWithDict but uses a class instead of a tuple.

### FindWithDictClassAndLinq

Similar to FindWithDictAndLinq but uses a class instead of a tuple.

### FindWithArray

Uses two arrays and stores first index and distances for numbers in seperate arrays. Number of different numbers must be known to use this approach. Fastest on large arrays.

### FindWithArray2

Simiar to FindWithArray but instead of storing distances for each number stores only the maximum distance. Fastest on smallest arrays.

## FindDistances - results

Example runs results using Benchmark.NET on a 16 GB 3200 MHz AMD Radeon 2600 machine.

### N = 100, N = 10000
All methods are tested

|                   Method |     N |             Mean |         Error |        StdDev |
|------------------------- |------ |-----------------:|--------------:|--------------:|
|                     Base |   100 |      16,164.0 ns |      75.54 ns |      70.66 ns |
|              BaseDivided |   100 |       6,445.4 ns |      34.25 ns |      32.03 ns |
|             FindWithDict |   100 |       2,849.7 ns |      14.80 ns |      13.84 ns |
|      FindWithDictAndLinq |   100 |       3,010.2 ns |      15.03 ns |      12.55 ns |
|     FindWithDictAndClass |   100 |       2,017.4 ns |      14.46 ns |      13.52 ns |
| FindWithDictClassAndLinq |   100 |       2,255.4 ns |      18.57 ns |      16.46 ns |
|            FindWithArray |   100 |         321.9 ns |       3.72 ns |       3.48 ns |
|           FindWithArray2 |   100 |         137.6 ns |       1.90 ns |       1.78 ns |
|                     Base | 10000 | 152,361,115.0 ns | 704,283.32 ns | 658,787.06 ns |
|              BaseDivided | 10000 |  61,746,354.8 ns | 193,858.27 ns | 161,880.43 ns |
|             FindWithDict | 10000 |     251,728.2 ns |   3,236.74 ns |   3,027.65 ns |
|      FindWithDictAndLinq | 10000 |     249,859.6 ns |   2,209.15 ns |   2,066.44 ns |
|     FindWithDictAndClass | 10000 |     162,810.3 ns |   1,140.10 ns |   1,010.67 ns |
| FindWithDictClassAndLinq | 10000 |     161,879.4 ns |     814.61 ns |     722.13 ns |
|            FindWithArray | 10000 |      21,802.5 ns |     206.52 ns |     193.18 ns |
|           FindWithArray2 | 10000 |      33,254.6 ns |     170.53 ns |     159.51 ns |

### N = 1000000
All methods but Base and BaseDivided due to times being too long (Base would take about 24 minutes). Take note results are in miliseconds not nanoseconds like before. 

|                   Method |       N |      Mean |     Error |    StdDev |
|------------------------- |-------- |----------:|----------:|----------:|
|             FindWithDict | 1000000 | 24.857 ms | 0.0946 ms | 0.0885 ms |
|      FindWithDictAndLinq | 1000000 | 25.004 ms | 0.1569 ms | 0.1467 ms |
|     FindWithDictAndClass | 1000000 | 16.114 ms | 0.0745 ms | 0.0697 ms |
| FindWithDictClassAndLinq | 1000000 | 16.162 ms | 0.0962 ms | 0.0900 ms |
|            FindWithArray | 1000000 |  2.187 ms | 0.0108 ms | 0.0101 ms |
|           FindWithArray2 | 1000000 |  3.698 ms | 0.0281 ms | 0.0263 ms |
