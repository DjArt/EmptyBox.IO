using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    /// <summary>
    /// Представляет методы для работы с протоколом, не требующим установку соединения.
    /// </summary>
    public interface IPointedSocket<out TAddress> : ISocket
        where TAddress : IAddress
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        new event PointedSocketMessageReceiveHandler<TAddress> MessageReceived;

        /// <summary>
        /// Локальная точка обмена данных.
        /// </summary>
        TAddress LocalPoint { get; }
        /// <summary>
        /// Интерфейс, на котором устанавливается обмен сообщениями.
        /// </summary>
        new IPointedSocketProvider<TAddress> SocketProvider { get; }

        /// <summary>
        /// Отправляет сообщение.
        /// </summary>
        /// <param name="data">Передаваемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<bool> Send(IAddress receiver, byte[] data);
    }

    public interface IPointedSocket<out TAddress, out TPort> : IPointedSocket<TAddress>, ISocket<TPort>
        where TAddress : IAddress
        where TPort : IPort
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        new event PointedSocketMessageReceiveHandler<TAddress, TPort> MessageReceived;

        /// <summary>
        /// Локальная точка обмена данных.
        /// </summary>
        new IAccessPoint<TAddress, TPort> LocalPoint { get; }
        /// <summary>
        /// Интерфейс, на котором устанавливается обмен сообщениями.
        /// </summary>
        new IPointedSocketProvider<TAddress, TPort> SocketProvider { get; }

        /// <summary>
        /// Отправляет сообщение.
        /// </summary>
        /// <param name="data">Передаваемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<bool> Send(IAccessPoint<IAddress, IPort> receiver, byte[] data);
    }
}
