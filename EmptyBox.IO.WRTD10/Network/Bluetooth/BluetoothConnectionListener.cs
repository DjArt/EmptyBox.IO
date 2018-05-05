using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothConnectionListener : IBluetoothConnectionListener
    {
        #region IConnectionProvider interface properties
        IConnectionProvider IConnectionListener.ConnectionProvider => ConnectionProvider;
        IPort IConnectionListener.Port => Port;
        #endregion

        #region IBluetoothConnectionProvider interface properties
        IBluetoothConnectionProvider IBluetoothConnectionListener.ConnectionProvider => ConnectionProvider;
        #endregion

        #region Private objects
        private StreamSocketListener _ConnectionListener { get; set; }
        private RfcommServiceProvider _ServiceProvider { get; set; }
        #endregion

        #region Public objects
        public BluetoothDeviceProvider ConnectionProvider { get; private set; }
        public BluetoothPort Port { get; private set; }
        public bool IsActive { get; protected set; }
        public event ConnectionReceivedDelegate ConnectionSocketReceived;
        #endregion

        #region Constructors
        public BluetoothConnectionListener(BluetoothDeviceProvider adapter, BluetoothPort port)
        {
            ConnectionProvider = adapter;
            Port = port;
            IsActive = false;
            _ConnectionListener = new StreamSocketListener();
            _ConnectionListener.ConnectionReceived += _ConnectionListener_ConnectionReceived;
        }
        #endregion

        #region Private functions
        private async void _ConnectionListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            var result = await ConnectionProvider.TryGetFromMAC(args.Socket.Information.RemoteHostName.ToMACAddress());
            if (result.Status == Access.AccessStatus.Success)
            {
                BluetoothAccessPoint remotehost = new BluetoothAccessPoint(result.Result, new BluetoothPort(Guid.Parse(args.Socket.Information.RemoteServiceName)));
                ConnectionSocketReceived?.Invoke(this, new BluetoothConnection(ConnectionProvider, args.Socket, Port, remotehost));
            }
        }
        #endregion

        #region Public functions
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
        #endregion
    }
}