using System;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using JitBuddy;

namespace LoadBalancing
{
    public class LbComparison
    {
        private Connection[] _connections;
        private LinkedListBalancer _linkedListBalancer;

        private ModuloLoadBalancer _moduloLoadBalancer;

        [Params(8)] public int NofConnections { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _connections = new Connection[NofConnections];
            for (var i = 0; i < _connections.Length; i++)
            {
                _connections[i] = new Connection(i);
            }

            _moduloLoadBalancer = new ModuloLoadBalancer(_connections);
            _linkedListBalancer = new LinkedListBalancer(_connections);
        }

        [GlobalCleanup]
        public void Print()
        {
            var random = typeof(RandomLoadBalancer)
                .GetMethod(nameof(RandomLoadBalancer.Select), BindingFlags.Static | BindingFlags.Public)
                .ToAsm();
            var modulo = typeof(ModuloLoadBalancer)
                .GetMethod(nameof(ModuloLoadBalancer.Select), BindingFlags.Instance | BindingFlags.Public)
                .ToAsm();
            var linkedList = typeof(LinkedListBalancer)
                .GetMethod(nameof(LinkedListBalancer.Select), BindingFlags.Instance | BindingFlags.Public)
                .ToAsm();

            Console.Out.WriteLine("RANDOM");
            Console.Out.WriteLine(random);
            Console.Out.WriteLine("MODULO");
            Console.Out.WriteLine(modulo);
            Console.Out.WriteLine("LINKED LIST");
            Console.Out.WriteLine(linkedList);
        }

        [Benchmark(Baseline = true)]
        public Connection Random()
            => RandomLoadBalancer.Select(_connections);

        [Benchmark]
        public Connection Modulo()
            => _moduloLoadBalancer.Select();

        [Benchmark]
        public Connection LinkedList()
            => _linkedListBalancer.Select();
    }
}