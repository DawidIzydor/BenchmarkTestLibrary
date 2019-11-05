using System;

namespace Library
{
    public class DataCreator
    {
        public int[] GetNInts(int N)
        {
            int[] data = new int[N];
            Random rnd = new Random(42);

            for (int i = 0; i < N; i++)
            {
                data[i] = rnd.Next(0, 10);
            }

            return data;
        }
    }
}