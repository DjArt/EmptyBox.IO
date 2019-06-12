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
    public abstract class AUWPPointedConnection<TAddress, TPort, TAccessPoint, TPointedConnectionProvider> : AStreamBasedPointedConnection<TAddress, TPort, TAccessPoint, TPointedConnectionProvider>
        where TAddress : IAddress
        where TPort : IPort
        where TAccessPoint : IAccessPoint<TAddress, TPort>
        where TPointedConnectionProvider : IPointedConnectionProvider<TAddress, TPort>
    {

        #region Protected objects
        protected bool ReceivedConnection { get; set; }
        protected StreamSocket Stream { get; set; }
        #endregion

        #region Public objects

        #endregion

        #region Protected abstract functions
        protected abstract (HostName Name, string Port) ConvertAccessPoint(TAccessPoint point);
        #endregion

        #region Protected functions
        
        #endregion

        #region Public functions
        public override async Task<VoidResult<SocketOperationStatus>> Close()
        {
            return await base.Close();
        }

        protected override void GetStreams(out Stream input, out Stream output)
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
                    throw new Exception();
                }
            }
            input = Stream.InputStream.AsStreamForRead();
            output = Stream.OutputStream.AsStreamForWrite();
        }

        //public override async Task<VoidResult<SocketOperationStatus>> Open()
        //{
        //    await Task.Yield();
        //    if (!IsActive)
        //    {
        //        try
        //        {
        //            if (!ReceivedConnection)
        //            {
        //                Stream = new StreamSocket();
        //                (HostName Name, string Port) pair = ConvertAccessPoint(RemotePoint);
        //                if (!string.IsNullOrWhiteSpace(pair.Port))
        //                {
        //                    Stream.ConnectAsync(pair.Name, pair.Port).AsTask().Wait();
        //                    //TODO Port = Stream.Information.LocalPort;
        //                }
        //                else
        //                {
        //                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ConnectionIsAlreadyClosed, null);
        //                }
        //            }
        //            Input = new DataReader(Stream.InputStream);
        //            Output = new DataWriter(Stream.OutputStream);
        //            ReceiveLoopTask = Task.Run(ReceiveLoop);
        //            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            switch (SocketError.GetStatus(ex.HResult))
        //            {
        //                default:
        //                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, null);
        //            }
        //        }
        //    }
        //    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, null);
        //}
        #endregion
    }
}