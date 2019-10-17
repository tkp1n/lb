using System;

namespace LoadBalancing
{
    public class FisherYates
    {
        public static void Shuffle<T>(T[] arr)
        {
            var r = new Random();
            for (var i = arr.Length - 1; i > 0; i--)
            {
                var j = r.Next(0, i + 1);

                var temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
    }
}