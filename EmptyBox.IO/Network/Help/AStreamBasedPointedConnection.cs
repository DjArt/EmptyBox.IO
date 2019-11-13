using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Help
{
    public abstract class AStreamBasedPointedConnection<TAddress, TPort, TPointedConnectionProvider> : APointedConnection<TAddress, TPort, TPointedConnectionProvider>, IStreamBasedConnection
        where TAddress : IAddress
        where TPort : IPort
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
        public override async Task<bool> Close()
        {
            await Task.Yield();
            if (IsActive)
            {
                OnConnectionInterrupt();
                IsActive = false;
                ReceiveLoopTask.Wait(100);
                Input.Dispose();
                Output.Dispose();
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
                GetStreams(out Stream? input, out Stream? output);
                Input = input;
                Output = output;
                ReceiveLoopTask = Task.Run(ReceiveLoop);
                return true;
            }
            return false;
        }

        public override async Task<bool> Send(byte[] data)
        {
            await Task.Yield();
            if (IsActive)
            {
                Output.Write(data, 0, data.Length);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
