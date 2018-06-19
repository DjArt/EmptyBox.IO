namespace EmptyBox.IO.Network.IP
{
    public interface ITCPConnection : IConnection
    {
        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new ITCPConnectionProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        new IPPort Port { get; }
        /// <summary>
        /// Адрес точки, с которой установлено соединение.
        /// </summary>
        new IPAccessPoint RemoteHost { get; }
    }
}
