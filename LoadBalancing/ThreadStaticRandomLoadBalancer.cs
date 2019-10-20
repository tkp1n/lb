using System;

namespace LoadBalancing
{
    internal static class ThreadStaticRandomLoadBalancer
    {
        [ThreadStatic] private static Random Random;

        public static Connection Select(Connection[] connections)
        {
            var r = Random;
            if (r == null)
            {
                r = new Random();
                Random = r;
            }
            
            var index = r.Next(0, connections.Length);
            return connections[index];
        }
    }
}