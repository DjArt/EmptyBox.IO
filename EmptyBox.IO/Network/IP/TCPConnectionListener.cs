using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using EmptyBox.IO.Interoperability;
using System.Net;
using EmptyBox.IO.Devices.Ethernet;

namespace EmptyBox.IO.Network.IP
{
    public class TCPConnectionListener : IConnectionListener
    {

        private Task _ReceiveLoop;
        
        public bool IsActive { get; protected set; }
        public event ConnectionReceivedDelegate ConnectionReceived;
        public Socket Socket { get; protected set; }

        public IEthernetAdapter ConnectionProvider => throw new NotImplementedException();

        public IPPort Port => throw new NotImplementedException();

        IConnectionProvider IConnectionListener.ConnectionProvider => throw new NotImplementedException();

        IPort IConnectionListener.Port => throw new NotImplementedException();

        public TCPConnectionListener(IPAccessPoint localhost)
        {
            IsActive = false;
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(localhost.ToIPEndPoint());
        }

        public async Task<SocketOperationStatus> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    Socket.Listen(512);
                    IsActive = true;
                    _ReceiveLoop = Task.Run((Action)ReceiveLoop);
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
                    Socket.Listen(0);
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
                    ConnectionReceived?.Invoke(this, tcpsocket);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
