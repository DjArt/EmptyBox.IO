using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Help
{
    public abstract class AStreamBasedPointedConnection<TAddress, TPort, TAccessPoint, TPointedConnectionProvider> : APointedConnection<TAddress, TPort, TAccessPoint, TPointedConnectionProvider>, IStreamBasedConnection
        where TAddress : IAddress
        where TPort : IPort
        where TAccessPoint : IAccessPoint<TAddress, TPort>
        where TPointedConnectionProvider : IPointedConnectionProvider<TAddress, TPort>
    {

        #region Protected objects
        protected Task? ReceiveLoopTask { get; set; }
        #endregion

        #region Public objects
        public Stream? Input { get; private set; }
        public Stream? Output { get; private set; }
        #endregion

        #region Protected functions
        protected abstract void GetStreams(out Stream? input, out Stream? output);

        protected async void ReceiveLoop()
        {
            await Task.Yield();
            byte[] buffer = new byte[4096];
            while (IsActive)
            {
                try
                {
                    int count = Input.Read(buffer, 0, buffer.Length);
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
                    Input.Dispose();
                    Output.Dispose();
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
                    GetStreams(out Stream? input, out Stream? output);
                    Input = input;
                    Output = output;
                    ReceiveLoopTask = Task.Run(ReceiveLoop);
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);

                }
                catch (Exception ex)
                {
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
                }
            }
            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, null);
        }

        public override async Task<VoidResult<SocketOperationStatus>> Send(byte[] data)
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    Output.Write(data, 0, data.Length);
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
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
        #endregion
    }
}
