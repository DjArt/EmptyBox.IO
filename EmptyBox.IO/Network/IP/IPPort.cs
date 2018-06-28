namespace EmptyBox.IO.Network.IP
{
    public struct IPPort : IPort
    {
        public static implicit operator IPPort(ushort value)
        {
            return new IPPort(value);
        }

        public static implicit operator ushort(IPPort value)
        {
            return value.Value;
        }

        public ushort Value { get; set; }

        public IPPort(ushort value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
