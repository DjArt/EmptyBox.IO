using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Help;
using System;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnection : AUWPPointedConnection<IBluetoothDevice, BluetoothPort, BluetoothAccessPoint, BluetoothAdapter>, IBluetoothConnection
    {
        IBluetoothConnectionProvider IBluetoothConnection.ConnectionProvider => ConnectionProvider;

        internal BluetoothConnection(BluetoothAdapter provider, StreamSocket stream, BluetoothPort port, BluetoothAccessPoint remote)
        {
            Stream = stream;
            RemotePoint = remote;
            LocalPoint = new BluetoothAccessPoint(provider, port, BluetoothAccessPointType.RFCOMM);
            ConnectionProvider = provider;
            ReceivedConnection = true;
        }

        public BluetoothConnection(BluetoothAdapter provider, BluetoothAccessPoint remote)
        {
            RemotePoint = remote;
            ConnectionProvider = provider;
            ReceivedConnection = false;
        }

        protected override (HostName Name, string Port) ConvertAccessPoint(BluetoothAccessPoint point)
        {
            return (point.Address.HardwareAddress.ToHostName(), point.ToServiceIDString().Result);
        }
    }
}
