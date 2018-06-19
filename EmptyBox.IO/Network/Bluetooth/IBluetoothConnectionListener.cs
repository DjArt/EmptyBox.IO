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
