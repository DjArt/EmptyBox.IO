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

        public static bool operator ==(IPAccessPoint x, IPAccessPoint y)
        {
            return x.Address == y.Address && x.Port == y.Port;
        }

        public static bool operator !=(IPAccessPoint x, IPAccessPoint y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (obj is IPAccessPoint point)
            {
                return this == point;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode() ^ Port.GetHashCode();
        }

        public override string ToString()
        {
            return Address.ToString() + ':' + Port.ToString();
        }

        public bool Equals(IAccessPoint<IAddress, IPort> other)
        {
            if (other is IPAccessPoint point)
            {
                return this == point;
            }
            else
            {
                return false;
            }
        }
    }
}
