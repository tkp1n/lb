using System.Threading;

namespace LoadBalancing
{
    internal class ModuloLoadBalancer
    {
        private readonly Connection[] _connections;
        private int _index;

        public ModuloLoadBalancer(Connection[] connections) 
            => _connections = connections.Copy().Shuffle();

        public Connection Select()
        {
            var index = Interlocked.Increment(ref _index);
            var connections = _connections;
            return connections[index % connections.Length];
        }
    }
}