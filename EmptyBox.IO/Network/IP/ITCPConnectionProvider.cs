namespace EmptyBox.IO.Network.IP
{
    public interface ITCPConnectionProvider : IConnectionProvider<IPAddress, IPPort, IPAccessPoint>
    {
        new IPAddress Address { get; }
        new TCPConnection CreateConnection(IPAccessPoint accessPoint);
        new TCPConnectionListener CreateConnectionListener(IPPort port);
        ITCPConnectionProvider TCPProvider { get; }
    }
}
