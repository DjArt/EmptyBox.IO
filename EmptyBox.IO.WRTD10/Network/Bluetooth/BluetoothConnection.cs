using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using System;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnection : AConnection<BluetoothAccessPoint, BluetoothPort, BluetoothDeviceProvider>, IBluetoothConnection
    {
        IBluetoothConnectionProvider IBluetoothConnection.ConnectionProvider => ConnectionProvider;

        internal BluetoothConnection(BluetoothDeviceProvider provider, StreamSocket stream, BluetoothPort port, BluetoothAccessPoint remote)
        {
            Stream = stream;
            RemoteHost = remote;
            Port = port;
            ConnectionProvider = provider;
            ReceivedConnection = true;
        }

        public BluetoothConnection(BluetoothDeviceProvider provider, BluetoothAccessPoint remote)
        {
            RemoteHost = remote;
            ConnectionProvider = provider;
            ReceivedConnection = false;
        }

        protected override (HostName Name, string Port) ConvertAccessPoint(BluetoothAccessPoint point)
        {
            return (point.Address.Address.ToHostName(), point.ToServiceIDString().Result);
        }
    }
}
