using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using EmptyBox.IO.Interoperability;
using EmptyBox.ScriptRuntime.Results;
using EmptyBox.IO.Network.Help;
using System.Net;

namespace EmptyBox.IO.Network.IP
{
    public sealed class TCPConnection : APointedConnection<IPAddress, IPPort, IPAccessPoint, ITCPConnectionProvider>, ITCPConnection
    {
        //ITCPConnectionProvider ITCPConnection.ConnectionProvider => ConnectionProvider;

        private Task _ReceiveLoopTask;
        private bool _InternalConstructor;

        public Socket Socket { get; private set; }

        internal TCPConnection(Socket socket, IPAccessPoint remotePoint)
        {
            Socket = socket;
            LocalPoint = (Socket.LocalEndPoint as IPEndPoint).ToIPAccessPoint();
            RemotePoint = remotePoint;
            IsActive = false;
            _InternalConstructor = true;
        }

        public TCPConnection(IPAccessPoint remotePoint)
        {
            RemotePoint = remotePoint;
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
                        OnMessageReceive(newbuffer);
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
                    Socket.Dispose();
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                }
                catch(Exception ex)
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
                    if (!_InternalConstructor)
                    {
                        Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                        Socket.Connect(RemotePoint.ToIPEndPoint());
                        LocalPoint = (Socket.LocalEndPoint as IPEndPoint).ToIPAccessPoint();
                    }
                    IsActive = true;
                    _ReceiveLoopTask = Task.Run(ReceiveLoop);
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
            if (IsActive)
            {
                try
                {
                    int count = Socket.Send(data);
                    if (count == data.Length)
                    {
                        return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                    }
                    else
                    {
                        return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, null);
                    }
                }
                catch (Exception ex)
                {
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
                }
            }
            else
            {
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ConnectionIsClosed, null);
            }
        }
    }
}
