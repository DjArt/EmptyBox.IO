using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected BinaryReader Input { get; set; }
        protected BinaryWriter Output { get; set; }
        protected Task ReceiveLoopTask { get; set; }
        protected bool ReceivedConnection { get; set; }
        protected Stream Stream { get; set; }
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
        protected abstract (BinaryReader Input, BinaryWriter Output) GetPair();
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
                        byte[] buffer = Input.ReadBytes(count);
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
                    (BinaryReader Input, BinaryWriter Output) pair = GetPair();
                    Input = pair.Input;
                    Output = pair.Output;
                    ReceiveLoopTask = Task.Run((Action)ReceiveLoop);
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            return SocketOperationStatus.UnknownError;
        }

        public async Task<SocketOperationStatus> Send(byte[] data)
        {
            await Task.Yield();
            try
            {
                Output.Write(data.Length);
                Output.Write(data);
                try
                {
                    Output.Flush();
                    return SocketOperationStatus.Success;
                }
                catch
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