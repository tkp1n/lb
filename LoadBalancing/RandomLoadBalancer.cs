using System;
using System.Threading;

namespace LoadBalancing
{
    public static class RandomLoadBalancer
    {
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random());

        public static Connection Select(Connection[] connections)
        {
            var index = Random.Value.Next(0, connections.Length);

            return connections[index];
        }
    }
}