using Android.Bluetooth;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Help;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.IO;
using System.Threading.Tasks;
using BluetoothDevice = EmptyBox.IO.Devices.Bluetooth.BluetoothDevice;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnection : APointedConnection<IBluetoothDevice, BluetoothPort, BluetoothAccessPoint, IBluetoothConnectionProvider>, IBluetoothConnection
    {
        IBluetoothConnectionProvider IBluetoothConnection.ConnectionProvider => ConnectionProvider;

        private bool _ReceivedConnection;
        private BinaryReader _Input;
        private BinaryWriter _Output;
        private Task _ReceiveLoopTask;

        public BluetoothSocket BluetoothSocket { get; private set; }

        internal BluetoothConnection(BluetoothDeviceProvider provider, BluetoothSocket socket, BluetoothAccessPoint local, BluetoothAccessPoint remote)
        {
            BluetoothSocket = socket;
            RemotePoint = remote;
            LocalPoint = local;
            ConnectionProvider = provider;
            _ReceivedConnection = true;
        }

        public BluetoothConnection(BluetoothDeviceProvider provider, BluetoothAccessPoint remote)
        {
            RemotePoint = remote;
            ConnectionProvider = provider;
            _ReceivedConnection = false;
        }

        #region Private functions
        private async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    int count = _Input.ReadInt32();
                    if (count > 0)
                    {
                        byte[] buffer = _Input.ReadBytes(count);
                        OnMessageReceive(buffer);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2147467259)
                    {
                        await Close();
                    }
                    //FOR DEBUG
                    else
                    {
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region Public functions
        public override async Task<VoidResult<SocketOperationStatus>> Close()
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    OnConnectionInterrupt();
                    IsActive = false;
                    _ReceiveLoopTask.Wait(100);
                    BluetoothSocket.Dispose();
                    _ReceivedConnection = false;
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                }
                catch (Exception ex)
                {
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
                }
            }
            else
            {
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ConnectionIsAlreadyClosed, null);
            }
        }

        public override async Task<VoidResult<SocketOperationStatus>> Open()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    if (!_ReceivedConnection)
                    {
                        BluetoothSocket = (RemotePoint.Address as BluetoothDevice).InternalDevice.CreateInsecureRfcommSocketToServiceRecord(RemotePoint.Port.ToUUID());
                        _Input = new BinaryReader(BluetoothSocket.InputStream);
                        _Output = new BinaryWriter(BluetoothSocket.OutputStream);
                    }
                    _ReceiveLoopTask = Task.Run((Action)ReceiveLoop);
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                }
                catch (Exception ex)
                {
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
                }
            }
            else
            {
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ConnectionIsAlreadyOpen, null);
            }
        }

        public override async Task<VoidResult<SocketOperationStatus>> Send(byte[] data)
        {
            await Task.Yield();
            try
            {
                _Output.Write(data.Length);
                _Output.Write(data);
                _Output.Flush();
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
            }
            catch (Exception ex)
            {
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
            }
        }
        #endregion
    }
}
