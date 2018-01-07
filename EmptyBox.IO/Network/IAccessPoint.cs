namespace EmptyBox.IO.Network
{
    public interface IAccessPoint
    {
        IAddress Address { get; }
        IPort Port { get; }
    }

    public interface IAccessPoint<TAddress, TPort> : IAccessPoint where TAddress : IAddress where TPort : IPort
    {
        new TAddress Address { get; }
        new TPort Port { get; }
    }
}
