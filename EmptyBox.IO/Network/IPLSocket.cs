using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    /// <summary>
    /// Port less socket interface.
    /// </summary>
    public interface IPLSocket
    {
        IAddress Address { get; }
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        event PLSocketMessageReceiveHandler MessageReceived;
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
}
