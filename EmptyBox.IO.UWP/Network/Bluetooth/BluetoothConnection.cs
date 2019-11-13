using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth.Classic;
using EmptyBox.IO.Network.Help;
using System;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnection : AUWPPointedConnection<IBluetoothDevice, BluetoothPort, BluetoothAdapter>, IBluetoothConnection
    {
        IBluetoothConnectionProvider IBluetoothConnection.ConnectionProvider => ConnectionProvider;

        event MessageReceiveHandler<IConnection<BluetoothPort>> IConnection<BluetoothPort>.MessageReceived
        {
            add => MessageReceived += value;
            remove => MessageReceived -= value;
        }
        event ConnectionInterruptHandler<IConnection<BluetoothPort>> IConnection<BluetoothPort>.ConnectionInterrupted
        {
            add => ConnectionInterrupted += value;
            remove => ConnectionInterrupted -= value;
        }

        internal BluetoothConnection(BluetoothAdapter provider, StreamSocket stream, BluetoothPort port, BluetoothClassicAccessPoint remote)
        {
            Stream = stream;
            RemotePoint = remote;
            LocalPoint = new BluetoothClassicAccessPoint(provider, port);
            ConnectionProvider = provider;
            ReceivedConnection = true;
        }

        public BluetoothConnection(BluetoothAdapter provider, BluetoothClassicAccessPoint remote)
        {
            RemotePoint = remote;
            ConnectionProvider = provider;
            ReceivedConnection = false;
        }

        protected override (HostName Name, string Port) ConvertAccessPoint(IAccessPoint<IBluetoothDevice, BluetoothPort> point)
        {
            return (point.Address.HardwareAddress.ToHostName(), point.ToServiceIDString().Result);
        }
    }
}
