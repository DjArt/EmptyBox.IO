using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnection : IPointedConnection<IBluetoothDevice, BluetoothPort>
    {
        /// <summary>
        /// Интерфейс, на котором устанавливается соединение.
        /// </summary>
        new IBluetoothConnectionProvider ConnectionProvider { get; }
    }
}
