using System;
using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using JitBuddy;

namespace LoadBalancing
{
    public class LbComparison
    {
        private Connection[] _connections;
        private ModuloLoadBalancer _moduloLoadBalancer;
        private LinkedListBalancer _linkedListBalancer;

        [GlobalSetup]
        public void Setup()
        {
            const int nofConnections = 8;
            _connections = new Connection[nofConnections];
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
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            
            var threadStaticRandom = typeof(ThreadStaticRandomLoadBalancer)
                .GetMethod(nameof(ThreadStaticRandomLoadBalancer.Select), BindingFlags.Static | BindingFlags.Public)
                .ToAsm();
            var syncRandom = typeof(SyncRandomLoadBalancer)
                .GetMethod(nameof(SyncRandomLoadBalancer.Select), BindingFlags.Static | BindingFlags.Public)
                .ToAsm();
            var modulo = typeof(ModuloLoadBalancer)
                .GetMethod(nameof(ModuloLoadBalancer.Select), BindingFlags.Instance | BindingFlags.Public)
                .ToAsm();
            var linkedList = typeof(LinkedListBalancer)
                .GetMethod(nameof(LinkedListBalancer.Select), BindingFlags.Instance | BindingFlags.Public)
                .ToAsm();

            Console.Out.WriteLine("THREAD STATIC RANDOM");
            Console.Out.WriteLine(threadStaticRandom);
            Console.Out.WriteLine("SYNC RANDOM");
            Console.Out.WriteLine(syncRandom);
            Console.Out.WriteLine("MODULO");
            Console.Out.WriteLine(modulo);
            Console.Out.WriteLine("LINKED LIST");
            Console.Out.WriteLine(linkedList);
        }

        [Benchmark(Baseline = true)]
        public Connection ThreadStaticRandom()
            => ThreadStaticRandomLoadBalancer.Select(_connections);

        [Benchmark]
        public Connection SyncRandom()
            => SyncRandomLoadBalancer.Select(_connections);

        [Benchmark]
        public Connection Modulo()
            => _moduloLoadBalancer.Select();

        [Benchmark]
        public Connection LinkedList()
            => _linkedListBalancer.Select();
    }
}