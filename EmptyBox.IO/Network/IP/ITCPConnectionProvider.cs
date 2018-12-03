namespace EmptyBox.IO.Network.IP
{
    public interface ITCPConnectionProvider : IPointedConnectionProvider<IPAddress, IPPort>
    {
        ITCPConnection CreateConnection(IPAccessPoint accessPoint);
        ITCPConnectionListener CreateConnectionListener(IPPort port);
    }
}
