using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnectionListener : IBluetoothConnectionListener
    {
        #region IConnectionProvider interface properties
        IConnectionProvider IConnectionListener.ConnectionProvider => ConnectionProvider;
        IPort IConnectionListener.Port => Port;
        #endregion

        #region IBluetoothConnectionProvider interface properties
        IBluetoothConnectionProvider IBluetoothConnectionListener.ConnectionProvider => ConnectionProvider;
        #endregion

        #region Private objects
        private Android.Bluetooth.BluetoothServerSocket ServerSocket;
        #endregion

        #region Public objects
        public BluetoothDeviceProvider ConnectionProvider { get; private set; }
        public BluetoothPort Port { get; private set; }
        public bool IsActive { get; private set; }
        public event ConnectionReceivedDelegate ConnectionSocketReceived;
        #endregion

        #region Constructors
        public BluetoothConnectionListener(BluetoothDeviceProvider provider, BluetoothPort port)
        {
            ConnectionProvider = provider;
            Port = port;
            IsActive = false;
        }
        #endregion

        #region Private functions
        private async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    Android.Bluetooth.BluetoothSocket socket = await ServerSocket.AcceptAsync(1000);
                    if (socket != null)
                    {
                        BluetoothAccessPoint accessPoint = new BluetoothAccessPoint(new BluetoothDevice(socket.RemoteDevice), new BluetoothPort());
                        BluetoothConnection connection = new BluetoothConnection(ConnectionProvider, socket, Port, accessPoint);
                        ConnectionSocketReceived?.Invoke(this, connection);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
                    ServerSocket = ConnectionProvider.Adapter.InternalDevice.ListenUsingInsecureRfcommWithServiceRecord("Name?", Port.ToUUID());
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
                    ServerSocket.Dispose();
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