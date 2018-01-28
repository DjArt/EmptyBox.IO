namespace EmptyBox.IO.Network.IP
{
    public interface ITCPConnectionProvider : IConnectionProvider
    {
        new IPAddress Address { get; }
        ITCPConnection CreateConnection(IPAccessPoint accessPoint);
        ITCPConnectionListener CreateConnectionListener(IPPort port);
    }
}
