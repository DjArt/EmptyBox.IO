namespace EmptyBox.IO.Network
{
    public interface ISocketProvider
    {
        IAddress Address { get; }
        ISocket CreateSocket(IPort port);
    }
}
