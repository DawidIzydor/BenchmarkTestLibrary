# Benchmark.NET example with tests

This is an example Benchmark.NET library with xUnit tests implemented.

## Summary

The fastest implementation is SearchFirstAndLastIndex for large arrays and FindWithArray2 for the smallest arrays. It searches for the first and last index of each number and then finds the largest distance. On an evenly random distributed array of data it should run in O(n/4)

Next is FindWithArray. The difference between small and large arrays in FindWithArray and FindWithArray2 is caused by comparing of maximum distance - in the FindWithArray method it happens only at the end at the cost of using a little bit more memory. FindWithArray2 needs to compare in each loop iteration thus being a little bit slower on bigger arrays but takes less  memory.

Next are the Dictionary ones. They have a big advantage on FindWithArray - they can run on unknown data (FindWithArray and FindWithArray2 needs to know how much different values can ve expect). Due to using Dictionaries in which searching takes O(log(n)) time they are faster than using lists which have O(n) search time but of course slower than FindWithArray where the search time is O(1).

The slowest implementations are the Base ones, as expected.

The use of LINQ makes the implementations a little bit slower.

# FindDistance

Finds the largest distance between two equal numbers in a sequence. For example for {1, 2, 1, 1  3, 2, 2, 1, 3} the answer is 6 (betwen the first and last 1).

## O(n^2) implementations

### BaseFind 

Most basic two loops implementation, easiest to implement, longest to run. Each loop goes for the whole array, a new distance is calculated for two equal numbers.

```
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
```
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
```
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
```
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
```
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

```
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

## FindDistances - results

Example runs results using Benchmark.NET on a 16 GB 3200 MHz AMD Ryzen 5 2600 machine.

### N = 100, N = 10000
All methods are tested

|                   Method |     N |              Mean |          Error |         StdDev |
|------------------------- |------ |------------------:|---------------:|---------------:|
|                     Base |    10 |         113.70 ns |       0.737 ns |       0.689 ns |
|              BaseDivided |    10 |          44.74 ns |       0.892 ns |       0.835 ns |
|             FindWithDict |    10 |         398.63 ns |       1.940 ns |       1.814 ns |
|      FindWithDictAndLinq |    10 |         500.39 ns |       1.165 ns |       1.032 ns |
|     FindWithDictAndClass |    10 |         339.03 ns |       2.318 ns |       2.168 ns |
| FindWithDictClassAndLinq |    10 |         529.00 ns |       1.899 ns |       1.683 ns |
|            FindWithArray |    10 |          98.94 ns |       0.531 ns |       0.497 ns |
|           FindWithArray2 |    10 |          29.26 ns |       0.584 ns |       0.673 ns |
|      FindWithArrayNoLINQ |    10 |          41.76 ns |       0.231 ns |       0.216 ns |
|  SearchFirstAndLastIndex |    10 |          51.42 ns |       0.706 ns |       0.626 ns |
| SearchFirstAndLastIndex2 |    10 |          46.09 ns |       0.407 ns |       0.381 ns |
|                     Base |   100 |      14,464.64 ns |      28.106 ns |      26.291 ns |
|              BaseDivided |   100 |       6,409.76 ns |      16.757 ns |      15.675 ns |
|             FindWithDict |   100 |       2,822.54 ns |      11.163 ns |      10.442 ns |
|      FindWithDictAndLinq |   100 |       2,993.64 ns |      38.974 ns |      34.550 ns |
|     FindWithDictAndClass |   100 |       1,962.76 ns |       5.723 ns |       5.074 ns |
| FindWithDictClassAndLinq |   100 |       2,250.63 ns |      18.054 ns |      16.888 ns |
|            FindWithArray |   100 |         311.22 ns |       3.220 ns |       3.012 ns |
|           FindWithArray2 |   100 |         164.24 ns |       1.666 ns |       1.558 ns |
|      FindWithArrayNoLINQ |   100 |         258.22 ns |       1.020 ns |       0.954 ns |
|  SearchFirstAndLastIndex |   100 |         238.34 ns |       1.096 ns |       1.026 ns |
| SearchFirstAndLastIndex2 |   100 |         237.16 ns |       0.669 ns |       0.626 ns |
|                     Base | 10000 | 128,430,587.50 ns | 316,450.742 ns | 280,525.340 ns |
|              BaseDivided | 10000 |  72,487,206.67 ns | 173,942.792 ns | 162,706.196 ns |
|             FindWithDict | 10000 |     272,272.90 ns |   1,477.749 ns |   1,382.287 ns |
|      FindWithDictAndLinq | 10000 |     250,048.18 ns |   1,046.047 ns |     927.293 ns |
|     FindWithDictAndClass | 10000 |     162,731.14 ns |   2,334.318 ns |   2,183.522 ns |
| FindWithDictClassAndLinq | 10000 |     162,408.00 ns |     849.677 ns |     794.788 ns |
|            FindWithArray | 10000 |      21,864.78 ns |     158.527 ns |     148.286 ns |
|           FindWithArray2 | 10000 |      34,964.93 ns |     117.787 ns |     110.178 ns |
|      FindWithArrayNoLINQ | 10000 |      22,610.18 ns |      78.407 ns |      73.342 ns |
|  SearchFirstAndLastIndex | 10000 |      18,239.36 ns |     120.305 ns |     112.533 ns |
| SearchFirstAndLastIndex2 | 10000 |      18,209.20 ns |      64.315 ns |      60.160 ns |

### N = 1000000
All methods but Base and BaseDivided due to times being too long (Base would take about 24 minutes). Take note results are in miliseconds not nanoseconds like before. 
|                   Method |       N |      Mean |     Error |    StdDev |
|------------------------- |-------- |----------:|----------:|----------:|
|             FindWithDict | 1000000 | 24.816 ms | 0.1010 ms | 0.0789 ms |
|      FindWithDictAndLinq | 1000000 | 24.701 ms | 0.0752 ms | 0.0667 ms |
|     FindWithDictAndClass | 1000000 | 16.090 ms | 0.1058 ms | 0.0990 ms |
| FindWithDictClassAndLinq | 1000000 | 16.288 ms | 0.1233 ms | 0.1093 ms |
|            FindWithArray | 1000000 |  2.182 ms | 0.0066 ms | 0.0062 ms |
|           FindWithArray2 | 1000000 |  3.485 ms | 0.0139 ms | 0.0130 ms |
|      FindWithArrayNoLINQ | 1000000 |  2.252 ms | 0.0100 ms | 0.0094 ms |
|  SearchFirstAndLastIndex | 1000000 |  2.068 ms | 0.0101 ms | 0.0095 ms |
| SearchFirstAndLastIndex2 | 1000000 |  2.075 ms | 0.0074 ms | 0.0066 ms |
