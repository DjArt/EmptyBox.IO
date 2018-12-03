namespace EmptyBox.IO.Network.IP
{
    public interface IUDPSocketProvider : IPointedSocketProvider<IPAddress, IPPort>
    {
        new IPAddress Address { get; }
        IUDPSocket CreateSocket(IPPort port);
    }
}
