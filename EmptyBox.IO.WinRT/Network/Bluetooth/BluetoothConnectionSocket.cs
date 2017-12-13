using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Interoperability;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothConnectionSocket : IConnectionSocket
    {
        IAccessPoint IConnectionSocket.LocalHost => LocalHost;
        IAccessPoint IConnectionSocket.RemoteHost => RemoteHost;

        private DataReader _Reader;
        private DataWriter _Writer;
        private Task _ReceiveLoopTask;
        private bool _InternalConstructor;

        public BluetoothAccessPoint LocalHost { get; private set; }
        public BluetoothAccessPoint RemoteHost { get; private set; }

        public StreamSocket Socket { get; protected set; }
        public bool IsActive { get; protected set; }
        public event ConnectionInterruptHandler ConnectionInterrupt;
        public event ConnectionSocketMessageReceiveHandler MessageReceived;

        internal BluetoothConnectionSocket(StreamSocket socket, BluetoothAccessPoint localhost, BluetoothAccessPoint remotehost)
        {
            Socket = socket;
            LocalHost = localhost;
            RemoteHost = remotehost;
            IsActive = false;
            _InternalConstructor = true;
        }

        [StandardRealization]
        public BluetoothConnectionSocket(BluetoothAccessPoint remotehost)
        {
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
                            await Socket.ConnectAsync(RemoteHost.Address.ToHostName(), port);
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
