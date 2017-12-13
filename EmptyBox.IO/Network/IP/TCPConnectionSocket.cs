using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using EmptyBox.IO.Interoperability;

namespace EmptyBox.IO.Network.IP
{
    public class TCPConnectionSocket : IConnectionSocket
    {
        IAccessPoint IConnectionSocket.LocalHost => LocalHost;
        IAccessPoint IConnectionSocket.RemoteHost => RemoteHost;

        private Task _ReceiveLoopTask;
        private bool _InternalConstructor;

        public IPAccessPoint LocalHost { get; private set; }
        public IPAccessPoint RemoteHost { get; private set; }

        public Socket Socket { get; protected set; }
        public bool IsActive { get; protected set; }
        public event ConnectionInterruptHandler ConnectionInterrupt;
        public event ConnectionSocketMessageReceiveHandler MessageReceived;

        internal TCPConnectionSocket(Socket socket, IPAccessPoint remotehost, IPAccessPoint localhost)
        {
            Socket = socket;
            RemoteHost = remotehost;
            LocalHost = localhost;
            IsActive = false;
            _InternalConstructor = true;
        }

        public TCPConnectionSocket(IPAccessPoint remotehost)
        {
            RemoteHost = remotehost;
            IsActive = false;
            _InternalConstructor = false;
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        private async void ReceiveLoop()
        {
            await Task.Yield();
            byte[] buffer = new byte[4096];
            while (IsActive)
            {
                try
                {
                    int count = Socket.Receive(buffer);
                    if (count > 0)
                    {
                        byte[] newbuffer = new byte[count];
                        Array.Copy(buffer, newbuffer, count);
                        MessageReceived?.Invoke(this, newbuffer);
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
                    return SocketOperationStatus.Success;
                }
                catch(Exception ex)
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
            //await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    if (!_InternalConstructor)
                    {
                        Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                        Socket.Connect(RemoteHost.ToIPEndPoint());
                    }
                    IsActive = true;
                    _ReceiveLoopTask = Task.Run((Action)ReceiveLoop);
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            else
            {
                return SocketOperationStatus.ConnectionIsAlreadyOpen;
            }
        }

        public async Task<SocketOperationStatus> Send(byte[] data)
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    int count = Socket.Send(data);
                    if (count == data.Length)
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
            else
            {
                return SocketOperationStatus.ConnectionIsClosed;
            }
        }
    }
}
