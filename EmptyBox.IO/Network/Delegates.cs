namespace EmptyBox.IO.Network
{
    public delegate void ConnectionMessageReceiveHandler(IConnection connection, byte[] message);
    public delegate void ConnectionReceivedDelegate(IConnectionListener handler, IConnection socket);
    public delegate void ConnectionInterruptHandler(IConnection connection);
    public delegate void SocketMessageReceiveHandler(ISocket connection, IAccessPoint host, byte[] message);
    public delegate void PLSocketMessageReceiveHandler(IPLSocket connection, IAddress host, byte[] message);
}