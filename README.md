# Benchmark.NET example with tests

This is an example Benchmark.NET library with xUnit tests implemented.

## Summary

The fastest implementation is SearchFirstAndLastIndex3. It searches for the first index of each number and then last index until reaches a certain point (it does not look for all last indexes before calculating distance like SearchFirstAndLastIndex and SearchFirstAndLastIndex2, see more below). On a test data in run in about O(3/4 N) (linear regression from the results provided a 0.778174x + 26.2857 equation). 

Next is SearchFirstAndLastIndex for large arrays and FindWithArray2 for the smallest arrays. It searches for the first and last index of each number and then finds the largest distance.

Next is FindWithArray. The difference between small and large arrays in FindWithArray and FindWithArray2 is caused by comparing of maximum distance - in the FindWithArray method it happens only at the end at the cost of using a little bit more memory. FindWithArray2 needs to compare in each loop iteration thus being a little bit slower on bigger arrays but takes less  memory.

Next are the Dictionary ones. They have a big advantage on FindWithArray - they can run on unknown data (FindWithArray and FindWithArray2 needs to know how much different values can ve expect). Due to using Dictionaries in which searching takes O(log(n)) time they are faster than using lists which have O(n) search time but of course slower than FindWithArray where the search time is O(1).

The slowest implementations are the Base ones, as expected.

The use of LINQ makes the implementations a little bit slower.

# FindDistance

Finds the largest distance between two equal numbers in a sequence. For example for {1, 2, 1, 1  3, 2, 2, 1, 3} the answer is 6 (betwen the first and last 1).

## O(n^2) implementations

### BaseFind 

Most basic two loops implementation, easiest to implement, longest to run. Each loop goes for the whole array, a new distance is calculated for two equal numbers.

```csharp
public int BaseFind(int[] data)
{
    int distance = 0;

    for (int i = 0; i < data.Length; ++i)
    {
        for (int j = 0; j < data.Length; ++j)
        {
            if (data[i] == data[j] && i != j)
            {
                int newDistance = Math.Abs(i - j);
                if (newDistance > distance)
                {
                    distance = newDistance;
                }
            }
        }
    }

    return distance;
}
```

### BaseDividedFind

A little bit quicker version that starts the base one. The inner loop starts from i+1 element instead of starting from 0.

## O(n) implementations

### FindWithDict

Implementation that uses generic dictionary with tuple. If a number is found for the first time it adds a tuple with the index it was found. The next occurences of the same number calculates the distance between first index and current index. Then a maximum distance value is found from the dictionary.
```csharp
public int FindWithDict(int[] data)
{
    int distance = 0;

    // (number, (distance, firstindex))
    Dictionary<int, (int, int)> distanceDictionary = new Dictionary<int, (int, int)>();
    for (int i = 0; i < data.Length; i++)
    {
        if (distanceDictionary.ContainsKey(data[i]))
        {
            distanceDictionary[data[i]] = (i - distanceDictionary[data[i]].Item2,
                distanceDictionary[data[i]].Item2);
        }
        else
        {
            distanceDictionary.Add(data[i], (0, i));
        }
    }

    foreach (KeyValuePair<int, (int, int)> distanceEntry in distanceDictionary)
    {
        if (distanceEntry.Value.Item1 > distance)
        {
            distance = distanceEntry.Value.Item1;
        }
    }

    return distance;
}
```

### FindWithDictAndLinq

Similar to the previous one but instead of searching maximum value with foreach LINQ is used

### FindWithDictAndClass

Similar to FindWithDict but uses a class instead of a tuple.
```csharp
private class DistanceStruct
{
    public int Distance { get; set; }
    public int FirstIndex { get; set; }
}

public int FindWithDictAndClass(int[] data)
{
    int distance = 0;

    // (number, (distance, firstindex))
    Dictionary<int, DistanceStruct> distanceDictionary = new Dictionary<int, DistanceStruct>();
    for (int i = 0; i < data.Length; i++)
    {
        if (distanceDictionary.ContainsKey(data[i]))
        {
            distanceDictionary[data[i]].Distance = i - distanceDictionary[data[i]].FirstIndex;
        }
        else
        {
            distanceDictionary.Add(data[i], new DistanceStruct {FirstIndex = i});
        }
    }

    foreach (KeyValuePair<int, DistanceStruct> distanceEntry in distanceDictionary)
    {
        if (distanceEntry.Value.Distance > distance)
        {
            distance = distanceEntry.Value.Distance;
        }
    }

    return distance;
}
```

### FindWithDictClassAndLinq

Similar to FindWithDictAndLinq but uses a class instead of a tuple.

### FindWithArray

Uses two arrays and stores first index and distances for numbers in seperate arrays. Number of different numbers must be known to use this approach. Fastest on large arrays.
```csharp
public int FindWithArray(int[] data)
{
    int[] distances = new int[11];
    int[] firstIndexes = new int[11];

    for (int i = 0; i < firstIndexes.Length; i++)
    {
        firstIndexes[i] = -1;
    }

    for (int i = 0; i < data.Length; ++i)
    {
        if (firstIndexes[data[i]] == -1)
        {
            firstIndexes[data[i]] = i;
        }
        else
        {
            distances[data[i]] = i - firstIndexes[data[i]];
        }
    }

    return distances.Max();
}
```

