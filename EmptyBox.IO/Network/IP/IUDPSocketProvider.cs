namespace EmptyBox.IO.Network.IP
{
    public interface IUDPSocketProvider : ISocketProvider
    {
        new IPAddress Address { get; }
        IUDPSocket CreateSocket(IPPort port);
    }
}
