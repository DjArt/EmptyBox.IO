using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Network.Help
{
    public abstract class APointedSocket<TAddress, TPointedSocketProvider> : ASocket<TPointedSocketProvider>, IPointedSocket<TAddress>
        where TAddress : IAddress
        where TPointedSocketProvider : IPointedSocketProvider<TAddress>
    {
        IPointedSocketProvider<TAddress> IPointedSocket<TAddress>.SocketProvider => SocketProvider;

        public new event PointedSocketMessageReceiveHandler<TAddress> MessageReceived;

        public TAddress LocalPoint { get; protected set; }

        protected void OnMessageReceived(TAddress sender, byte[] message)
        {
            MessageReceived?.Invoke(this, sender, message);
            base.OnMessageReceived(message);
        }

        public override Task<VoidResult<SocketOperationStatus>> Send(byte[] data) => throw new NotSupportedException();
        public abstract Task<VoidResult<SocketOperationStatus>> Send(IAddress receiver, byte[] data);
    }

    public abstract class APointedSocket<TAddress, TPort, TAccessPoint, TPointedSocketProvider> : APointedSocket<TAddress, TPointedSocketProvider>, IPointedSocket<TAddress, TPort>
        where TAddress : IAddress
        where TPort : IPort
        where TAccessPoint : IAccessPoint<TAddress, TPort>
        where TPointedSocketProvider : IPointedSocketProvider<TAddress, TPort>
    {
        IAccessPoint<TAddress, TPort> IPointedSocket<TAddress, TPort>.LocalPoint => LocalPoint;
        IPointedSocketProvider<TAddress, TPort> IPointedSocket<TAddress, TPort>.SocketProvider => SocketProvider;

        event SocketMessageReceiveHandler<TPort> ISocket<TPort>.MessageReceived
        {
            add => _MessageReceived += value;
            remove => _MessageReceived -= value;
        }

        TPort ISocket<TPort>.LocalPoint => LocalPoint.Port;
        ISocketProvider<TPort> ISocket<TPort>.SocketProvider => SocketProvider;

        private event SocketMessageReceiveHandler<TPort> _MessageReceived;

        public new event PointedSocketMessageReceiveHandler<TAddress, TPort> MessageReceived;

        private TAccessPoint _LocalPoint;

        public new TAccessPoint LocalPoint
        {
            get => _LocalPoint;
            set
            {
                _LocalPoint = value;
                base.LocalPoint = _LocalPoint.Address;
            }
        }

        protected void OnMessageReceived(TAccessPoint sender, byte[] message)
        {
            MessageReceived?.Invoke(this, sender, message);
            _MessageReceived?.Invoke(this, sender.Port, message);
            base.OnMessageReceived(message);
        }

        public override Task<VoidResult<SocketOperationStatus>> Send(IAddress receiver, byte[] data) => throw new NotSupportedException();
        public virtual Task<VoidResult<SocketOperationStatus>> Send(IPort receiver, byte[] data) => throw new NotSupportedException();
        public abstract Task<VoidResult<SocketOperationStatus>> Send(IAccessPoint<IAddress, IPort> receiver, byte[] data);
    }
}
