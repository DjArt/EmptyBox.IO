using EmptyBox.IO.Network.IP;

namespace EmptyBox.IO.Network
{
    public interface IPointedConnectionProvider<out TAddress> : IConnectionProvider
        where TAddress : IAddress
    {
        TAddress Address { get; }

        /// <summary>
        /// Создаёт соединение на данном интерфейсе.
        /// </summary>
        /// <param name="address">Адрес удалённой точки для подключения.</param>
        /// <returns>Соединение.</returns>
        IPointedConnection<TAddress> CreateConnection(IAddress address);
        /// <summary>
        /// Создаёт прослушиватель соединений на данном интерфейсе.
        /// </summary>
        /// <returns>Прослушиватель входящих соединений.</returns>
        new IPointedConnectionListener<TAddress> CreateConnectionListener();
    }

    public interface IPointedConnectionProvider<out TAddress, out TPort> : IPointedConnectionProvider<TAddress>, IConnectionProvider<TPort>
        where TAddress : IAddress
        where TPort : IPort
    {
        /// <summary>
        /// Создаёт соединение на данном интерфейсе.
        /// </summary>
        /// <param name="accessPoint">Удалённая точка для подключения.</param>
        /// <returns>Соединение.</returns>
        IPointedConnection<TAddress, TPort> CreateConnection(IAccessPoint<IAddress, IPort> accessPoint);
        /// <summary>
        /// Создаёт прослушиватель соединений на данном интерфейсе.
        /// </summary>
        /// <param name="port">Порт для прослушивания входящих сообщений.</param>
        /// <returns>Прослушиватель входящих соединений.</returns>
        new IPointedConnectionListener<TAddress, TPort> CreateConnectionListener(IPort port);
    }
}
