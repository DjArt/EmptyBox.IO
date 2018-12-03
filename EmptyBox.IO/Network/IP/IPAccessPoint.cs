namespace EmptyBox.IO.Network.IP
{
    public class IPAccessPoint : IAccessPoint<IPAddress, IPPort>
    {
        public IPAddress Address { get; set; }
        public IPPort Port { get; set; }

        public IPAccessPoint(IPAddress address, IPPort port)
        {
            Address = address;
            Port = port;
        }

        public override string ToString()
        {
            return Address.ToString() + ':' + Port.ToString();
        }
    }
}