### FindWithArray2

Simiar to FindWithArray but instead of storing distances for each number stores only the maximum distance. Fastest on smallest arrays.
```csharp
public int FindWithArray2(int[] data)
{
    int maxDist = 0;
    int[] firstIndexes = new int[11];

    for (int i = 0; i < firstIndexes.Length; i++)
    {
        firstIndexes[i] = -1;
    }

    for (int i = 0; i < data.Length; ++i)
    {
        if (firstIndexes[data[i]] == -1)
        {
            firstIndexes[data[i]] = i;
        }
        else
        {
            var dist = i - firstIndexes[data[i]];
            if (dist > maxDist)
            {
                maxDist = dist;
            }
        }
    }

    return maxDist;
}
```

### SearchFirstAndLastIndex

Searches for the first and last index for each number and then compares distances between them

### SearchFirstAndLastIndex2

A little bit faster version of SearchFirstAndLastIndex - it searches for first and last index in the same loop

```csharp
public int SearchFirstAndLastIndex2(int[] data)
{
    int[] firstIndexes = new int[11];
    int[] lastIndexes = new int[11];
    for (int i = 0; i < firstIndexes.Length; i++)
    {
        firstIndexes[i] = -1;
        lastIndexes[i] = -1;
    }

    int firstIndexesToFind = 11;
    int lastIndexesToFind = 11;

    for (int i = 0; i < data.Length; ++i)
    {
        if (firstIndexesToFind > 0)
        {
            if (firstIndexes[data[i]] == -1)
            {
                firstIndexes[data[i]] = i;
                firstIndexesToFind--;
            }
        }

        if (lastIndexesToFind > 0)
        {
            if (lastIndexes[data[data.Length - 1 - i]] == -1)
            {
                lastIndexes[data[data.Length - 1 - i]] = data.Length - 1 - i;
                lastIndexesToFind--;
            }
        }

        if (firstIndexesToFind <= 0 && lastIndexesToFind <= 0)
        {
            break;
        }
    }

    int maxDist = 0;
    for (int i = 0; i < firstIndexes.Length; ++i)
    {
        int dist = lastIndexes[i] - firstIndexes[i];
        if (dist > maxDist)
        {
            maxDist = dist;
        }
    }

    return maxDist;
}
```

### SearchFirstAndLastIndex3
Similar to SearchFirstAndLastIndex and SearchFirstAndLastIndex2 but does not look for all last indexes before calculating distance.

```csharp
public int SearchFirstAndLastIndex3(int[] data)
{
    int[] firstIndexes = new int[11];
    for (int i = 0; i < firstIndexes.Length; i++)
    {
        firstIndexes[i] = -1;
    }

    int indexesToFind = 11;

    for (int i = 0; i < data.Length; ++i)
    {
        if (firstIndexes[data[i]] == -1)
        {
            firstIndexes[data[i]] = i;
            indexesToFind--;
            if (indexesToFind <= 0)
            {
                break;
            }
        }
    }

    int maxDistance = 0;

    for (int i = data.Length-1; i > maxDistance; --i)
    {
        int dist = i - firstIndexes[data[i]];
        if (dist > maxDistance)
        {
            maxDistance = dist;
        }
    }

    return maxDistance;
}
```

## FindDistances - results

Example runs results using Benchmark.NET on a 16 GB 3200 MHz AMD Ryzen 5 2600 machine.

### N = 100, N = 10000
All methods are tested

