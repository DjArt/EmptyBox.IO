using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public interface IConnectionListener
    {
        IConnectionProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        IPort Port { get; }
        /// <summary>
        /// Адрес точки, с которой установлено соединение.
        /// </summary>
        bool IsActive { get; }
        event ConnectionReceivedDelegate ConnectionSocketReceived;
        Task<SocketOperationStatus> Start();
        Task<SocketOperationStatus> Stop();
    }

    public interface IConnectionListener<TAddress, TPort, TAccessPoint, TProvider> : IConnectionListener where TAddress : IAddress where TPort : IPort where TAccessPoint : IAccessPoint<TAddress, TPort> where TProvider : IConnectionProvider<TAddress, TPort, TAccessPoint>
    {
        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new TProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        new TPort Port { get; }
        /// <summary>
        /// Адрес точки, с которой установлено соединение.
        /// </summary>
    }
}
