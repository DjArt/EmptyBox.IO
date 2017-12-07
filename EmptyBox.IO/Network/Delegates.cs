namespace EmptyBox.IO.Network
{
    public delegate void ConnectionSocketMessageReceiveHandler(IConnectionSocket connection, byte[] message);
    public delegate void ConnectionReceivedDelegate(IConnectionSocketHandler handler, IConnectionSocket socket);
    public delegate void SocketMessageReceiveHandler(ISocket connection, IAddress host, byte[] message);
    public delegate void ConnectionInterruptHandler(IConnectionSocket connection);
}