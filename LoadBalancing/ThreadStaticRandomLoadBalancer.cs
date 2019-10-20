using System;
using System.Threading;

namespace LoadBalancing
{
    internal static class ThreadStaticRandomLoadBalancer
    {
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random());

        public static Connection Select(Connection[] connections)
        {
            var index = Random.Value.Next(0, connections.Length);
            return connections[index];
        }
    }
}