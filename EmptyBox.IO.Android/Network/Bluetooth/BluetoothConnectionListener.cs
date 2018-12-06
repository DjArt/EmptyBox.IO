using Android.Bluetooth;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Help;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using BluetoothAdapter = EmptyBox.IO.Devices.Bluetooth.BluetoothAdapter;
using BluetoothDevice = EmptyBox.IO.Devices.Bluetooth.BluetoothDevice;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnectionListener : APointedConnectionListener<IBluetoothDevice, BluetoothPort, BluetoothAccessPoint, IBluetoothConnectionProvider>, IBluetoothConnectionListener
    {
        IBluetoothConnectionProvider IBluetoothConnectionListener.ConnectionProvider => throw new NotImplementedException();

        #region Private objects
        public BluetoothServerSocket ServerSocket;
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
                    BluetoothSocket socket = await ServerSocket.AcceptAsync(1000);
                    if (socket != null)
                    {
                        BluetoothAccessPoint accessPoint = new BluetoothAccessPoint(new BluetoothDevice(socket.RemoteDevice), new BluetoothPort());
                        BluetoothConnection connection = new BluetoothConnection(ConnectionProvider, socket, Port, accessPoint);
                        OnConnectionReceive(connection);
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
        public override async Task<VoidResult<SocketOperationStatus>> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    ServerSocket = ConnectionProvider.Adapter.InternalDevice.ListenUsingInsecureRfcommWithServiceRecord("Name?", Port.ToUUID());
                    IsActive = true;
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                }
                catch (Exception ex)
                {
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
                }
            }
            else
            {
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ListenerIsAlreadyStarted, null);
            }
        }

        public override async Task<VoidResult<SocketOperationStatus>> Stop()
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    ServerSocket.Dispose();
                    IsActive = false;
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                }
                catch (Exception ex)
                {
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
                }
            }
            else
            {
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ListenerIsAlreadyClosed, null);
            }
        }
        #endregion
    }
}