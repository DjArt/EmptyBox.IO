namespace EmptyBox.IO.Network
{
    public interface ISocketProvider
    {
        IAddress Address { get; }
        ISocket CreateSocket(IPort port);
    }

    public interface ISocketProvider<TAddress, TPort, TAccessPoint> : IConnectionProvider where TAddress : IAddress where TPort : IPort where TAccessPoint : IAccessPoint<TAddress, TPort>
    {
        new TAddress Address { get; }
        new ISocket<TAddress, TPort, TAccessPoint, ISocketProvider<TAddress, TPort, TAccessPoint>> CreateSocket(TPort port);
    }
}
