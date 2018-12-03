namespace EmptyBox.IO.Network.IP
{
    public interface ITCPConnectionListener : IPointedConnectionListener<IPAddress, IPPort>
    {
        new ITCPConnectionProvider ConnectionProvider { get; }
    }
}
