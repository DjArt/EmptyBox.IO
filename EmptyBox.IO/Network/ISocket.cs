using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public delegate void MessageReceiveHandler(ISocket connection, ILinkLevelAddress host, byte[] message);
    //Добавить причину отключения
    public delegate void ConnectionInterruptHandler(IConnectionSocket connection);
    //Пусть, пока что, будет в одном файле. После определения конкретных методов нужно разделить.

    /// <summary>
    /// Представляет методы для работы с протоколом, не требующим установку соединения.
    /// </summary>
    public interface ISocket
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        event MessageReceiveHandler MessageReceived;
        int ReadBufferLength { get; }
        int WriteBufferLength { get; }
        /// <summary>
        /// Определяет текущий адрес для приёма и отправки сообщений.
        /// </summary>
        ILinkLevelAddress LocalHost { get; }
        /// <summary>
        /// Отправляет сообщение по указанному адресу. Для протоколов с установкой соединения указанный адрес должен совпадать с адресом установки соединения.
        /// </summary>
        /// <param name="host">Адрес доставки.</param>
        /// <param name="data">Доставляемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<bool> Send(ILinkLevelAddress host, byte[] data);
        /// <summary>
        /// Запускает прослушивание входящих сообщений. Для протоколов с установкой соединения устанавливает соединение, если оно не было ранее установлено.
        /// </summary>
        /// <returns>Результат запуска.</returns>
        Task<bool> Open();
        /// <summary>
        /// Останавливает прослушивание входящих сообщений. Для протоколов с установкой соединения закрывает соединение, если оно не было ещё закрыто.
        /// </summary>
        /// <returns>Результат закрытия.</returns>
        Task<bool> Close();
    }

    /// <summary>
    /// Представляет методы для работы с протоколом, требующим установку соединения.
    /// </summary>
    public interface IConnectionSocket : ISocket
    {
        /// <summary>
        /// Событие, уведомляющее о закрытии соединения.
        /// </summary>
        event ConnectionInterruptHandler ConnectionInterrupt;
        /// <summary>
        /// Адрес устройства, с которым установлено соединение.
        /// </summary>
        ILinkLevelAddress RemoteHost { get; set; }
        /// <summary>
        /// Отправляет сообщение устройству, с которым установлено соединение.
        /// </summary>
        /// <param name="data">Передаваемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<bool> Send(byte[] data);

        bool IsActive { get; }
    }
}
