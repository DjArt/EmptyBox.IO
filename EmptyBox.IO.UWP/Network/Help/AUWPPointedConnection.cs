using EmptyBox.ScriptRuntime.Results;
using EmptyBox.IO.Network.Help;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network.Help
{
    public abstract class AUWPPointedConnection<TAddress, TPort, TAccessPoint, TPointedConnectionProvider> : APointedConnection<TAddress, TPort, TAccessPoint, TPointedConnectionProvider>
        where TAddress : IAddress
        where TPort : IPort
        where TAccessPoint : IAccessPoint<TAddress, TPort>
        where TPointedConnectionProvider : IPointedConnectionProvider<TAddress, TPort>
    {

        #region Protected objects
        protected DataReader Input { get; set; }
        protected DataWriter Output { get; set; }
        protected Task ReceiveLoopTask { get; set; }
        protected bool ReceivedConnection { get; set; }
        protected StreamSocket Stream { get; set; }
        #endregion

        #region Public objects

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
                    ReceiveLoopTask.Wait(100);
                    Stream.Dispose();
                    ReceivedConnection = false;
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
                    if (!ReceivedConnection)
                    {
                        Stream = new StreamSocket();
                        (HostName Name, string Port) pair = ConvertAccessPoint(RemotePoint);
                        if (!string.IsNullOrWhiteSpace(pair.Port))
                        {
                            Stream.ConnectAsync(pair.Name, pair.Port).AsTask().Wait();
                            //TODO Port = Stream.Information.LocalPort;
                        }
                        else
                        {
                            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ConnectionIsAlreadyClosed, null);
                        }
                    }
                    Input = new DataReader(Stream.InputStream);
                    Output = new DataWriter(Stream.OutputStream);
                    ReceiveLoopTask = Task.Run((Action)ReceiveLoop);
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    switch (SocketError.GetStatus(ex.HResult))
                    {
                        default:
                            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, null);
                    }
                }
            }
            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, null);
        }

        public override async Task<VoidResult<SocketOperationStatus>> Send(byte[] data)
        {
            await Task.Yield();
            try
            {
                Output.WriteInt32(data.Length);
                Output.WriteBytes(data);
                if (await Output.FlushAsync())
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
        #endregion
    }
}