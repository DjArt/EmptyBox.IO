using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    /// <summary>
    /// Представляет методы для работы с протоколом, не требующим установку соединения.
    /// </summary>
    public interface ISocket : ICommunicationElement
    {
        /// <summary>
        /// Интерфейс, на котором устанавливается обмен сообщениями.
        /// </summary>
        ISocketProvider SocketProvider { get; }
    }

    public interface ISocket<out TPort> : ISocket
        where TPort : IPort
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        new event SocketMessageReceiveHandler<TPort> MessageReceived;

        /// <summary>
        /// Локальная точка обмена данных.
        /// </summary>
        TPort LocalPoint { get; }
        /// <summary>
        /// Интерфейс, на котором устанавливается обмен сообщениями.
        /// </summary>
        new ISocketProvider<TPort> SocketProvider { get; }

        /// <summary>
        /// Отправляет сообщение.
        /// </summary>
        /// <param name="data">Передаваемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<bool> Send(IPort receiver, byte[] data);
    }
}
