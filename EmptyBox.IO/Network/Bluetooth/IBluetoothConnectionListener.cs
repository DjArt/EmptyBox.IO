using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionListener : IConnectionListener
    {
        new IBluetoothConnectionProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        new BluetoothPort Port { get; }
    }
}
