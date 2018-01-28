using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network
{
    public abstract class AConnection<TAccessPoint, TPort, TConnectionProvider> : IConnection where TAccessPoint : IAccessPoint where TPort : IPort where TConnectionProvider : IConnectionProvider
    {
        #region IConnection interface properties
        IConnectionProvider IConnection.ConnectionProvider => ConnectionProvider;
        IPort IConnection.Port => Port;
        IAccessPoint IConnection.RemoteHost => RemoteHost;
        #endregion

        #region Protected objects
        protected DataReader Input { get; set; }
        protected DataWriter Output { get; set; }
        protected Task ReceiveLoopTask { get; set; }
        protected bool ReceivedConnection { get; set; }
        protected StreamSocket Stream { get; set; }
        #endregion

        #region Public objects
        public event ConnectionMessageReceiveHandler MessageReceived;
        public event ConnectionInterruptHandler ConnectionInterrupt;

        public TConnectionProvider ConnectionProvider { get; protected set; }
        public TPort Port { get; protected set; }
        public TAccessPoint RemoteHost { get; protected set; }
        public bool IsActive { get; protected set; }
        #endregion

        #region Protected abstract functions
        protected abstract (HostName Name, string Port) ConvertAccessPoint(TAccessPoint point);
        #endregion

        #region Protected functions
        protected async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    int count = Input.ReadInt32();
                    if (count > 0)
                    {
                        byte[] buffer = new byte[count];
                        Input.ReadBytes(buffer);
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
        #endregion

        #region Public functions
        public async Task<SocketOperationStatus> Close()
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    ConnectionInterrupt?.Invoke(this);
                    IsActive = false;
                    ReceiveLoopTask.Wait(100);
                    Stream.Dispose();
                    ReceivedConnection = false;
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
                    if (!ReceivedConnection)
                    {
                        Stream = new StreamSocket();
                        (HostName Name, string Port) pair = ConvertAccessPoint(RemoteHost);
                        if (pair.Port != String.Empty)
                        {
                            Stream.ConnectAsync(pair.Name, pair.Port).AsTask().Wait();
                            //TODO Port = Stream.Information.LocalPort;
                        }
                        else
                        {
                            return SocketOperationStatus.ConnectionIsAlreadyClosed;
                        }
                    }
                    Input = new DataReader(Stream.InputStream);
                    Output = new DataWriter(Stream.OutputStream);
                    ReceiveLoopTask = Task.Run((Action)ReceiveLoop);
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
                Output.WriteInt32(data.Length);
                Output.WriteBytes(data);
                if (await Output.FlushAsync())
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
        #endregion
    }
}