using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnection : AConnection<BluetoothAccessPoint, BluetoothPort, BluetoothDeviceProvider>, IBluetoothConnection
    {
        IBluetoothConnectionProvider IBluetoothConnection.ConnectionProvider => ConnectionProvider;

        private Android.Bluetooth.BluetoothSocket Socket;

        internal BluetoothConnection(BluetoothDeviceProvider provider, Android.Bluetooth.BluetoothSocket socket, BluetoothPort port, BluetoothAccessPoint remote)
        {
            Socket = socket;
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

        protected override (BinaryReader Input, BinaryWriter Output) GetPair()
        {
            if (!ReceivedConnection)
            {
                Socket = (RemoteHost.Address as BluetoothDevice).InternalDevice.CreateInsecureRfcommSocketToServiceRecord(RemoteHost.Port.ToUUID());
            }
            return (new BinaryReader(Socket.InputStream), new BinaryWriter(Socket.OutputStream));
        }
    }
}
