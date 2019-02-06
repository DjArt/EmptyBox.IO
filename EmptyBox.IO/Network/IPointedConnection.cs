using EmptyBox.IO.Network.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    /// <summary>
    /// Представляет методы для работы с протоколом без протоколизации, требующим установку соединения.
    /// </summary>
    public interface IPointedConnection<out TAddress> : IConnection
        where TAddress : IAddress
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        new event MessageReceiveHandler<IPointedConnection<TAddress>> MessageReceived;
        /// <summary>
        /// Событие, уведомляющее о закрытии соединения.
        /// </summary>
        new event ConnectionInterruptHandler<IPointedConnection<TAddress>> ConnectionInterrupted;

        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new IPointedConnectionProvider<TAddress> ConnectionProvider { get; }
        /// <summary>
        /// Локальная точка обмена данных.
        /// </summary>
        TAddress LocalPoint { get; }
        /// <summary>
        /// Удалённая точка обмена данных.
        /// </summary>
        TAddress RemotePoint { get; }
    }

    /// <summary>
    /// Представляет методы для работы с протоколом, требующим установку соединения.
    /// </summary>
    public interface IPointedConnection<out TAddress, out TPort> : IPointedConnection<TAddress>, IConnection<TPort>
        where TAddress : IAddress
        where TPort : IPort
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        new event MessageReceiveHandler<IPointedConnection<TAddress, TPort>> MessageReceived;
        /// <summary>
        /// Событие, уведомляющее о закрытии соединения.
        /// </summary>
        new event ConnectionInterruptHandler<IPointedConnection<TAddress, TPort>> ConnectionInterrupted;

        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new IPointedConnectionProvider<TAddress, TPort> ConnectionProvider { get; }
        /// <summary>
        /// Локальная точка обмена данных.
        /// </summary>
        new IAccessPoint<TAddress, TPort> LocalPoint { get; }
        /// <summary>
        /// Удалённая точка обмена данных.
        /// </summary>
        new IAccessPoint<TAddress, TPort> RemotePoint { get; }
    }
}
