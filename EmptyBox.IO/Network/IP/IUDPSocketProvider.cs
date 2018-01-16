namespace EmptyBox.IO.Network.IP
{
    public interface IUDPSocketProvider : ISocketProvider<IPAddress, IPPort, IPAccessPoint>
    {
        new IPAddress Address { get; }
        new ISocket CreateSocket(IPPort port);
        IUDPSocketProvider UDPProvider { get; }
    }
}
