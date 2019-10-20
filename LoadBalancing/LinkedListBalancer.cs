using System.Threading;

namespace LoadBalancing
{
    internal class LinkedListBalancer
    {
        private Node _node;

        public LinkedListBalancer(Connection[] connections) 
            => _node = BuildList(connections.Copy().Shuffle());

        public Connection Select()
        {
            Node selected;
            do
            {
                selected = _node;
            } while (Interlocked.CompareExchange(ref _node, selected.Next, selected) != selected);

            return selected.Current;
        }

        private static Node BuildList(Connection[] connections)
        {
            var first = new Node(connections[0]);
            var last = first;
            for (var i = 1; i < connections.Length; i++)
            {
                var node = new Node(connections[i]);
                last.Next = node;
                last = node;
            }

            last.Next = first;

            return last;
        }

        private class Node
        {
            public Node(Connection current) => Current = current;

            public Connection Current { get; }
            public Node Next { get; set; }
        }
    }
}