|                   Method |     N |              Mean |          Error |         StdDev |            Median |
|------------------------- |------ |------------------:|---------------:|---------------:|------------------:|
|                     Base |    10 |         135.63 ns |       1.105 ns |       0.979 ns |         135.12 ns |
|              BaseDivided |    10 |          50.22 ns |       1.037 ns |       2.907 ns |          50.89 ns |
|             FindWithDict |    10 |         398.44 ns |       1.293 ns |       1.147 ns |         398.09 ns |
|      FindWithDictAndLinq |    10 |         510.46 ns |       2.425 ns |       2.268 ns |         510.50 ns |
|     FindWithDictAndClass |    10 |         337.20 ns |       2.626 ns |       2.456 ns |         336.47 ns |
| FindWithDictClassAndLinq |    10 |         512.34 ns |       3.340 ns |       3.124 ns |         512.36 ns |
|            FindWithArray |    10 |         100.13 ns |       0.579 ns |       0.513 ns |         100.03 ns |
|           FindWithArray2 |    10 |          26.05 ns |       0.532 ns |       0.570 ns |          25.90 ns |
|      FindWithArrayNoLINQ |    10 |          38.39 ns |       1.112 ns |       3.280 ns |          36.78 ns |
|  SearchFirstAndLastIndex |    10 |          46.29 ns |       0.435 ns |       0.406 ns |          46.35 ns |
| SearchFirstAndLastIndex2 |    10 |          54.81 ns |       0.904 ns |       0.801 ns |          54.44 ns |
| SearchFirstAndLastIndex3 |    10 |          25.73 ns |       0.108 ns |       0.101 ns |          25.74 ns |
|                     Base |   100 |      13,899.35 ns |      76.602 ns |      63.966 ns |      13,896.10 ns |
|              BaseDivided |   100 |       6,414.24 ns |      15.427 ns |      13.676 ns |       6,412.38 ns |
|             FindWithDict |   100 |       2,822.55 ns |      14.248 ns |      12.630 ns |       2,823.48 ns |
|      FindWithDictAndLinq |   100 |       2,951.01 ns |      15.145 ns |      14.167 ns |       2,945.00 ns |
|     FindWithDictAndClass |   100 |       1,984.49 ns |      15.905 ns |      14.099 ns |       1,982.69 ns |
| FindWithDictClassAndLinq |   100 |       2,706.81 ns |      16.756 ns |      14.854 ns |       2,704.69 ns |
|            FindWithArray |   100 |         276.54 ns |       5.524 ns |      12.693 ns |         277.74 ns |
|           FindWithArray2 |   100 |         135.14 ns |       0.892 ns |       0.834 ns |         134.88 ns |
|      FindWithArrayNoLINQ |   100 |         244.89 ns |       4.897 ns |      13.155 ns |         249.42 ns |
|  SearchFirstAndLastIndex |   100 |         226.69 ns |       2.390 ns |       2.236 ns |         226.90 ns |
| SearchFirstAndLastIndex2 |   100 |         239.34 ns |       3.204 ns |       2.997 ns |         238.70 ns |
| SearchFirstAndLastIndex3 |   100 |         126.42 ns |       0.875 ns |       0.818 ns |         126.72 ns |
|                     Base | 10000 | 131,054,505.00 ns | 670,052.729 ns | 626,767.739 ns | 130,983,525.00 ns |
|              BaseDivided | 10000 |  61,527,713.68 ns |  71,042.465 ns |  59,323.676 ns |  61,498,833.33 ns |
|             FindWithDict | 10000 |     250,590.46 ns |     754.727 ns |     705.972 ns |     250,339.45 ns |
|      FindWithDictAndLinq | 10000 |     249,421.51 ns |   1,691.051 ns |   1,581.810 ns |     249,170.21 ns |
|     FindWithDictAndClass | 10000 |     161,527.06 ns |     935.226 ns |     874.811 ns |     160,965.01 ns |
| FindWithDictClassAndLinq | 10000 |     163,146.77 ns |   1,907.204 ns |   1,784.000 ns |     162,619.92 ns |
|            FindWithArray | 10000 |      21,728.67 ns |     370.657 ns |     346.713 ns |      21,865.56 ns |
|           FindWithArray2 | 10000 |      33,209.87 ns |      75.256 ns |      62.843 ns |      33,183.92 ns |
|      FindWithArrayNoLINQ | 10000 |      22,284.22 ns |     427.739 ns |     525.302 ns |      22,309.45 ns |
|  SearchFirstAndLastIndex | 10000 |      20,754.98 ns |      51.310 ns |      47.995 ns |      20,738.47 ns |
| SearchFirstAndLastIndex2 | 10000 |      18,149.69 ns |      22.794 ns |      20.207 ns |      18,147.49 ns |
| SearchFirstAndLastIndex3 | 10000 |       7,793.60 ns |      10.939 ns |       9.134 ns |       7,790.20 ns |

### N = 1000000
All methods but Base and BaseDivided due to times being too long (Base would take about 24 minutes).

|                   Method |       N |        Mean |     Error |    StdDev |
|------------------------- |-------- |------------:|----------:|----------:|
|             FindWithDict | 1000000 | 24,845.5 us | 115.08 us | 102.01 us |
|      FindWithDictAndLinq | 1000000 | 24,847.6 us | 133.73 us | 125.09 us |
|     FindWithDictAndClass | 1000000 | 16,201.4 us | 244.09 us | 228.32 us |
| FindWithDictClassAndLinq | 1000000 | 16,498.3 us | 152.33 us | 142.49 us |
|            FindWithArray | 1000000 |  2,184.1 us |   6.22 us |   5.81 us |
|           FindWithArray2 | 1000000 |  3,653.7 us |  32.93 us |  29.19 us |
|      FindWithArrayNoLINQ | 1000000 |  2,265.3 us |  13.13 us |  12.28 us |
|  SearchFirstAndLastIndex | 1000000 |  1,553.3 us |   5.06 us |   4.73 us |
| SearchFirstAndLastIndex2 | 1000000 |  1,811.1 us |   4.46 us |   4.17 us |
| SearchFirstAndLastIndex3 | 1000000 |    778.2 us |   3.24 us |   2.71 us |
