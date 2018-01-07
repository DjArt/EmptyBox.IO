namespace EmptyBox.IO.Network
{
    public delegate void ConnectionSocketMessageReceiveHandler(IConnection connection, byte[] message);
    public delegate void ConnectionReceivedDelegate(IConnectionListener handler, IConnection socket);
    public delegate void SocketMessageReceiveHandler(ISocket connection, IAccessPoint host, byte[] message);
    public delegate void ConnectionInterruptHandler(IConnection connection);
}