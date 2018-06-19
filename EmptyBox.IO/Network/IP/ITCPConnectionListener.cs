namespace EmptyBox.IO.Network.IP
{
    public interface ITCPConnectionListener : IConnectionListener
    {
        new ITCPConnectionProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        new IPPort Port { get; }
        /// <summary>
        /// Адрес точки, с которой установлено соединение.
        /// </summary>
    }
}
