namespace EmptyBox.IO.Network
{
    public interface IPointedSocketProvider<out TAddress> : ISocketProvider
        where TAddress : IAddress
    {
        TAddress Address { get; }

        new IPointedSocket<TAddress> CreateSocket();
    }

    public interface IPointedSocketProvider<out TAddress, out TPort> : IPointedSocketProvider<TAddress>, ISocketProvider<TPort>
        where TAddress : IAddress
        where TPort : IPort
    {
        new IPointedSocket<TAddress, TPort> CreateSocket(IPort port);
    }
}
