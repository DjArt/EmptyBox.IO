using EmptyBox.IO.Interoperability;
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothConnectionSocketHandler : IConnectionListener
    {
        IAccessPoint IConnectionListener.LocalHost => LocalHost;

        private StreamSocketListener _ConnectionListener { get; set; }
        private RfcommServiceProvider _ServiceProvider { get; set; }

        public BluetoothAccessPoint LocalHost { get; private set; }
        public bool IsActive { get; protected set; }
        public event ConnectionReceivedDelegate ConnectionSocketReceived;

        public BluetoothConnectionSocketHandler(BluetoothAccessPoint localhost)
        {
            LocalHost = localhost;
            IsActive = false;
            _ConnectionListener = new StreamSocketListener();
            _ConnectionListener.ConnectionReceived += _ConnectionListener_ConnectionReceived;
        }

        private void _ConnectionListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            BluetoothAccessPoint remotehost = new BluetoothAccessPoint(args.Socket.Information.RemoteHostName.ToMACAddress(), new BluetoothPort(Guid.Parse(args.Socket.Information.RemoteServiceName)));
            ConnectionSocketReceived?.Invoke(this, new BluetoothConnectionSocket(args.Socket, LocalHost, remotehost));
        }

        public async Task<SocketOperationStatus> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    _ServiceProvider = await RfcommServiceProvider.CreateAsync(LocalHost.Port.ToRfcommServiceID());
                    await _ConnectionListener.BindServiceNameAsync(LocalHost.Port.ToRfcommServiceID().AsString());
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