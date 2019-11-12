using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public interface IConnectionListener
    {
        /// <summary>
        /// Уведомляет о входящих соединениях.
        /// </summary>
        event ConnectionReceiveHandler ConnectionReceived;

        /// <summary>
        /// Интерфейс, на котором прослушивается соединение.
        /// </summary>
        IConnectionProvider ConnectionProvider { get; }
        bool IsActive { get; }

        Task<bool> Start();
        Task<bool> Stop();
    }

    public interface IConnectionListener<out TPort> : IConnectionListener
        where TPort : IPort
    {
        /// <summary>
        /// Уведомляет о входящих соединениях.
        /// </summary>
        new event ConnectionReceiveHandler<TPort> ConnectionReceived;

        /// <summary>
        /// Интерфейс, на котором прослушивается соединение.
        /// </summary>
        new IConnectionProvider<TPort> ConnectionProvider { get; }
        /// <summary>
        /// Точка прослушивания.
        /// </summary>
        TPort ListenerPoint { get; }
    }
}
