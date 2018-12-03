using EmptyBox.IO.Network.IP;

namespace EmptyBox.IO.Network
{
    public interface IConnectionProvider
    {
        /// <summary>
        /// Создаёт соединение на данном интерфейсе.
        /// </summary>
        /// <returns>Соединение.</returns>
        IConnection CreateConnection();
        /// <summary>
        /// Создаёт прослушиватель соединений на данном интерфейсе.
        /// </summary>
        /// <returns>Прослушиватель входящих соединений.</returns>
        IConnectionListener CreateConnectionListener();
    }

    public interface IConnectionProvider<out TPort> : IConnectionProvider
        where TPort : IPort
    {
        /// <summary>
        /// Создаёт соединение на данном интерфейсе.
        /// </summary>
        /// <param name="port">Порт для подключения к удалённой точке.</param>
        /// <returns>Соединение</returns>
        IConnection<TPort> CreateConnection(IPort port);
        /// <summary>
        /// Создаёт прослушиватель соединений на данном интерфейсе.
        /// </summary>
        /// <param name="port">Порт для прослушивания входящих соединений.</param>
        /// <returns>Прослушиватель входящих соединений.</returns>
        IConnectionListener<TPort> CreateConnectionListener(IPort port);
    }
}
