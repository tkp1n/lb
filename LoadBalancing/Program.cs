using BenchmarkDotNet.Running;

namespace LoadBalancing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<LbComparison>();
        }
    }
}