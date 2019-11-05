# Benchmark.NET example with tests

This is an example Benchmark.NET library with xUnit tests implemented.

## Summary

The fastest implementations are FindWithArray for large arrays and FindWithArray2 for the smallest arrays. This is caused by comparing of maximum distance - in the FindWithArray method it happens only at the end at the cost of using a little bit more memory. FindWithArray2 needs to compare in each loop iteration thus being a little bit slower on bigger arrays but takes less  memory.

The second fastests implementations are the Dictionary ones. They have a big advantage on FindWithArray - they can run on unknown data (FindWithArray and FindWithArray2 needs to know how much different values can ve expect). Due to using Dictionaries in which searching takes O(log(n)) time they are faster than using lists which have O(n) search time but of course slower than FindWithArray where the search time is O(1).

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

## FindDistances - results

Example runs results using Benchmark.NET on a 16 GB 3200 MHz AMD Ryzen 5 2600 machine.

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
