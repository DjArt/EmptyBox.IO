namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionProvider : IConnectionProvider
    {
        new MACAddress Address { get; }
        IBluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint);
        IBluetoothConnectionListener CreateConnectionListener(BluetoothPort port);
    }
}
