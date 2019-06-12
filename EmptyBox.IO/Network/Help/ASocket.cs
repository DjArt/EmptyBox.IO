using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Network.Help
{
    public abstract class ASocket<TSocketProvider> : ISocket
        where TSocketProvider : ISocketProvider
    {
        ISocketProvider ISocket.SocketProvider => SocketProvider;

        public event MessageReceiveHandler<ICommunicationElement> MessageReceived;

        public virtual bool IsActive { get; protected set; }
        public virtual TSocketProvider SocketProvider { get; protected set; }


        protected virtual void OnMessageReceive(byte[] message)
        {
            MessageReceived?.Invoke(this, message);
        }

        public abstract Task<VoidResult<SocketOperationStatus>> Close();
        public abstract Task<VoidResult<SocketOperationStatus>> Open();
        public abstract Task<VoidResult<SocketOperationStatus>> Send(byte[] data);
    }

    public abstract class ASocket<TPort, TSocketProvider> : ASocket<TSocketProvider>, ISocket<TPort>
        where TPort : IPort
        where TSocketProvider : ISocketProvider<TPort>
    {
        ISocketProvider<TPort> ISocket<TPort>.SocketProvider => SocketProvider;

        public new event SocketMessageReceiveHandler<TPort> MessageReceived;

        public TPort LocalPoint { get; protected set; }

        protected void OnMessageReceived(TPort sender, byte[] message)
        {
            MessageReceived?.Invoke(this, sender, message);
            base.OnMessageReceive(message);
        }

        public override Task<VoidResult<SocketOperationStatus>> Send(byte[] data) => throw new NotSupportedException();
        public abstract Task<VoidResult<SocketOperationStatus>> Send(IPort receiver, byte[] data);
    }
}
