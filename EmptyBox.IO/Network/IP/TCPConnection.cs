using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Help;
using System.Net;

namespace EmptyBox.IO.Network.IP
{
    public sealed class TCPConnection : APointedConnection<IPAddress, IPPort, ITCPConnectionProvider>, ITCPConnection
    {
        //ITCPConnectionProvider ITCPConnection.ConnectionProvider => ConnectionProvider;

        private Task? _ReceiveLoopTask;
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
                    else if (Socket.Poll(1, SelectMode.SelectRead) && Socket.Available == 0)
                    {
                        await Close();
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

        public override async Task<bool> Close()
        {
            await Task.Yield();
            if (IsActive)
            {
                OnConnectionInterrupt();
                IsActive = false;
                _ReceiveLoopTask?.Wait(100);
                Socket.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override async Task<bool> Open()
        {
            await Task.Yield();
            if (!IsActive)
            {
                if (!_InternalConstructor)
                {
                    Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                    Socket.Connect(RemotePoint.ToIPEndPoint());
                    LocalPoint = (Socket.LocalEndPoint as IPEndPoint).ToIPAccessPoint();
                }
                IsActive = true;
                _ReceiveLoopTask = Task.Factory.StartNew(ReceiveLoop, TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override async Task<bool> Send(byte[] data)
        {
            await Task.Yield();
            if (IsActive)
            {
                int count = Socket.Send(data);
                if (count == data.Length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
