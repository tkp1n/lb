using System;
using System.Threading;

namespace LoadBalancing
{
    public class ModuloLoadBalancer
    {
        private readonly Connection[] _connections;
        private int _index;

        public ModuloLoadBalancer(Connection[] connections)
        {
            var copy = new Connection[connections.Length];
            Array.Copy(connections, copy, connections.Length);
            FisherYates.Shuffle(copy);
            _connections = copy;
        }

        public Connection Select()
        {
            var index = Interlocked.Increment(ref _index);
            var connections = _connections;
            return connections[index % connections.Length];
        }
    }
}