using System.Threading.Tasks;

namespace EmptyBox.IO.Network.IP
{
    public interface IUDPSocket : IPointedSocket<IPAddress, IPPort>
    {
        /// <summary>
        /// Интерфейс для приёма и отправки сообщений.
        /// </summary>
        new IUDPSocketProvider SocketProvider { get; }
        /// Отправляет сообщение по указанному адресу.
        /// </summary>
        /// <param name="host">Адрес доставки.</param>
        /// <param name="data">Доставляемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<SocketOperationStatus> Send(IPAccessPoint host, byte[] data);
    }
}
