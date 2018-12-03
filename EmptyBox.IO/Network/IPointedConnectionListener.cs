using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public interface IPointedConnectionListener<out TAddress> : IConnectionListener
        where TAddress : IAddress
    {
        /// <summary>
        /// Уведомляет о входящих соединениях.
        /// </summary>
        new event PointedConnectionReceiveHandler<TAddress> ConnectionReceived;

        /// <summary>
        /// Интерфейс, на котором прослушивается соединение.
        /// </summary>
        new IPointedConnectionProvider<TAddress> ConnectionProvider { get; }
        /// <summary>
        /// Точка прослушивания.
        /// </summary>
        TAddress ListenerPoint { get; }
    }

    public interface IPointedConnectionListener<out TAddress, out TPort> : IPointedConnectionListener<TAddress>, IConnectionListener<TPort>
        where TAddress : IAddress
        where TPort : IPort
    {
        /// <summary>
        /// Уведомляет о входящих соединениях.
        /// </summary>
        new event PointedConnectionReceiveHandler<TAddress, TPort> ConnectionReceived;

        /// <summary>
        /// Интерфейс, на котором прослушивается соединение.
        /// </summary>
        new IPointedConnectionProvider<TAddress, TPort> ConnectionProvider { get; }
        /// <summary>
        /// Точка прослушивания.
        /// </summary>
        new IAccessPoint<TAddress, TPort> ListenerPoint { get; }
    }
}
