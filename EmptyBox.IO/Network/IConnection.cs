using EmptyBox.ScriptRuntime.Results;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    /// <summary>
    /// Представляет методы для работы с протоколом без адресации и протоколизации, требующим установку соединения.
    /// </summary>
    public interface IConnection : ICommunicationElement
    {
        /// <summary>
        /// Событие, уведомляющее о закрытии соединения.
        /// </summary>
        event ConnectionInterruptHandler<IConnection> ConnectionInterrupted;

        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        IConnectionProvider ConnectionProvider { get; }
    }

    /// <summary>
    /// Представляет методы для работы с протоколом без адресации, требующим установку соединения.
    /// </summary>
    public interface IConnection<out TPort> : IConnection
        where TPort : IPort
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        new event MessageReceiveHandler<IConnection<TPort>> MessageReceived;
        /// <summary>
        /// Событие, уведомляющее о закрытии соединения.
        /// </summary>
        new event ConnectionInterruptHandler<IConnection<TPort>> ConnectionInterrupted;

        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new IConnectionProvider<TPort> ConnectionProvider { get; }
        /// <summary>
        /// Локальная точка обмена данных.
        /// </summary>
        TPort LocalPoint { get; }
        /// <summary>
        /// Удалённая точка обмена данных.
        /// </summary>
        TPort RemotePoint { get; }
    }
}