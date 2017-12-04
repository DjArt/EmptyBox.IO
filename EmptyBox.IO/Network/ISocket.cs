using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public delegate void SocketMessageReceiveHandler(ISocket connection, IAddress host, byte[] message);
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
        /// Определяет текущий адрес для приёма и отправки сообщений.
        /// </summary>
        IAddress LocalHost { get; }
        /// <summary>
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
