using System;

namespace LoadBalancing
{
    internal static class ArrayExtensions
    {
        public static T[] Copy<T>(this T[] arr)
        {
            if ((arr?.Length ?? 0) == 0) return Array.Empty<T>();

            var res = new T[arr.Length];
            Array.Copy(arr, res, arr.Length);
            return arr;
        }
        
        public static T[] Shuffle<T>(this T[] arr)
        {
            if ((arr?.Length ?? 0) < 2) return arr;
            
            var r = new Random();
            for (var i = arr.Length - 1; i > 0; i--)
            {
                var j = r.Next(0, i + 1);

                var temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }

            return arr;
        }
    }
}