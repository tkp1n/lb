using System;

namespace LoadBalancing
{
    internal static class SyncRandomLoadBalancer
    {
        private static readonly Random Random = new Random();

        public static Connection Select(Connection[] connections)
        {
            int index;
            lock (Random)
            {
                index = Random.Next(0, connections.Length);
            }
            
            return connections[index];
        }
    }
}