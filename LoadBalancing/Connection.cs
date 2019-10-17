namespace LoadBalancing
{
    public class Connection
    {
        private readonly int _index;

        public Connection(int index)
        {
            _index = index;
        }

        public override string ToString()
            => _index.ToString();
    }
}