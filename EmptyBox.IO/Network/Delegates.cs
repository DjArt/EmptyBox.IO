namespace EmptyBox.IO.Network
{
    public delegate void MessageReceiveHandler(ICommunicationElement socket, byte[] message);

    public delegate void ConnectionReceiveHandler(IConnectionListener handler, IConnection connection);

    public delegate void ConnectionInterruptHandler(IConnection connection);

    public delegate void ConnectionMessageReceiveHandler<in TPort>(IConnection<TPort> connection, byte[] message)
        where TPort : IPort;

    public delegate void ConnectionReceiveHandler<in TPort>(IConnectionListener<TPort> handler, IConnection<TPort> connection)
        where TPort : IPort;

    public delegate void ConnectionInterruptHandler<in TPort>(IConnection<TPort> connection)
        where TPort : IPort;

    public delegate void PointedConnectionMessageReceiveHandler<in TAddress>(IPointedConnection<TAddress> connection, byte[] message)
        where TAddress : IAddress;

    public delegate void PointedConnectionReceiveHandler<in TAddress>(IPointedConnectionListener<TAddress> handler, IPointedConnection<TAddress> connection)
        where TAddress : IAddress;

    public delegate void PointedConnectionInterruptHandler<in TAddress>(IPointedConnection<TAddress> connection)
        where TAddress : IAddress;

    public delegate void PointedConnectionMessageReceiveHandler<in TAddress, in TPort>(IPointedConnection<TAddress, TPort> connection, byte[] message)
        where TAddress : IAddress
        where TPort : IPort;

    public delegate void PointedConnectionReceiveHandler<in TAddress, in TPort>(IPointedConnectionListener<TAddress, TPort> handler, IPointedConnection<TAddress, TPort> connection)
        where TAddress : IAddress
        where TPort : IPort;

    public delegate void PointedConnectionInterruptHandler<in TAddress, in TPort>(IPointedConnection<TAddress, TPort> connection)
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