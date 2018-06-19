using System.Threading.Tasks;

namespace EmptyBox.IO.Network.IP
{
    public interface IUDPSocket : ISocket
    {
        /// <summary>
        /// Интерфейс для приёма и отправки сообщений.
        /// </summary>
        new IUDPSocketProvider SocketProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        new IPPort Port { get; }
        /// Отправляет сообщение по указанному адресу.
        /// </summary>
        /// <param name="host">Адрес доставки.</param>
        /// <param name="data">Доставляемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<SocketOperationStatus> Send(IPAccessPoint host, byte[] data);
        /// <summary>
        /// Запускает прослушивание входящих сообщений.
        /// </summary>
        /// <returns>Результат запуска.</returns>
    }
}
