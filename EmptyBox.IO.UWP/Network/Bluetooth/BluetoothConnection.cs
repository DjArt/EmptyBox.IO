using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.MAC;
using System;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothConnection : IBluetoothConnection
    {
        IConnectionProvider IConnection.ConnectionProvider => ConnectionProvider;
        IPort IConnection.Port => Port;
        IAccessPoint IConnection.RemoteHost => RemoteHost;

        IBluetoothAdapter IConnection<MACAddress, BluetoothPort, BluetoothAccessPoint, IBluetoothAdapter>.ConnectionProvider => ConnectionProvider;

        private DataReader _Reader;
        private DataWriter _Writer;
        private Task _ReceiveLoopTask;
        private bool _InternalConstructor;
        private StreamSocket Socket;

        public event ConnectionInterruptHandler ConnectionInterrupt;
        public event ConnectionSocketMessageReceiveHandler MessageReceived;

        public BluetoothAdapter ConnectionProvider { get; private set; }
        public BluetoothPort Port { get; private set; }
        public BluetoothAccessPoint RemoteHost { get; private set; }
        public bool IsActive { get; protected set; }

        internal BluetoothConnection(BluetoothAdapter adapter, BluetoothPort port, StreamSocket socket, BluetoothAccessPoint remotehost)
        {
            ConnectionProvider = adapter;
            Port = port;
            Socket = socket;
            RemoteHost = remotehost;
            IsActive = false;
            _InternalConstructor = true;
        }

        public BluetoothConnection(BluetoothAdapter adapter, BluetoothAccessPoint remotehost)
        {
            ConnectionProvider = adapter;
            Socket = new StreamSocket();
            RemoteHost = remotehost;
            IsActive = false;
            _InternalConstructor = false;
        }

        private async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    int count = _Reader.ReadInt32();
                    if (count > 0)
                    {
                        byte[] buffer = new byte[count];
                        _Reader.ReadBytes(buffer);
                        MessageReceived?.Invoke(this, buffer);
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

        public async Task<SocketOperationStatus> Close()
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    ConnectionInterrupt?.Invoke(this);
                    IsActive = false;
                    _ReceiveLoopTask.Wait(100);
                    Socket.Dispose();
                    _InternalConstructor = false;
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            else
            {
                return SocketOperationStatus.ConnectionIsAlreadyClosed;
            }
        }

        public async Task<SocketOperationStatus> Open()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    if (!_InternalConstructor)
                    {
                        Socket = new StreamSocket();
                        string port = await RemoteHost.ToServiceIDString();
                        if (port != String.Empty)
                        {
                            Socket.ConnectAsync(RemoteHost.Address.ToHostName(), port).AsTask().Wait();
                        }
                        else
                        {
                            return SocketOperationStatus.ConnectionIsAlreadyClosed;
                        }
                    }
                    _Reader = new DataReader(Socket.InputStream);
                    _Writer = new DataWriter(Socket.OutputStream);
                    _ReceiveLoopTask = Task.Run((Action)ReceiveLoop);
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    switch (SocketError.GetStatus(ex.HResult))
                    {
                        default:
                            return SocketOperationStatus.UnknownError;
                    }
                }
            }
            return SocketOperationStatus.UnknownError;
        }

        public async Task<SocketOperationStatus> Send(byte[] data)
        {
            await Task.Yield();
            try
            {
                _Writer.WriteInt32(data.Length);
                _Writer.WriteBytes(data);
                if (await _Writer.FlushAsync())
                {
                    return SocketOperationStatus.Success;
                }
                else
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            catch (Exception ex)
            {
                return SocketOperationStatus.UnknownError;
            }
        }
    }
}
