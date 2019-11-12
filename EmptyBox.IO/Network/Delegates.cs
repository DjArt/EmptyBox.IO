using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public delegate void MessageReceiveHandler<in TCommunicationElement>(TCommunicationElement socket, byte[] message)
        where TCommunicationElement : ICommunicationElement;
    public delegate void ConnectionInterruptHandler<in TConnection>(TConnection connection)
        where TConnection : IConnection;
    public delegate void ConnectionReceiveHandler(IConnectionListener handler, IConnection connection);
    public delegate void ConnectionReceiveHandler<in TPort>(IConnectionListener<TPort> handler, IConnection<TPort> connection)
        where TPort : IPort;
    public delegate void PointedConnectionReceiveHandler<in TAddress>(IPointedConnectionListener<TAddress> handler, IPointedConnection<TAddress> connection)
        where TAddress : IAddress;
    public delegate void PointedConnectionReceiveHandler<in TAddress, in TPort>(IPointedConnectionListener<TAddress, TPort> handler, IPointedConnection<TAddress, TPort> connection)
        where TAddress : IAddress
        where TPort : IPort;
    public delegate void SocketMessageReceiveHandler<in TPort>(ISocket<TPort> socket, TPort sender, byte[] message)
        where TPort : IPort;
    public delegate void PointedSocketMessageReceiveHandler<in TAddress>(IPointedSocket<TAddress> socket, TAddress sender, byte[] message)
        where TAddress : IAddress;
    public delegate void PointedSocketMessageReceiveHandler<in TAddress, in TPort>(IPointedSocket<TAddress, TPort> socket, IAccessPoint<TAddress, TPort> sender, byte[] message)
        where TAddress : IAddress
        where TPort : IPort;
}