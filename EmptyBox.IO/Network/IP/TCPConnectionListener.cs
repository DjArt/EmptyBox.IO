using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using EmptyBox.IO.Interoperability;
using System.Net;
using EmptyBox.ScriptRuntime.Results;
using EmptyBox.IO.Network.Help;

namespace EmptyBox.IO.Network.IP
{
    public sealed class TCPConnectionListener : APointedConnectionListener<IPAddress, IPPort, IPAccessPoint, ITCPConnectionProvider>
    {
        private Task _ReceiveLoop;
        
        public Socket Socket { get; private set; }

        public TCPConnectionListener(IPAccessPoint localhost)
        {
            IsActive = false;
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(localhost.ToIPEndPoint());
        }

        public override async Task<VoidResult<SocketOperationStatus>> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    Socket.Listen(512);
                    IsActive = true;
                    _ReceiveLoop = Task.Run((Action)ReceiveLoop);
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
                    Socket.Listen(0);
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

        private async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    Socket received = Socket.Accept();
                    TCPConnection tcpsocket = new TCPConnection(received, (received.RemoteEndPoint as IPEndPoint).ToIPAccessPoint());
                    await tcpsocket.Open();
                    OnConnectionReceive(tcpsocket);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
