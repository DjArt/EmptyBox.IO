using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.MAC;
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothConnectionListener : IBluetoothConnectionListener
    {
        IConnectionProvider IConnectionListener.ConnectionProvider => ConnectionProvider;
        IPort IConnectionListener.Port => Port;
        IBluetoothAdapter IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IBluetoothAdapter>.ConnectionProvider => ConnectionProvider;

        private StreamSocketListener _ConnectionListener { get; set; }
        private RfcommServiceProvider _ServiceProvider { get; set; }

        public BluetoothAdapter ConnectionProvider { get; private set; }
        public BluetoothPort Port { get; private set; }
        public bool IsActive { get; protected set; }
        public event ConnectionReceivedDelegate ConnectionSocketReceived;

        public BluetoothConnectionListener(BluetoothAdapter adapter, BluetoothPort port)
        {
            ConnectionProvider = adapter;
            Port = port;
            IsActive = false;
            _ConnectionListener = new StreamSocketListener();
            _ConnectionListener.ConnectionReceived += _ConnectionListener_ConnectionReceived;
        }

        private void _ConnectionListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            BluetoothAccessPoint remotehost = new BluetoothAccessPoint(args.Socket.Information.RemoteHostName.ToMACAddress(), new BluetoothPort(Guid.Parse(args.Socket.Information.RemoteServiceName)));
            ConnectionSocketReceived?.Invoke(this, new BluetoothConnection(ConnectionProvider, Port, args.Socket, remotehost));
        }

        public async Task<SocketOperationStatus> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    _ServiceProvider = await RfcommServiceProvider.CreateAsync(Port.ToRfcommServiceID());
                    await _ConnectionListener.BindServiceNameAsync(Port.ToRfcommServiceID().AsString());
                    _ServiceProvider.StartAdvertising(_ConnectionListener);
                    IsActive = true;
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            else
            {
                return SocketOperationStatus.ListenerIsAlreadyStarted;
            }
        }

        public async Task<SocketOperationStatus> Stop()
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    _ServiceProvider.StopAdvertising();
                    IsActive = false;
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            else
            {
                return SocketOperationStatus.ListenerIsAlreadyClosed;
            }
        }
    }
}