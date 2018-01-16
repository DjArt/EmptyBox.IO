using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    /// <summary>
    /// Представляет методы для работы с протоколом, не требующим установку соединения.
    /// </summary>
    public interface ISocket
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        event SocketMessageReceiveHandler MessageReceived;
        /// <summary>
        /// Интерфейс для приёма и отправки сообщений.
        /// </summary>
        IConnectionProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        IPort Port { get; }
        /// Отправляет сообщение по указанному адресу.
        /// </summary>
        /// <param name="host">Адрес доставки.</param>
        /// <param name="data">Доставляемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<SocketOperationStatus> Send(IAddress host, byte[] data);
        /// <summary>
        /// Запускает прослушивание входящих сообщений.
        /// </summary>
        /// <returns>Результат запуска.</returns>
        Task<SocketOperationStatus> Open();
        /// <summary>
        /// Останавливает прослушивание входящих сообщений.
        /// </summary>
        /// <returns>Результат закрытия.</returns>
        Task<SocketOperationStatus> Close();
    }

    public interface ISocket<TAddress, TPort, TAccessPoint, TProvider> : ISocket where TAddress : IAddress where TPort : IPort where TAccessPoint : IAccessPoint<TAddress, TPort> where TProvider : ISocketProvider<TAddress, TPort, TAccessPoint>
    {
        /// <summary>
        /// Интерфейс для приёма и отправки сообщений.
        /// </summary>
        new TProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        new TPort Port { get; }
    }
}
