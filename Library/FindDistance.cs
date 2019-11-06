using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class FindDistance
    {
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

        public int BaseDividedFind(int[] data)
        {
            int distance = 0;

            for (int i = 0; i < data.Length; ++i)
            {
                for (int j = i + 1; j < data.Length; ++j)
                {
                    if (data[i] == data[j])
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

        public int FindWithDictAndLinq(int[] data)
        {
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

            return distanceDictionary.Select(distanceEntry => distanceEntry.Value.Item1).Max();
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

        public int FindWithDictClassAndLinq(int[] data)
        {
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

            return distanceDictionary.Select(distanceEntry => distanceEntry.Value.Distance).Max();
        }

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
                    int dist = i - firstIndexes[data[i]];
                    if (dist > maxDist)
                    {
                        maxDist = dist;
                    }
                }
            }

            return maxDist;
        }

        public int FindWithArrayNoLINQ(int[] data)
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

            int maxDist = 0;
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] > maxDist)
                {
                    maxDist = distances[i];
                }
            }

            return maxDist;
        }

        public int SearchFirstAndLastIndex(int[] data)
        {
            int[] firstIndexes = new int[11];
            int[] lastIndexes = new int[11];
            for (int i = 0; i < firstIndexes.Length; i++)
            {
                firstIndexes[i] = -1;
                lastIndexes[i] = -1;
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

            indexesToFind = 11;

            for (int i = 0; i < data.Length; ++i)
            {
                if (lastIndexes[data[data.Length - 1 - i]] == -1)
                {
                    lastIndexes[data[data.Length - 1 - i]] = data.Length - 1 - i;
                    indexesToFind--;
                    if (indexesToFind <= 0)
                    {
                        break;
                    }
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


        private class DistanceStruct
        {
            public int Distance { get; set; }
            public int FirstIndex { get; set; }
        }
    }
}