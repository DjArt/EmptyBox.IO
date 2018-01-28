using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnection : IConnection
    {
        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new IBluetoothConnectionProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        new BluetoothPort Port { get; }
        /// <summary>
        /// Адрес точки, с которой установлено соединение.
        /// </summary>
        new BluetoothAccessPoint RemoteHost { get; }
    }
}
