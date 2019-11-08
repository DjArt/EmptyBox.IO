namespace EmptyBox.IO.Network.IP
{
    public sealed class IPPort : IPort
    {
        public ushort Value { get; set; }

        public IPPort(ushort value)
        {
            Value = value;
        }

        public static bool operator ==(IPPort x, IPPort y)
        {
            return x.Value == y.Value;
        }

        public static bool operator !=(IPPort x, IPPort y)
        {
            return !(x == y);
        }

        public static implicit operator IPPort(ushort value)
        {
            return new IPPort(value);
        }

        public static implicit operator ushort(IPPort value)
        {
            return value.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is IPPort port)
            {
                return this == port;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool Equals(IPort other)
        {
            if (other is IPPort port)
            {
                return this == port;
            }
            else
            {
                return false;
            }
        }
    }
}
