using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    /// <summary>
    /// Представляет методы для работы с протоколом, требующим установку соединения.
    /// </summary>
    public interface IConnectionSocket
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        event ConnectionSocketMessageReceiveHandler MessageReceived;
        /// <summary>
        /// Определяет текущий адрес для приёма и отправки сообщений.
        /// </summary>
        IAccessPoint LocalHost { get; }
        /// <summary>
        /// Событие, уведомляющее о закрытии соединения.
        /// </summary>
        event ConnectionInterruptHandler ConnectionInterrupt;
        /// <summary>
        /// Адрес точки, с которой установлено соединение.
        /// </summary>
        IAccessPoint RemoteHost { get; }
        /// <summary>
        /// Отправляет сообщение точке, с которой установлено соединение.
        /// </summary>
        /// <param name="data">Передаваемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<SocketOperationStatus> Send(byte[] data);
        /// <summary>
        /// Устанавливает соединение с удалённой точкой.
        /// </summary>
        /// <returns>Результат запуска.</returns>
        Task<SocketOperationStatus> Open();
        /// <summary>
        /// Разрывает соединение с удалённой точкой.
        /// </summary>
        /// <returns>Результат закрытия.</returns>
        Task<SocketOperationStatus> Close();

        bool IsActive { get; }
    }
}