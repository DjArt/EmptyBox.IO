namespace EmptyBox.IO.Network
{
    public interface IConnectionProvider
    {
        IAddress Address { get; }
        IConnection CreateConnection(IAccessPoint accessPoint);
        IConnectionListener CreateConnectionListener(IPort port);
    }
}
