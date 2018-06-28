namespace EmptyBox.IO.Network.IP
{
    public struct IPAccessPoint : IAccessPoint
    {
        IAddress IAccessPoint.Address => Address;
        IPort IAccessPoint.Port => Port;

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
