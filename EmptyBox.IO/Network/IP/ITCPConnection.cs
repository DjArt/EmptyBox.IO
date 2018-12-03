namespace EmptyBox.IO.Network.IP
{
    public interface ITCPConnection : IPointedConnection<IPAddress, IPPort>
    {
        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new ITCPConnectionProvider ConnectionProvider { get; }
    }
}
