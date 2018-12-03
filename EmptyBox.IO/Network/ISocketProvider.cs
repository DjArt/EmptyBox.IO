namespace EmptyBox.IO.Network
{
    public interface ISocketProvider
    {
        ISocket CreateSocket();
    }

    public interface ISocketProvider<out TPort> : ISocketProvider
        where TPort : IPort
    {
        ISocket<TPort> CreateSocket(IPort port);
    }
}
