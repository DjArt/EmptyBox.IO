using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Help
{
    public abstract class APointedConnection<TAddress, TPointedConnectionProvider> : AConnection<TPointedConnectionProvider>, IPointedConnection<TAddress>
        where TAddress : IAddress
        where TPointedConnectionProvider : IPointedConnectionProvider<TAddress>
    {
        IPointedConnectionProvider<TAddress> IPointedConnection<TAddress>.ConnectionProvider => ConnectionProvider;

        public new event MessageReceiveHandler<IPointedConnection<TAddress>> MessageReceived;
        public new event ConnectionInterruptHandler<IPointedConnection<TAddress>> ConnectionInterrupted;

        public TAddress LocalPoint { get; protected set; }
        public TAddress RemotePoint { get; protected set; }

        protected override void OnConnectionInterrupt()
        {
            ConnectionInterrupted?.Invoke(this);
            base.OnConnectionInterrupt();
        }

        protected override void OnMessageReceive(byte[] message)
        {
            MessageReceived?.Invoke(this, message);
            base.OnMessageReceive(message);
        }
    }

    public abstract class APointedConnection<TAddress, TPort, TAccessPoint, TPointedConnectionProvider> : APointedConnection<TAddress, TPointedConnectionProvider>, IPointedConnection<TAddress, TPort>
        where TAddress : IAddress
        where TPort : IPort
        where TAccessPoint : IAccessPoint<TAddress, TPort>
        where TPointedConnectionProvider : IPointedConnectionProvider<TAddress, TPort>
    {
        IPointedConnectionProvider<TAddress, TPort> IPointedConnection<TAddress, TPort>.ConnectionProvider => ConnectionProvider;
        IAccessPoint<TAddress, TPort> IPointedConnection<TAddress, TPort>.LocalPoint => LocalPoint;
        IAccessPoint<TAddress, TPort> IPointedConnection<TAddress, TPort>.RemotePoint => RemotePoint;

        event MessageReceiveHandler<IConnection<TPort>> IConnection<TPort>.MessageReceived
        {
            add => MessageReceived += value;
            remove => MessageReceived -= value;
        }
        event ConnectionInterruptHandler<IConnection<TPort>> IConnection<TPort>.ConnectionInterrupted
        {
            add => ConnectionInterrupted += value;
            remove => ConnectionInterrupted -= value;
        }

        IConnectionProvider<TPort> IConnection<TPort>.ConnectionProvider => ConnectionProvider;
        TPort IConnection<TPort>.LocalPoint => LocalPoint.Port;
        TPort IConnection<TPort>.RemotePoint => RemotePoint.Port;

        public new event MessageReceiveHandler<IPointedConnection<TAddress, TPort>> MessageReceived;
        public new event ConnectionInterruptHandler<IPointedConnection<TAddress, TPort>> ConnectionInterrupted;

        private TAccessPoint _LocalPoint;
        private TAccessPoint _RemotePoint;

        public new TAccessPoint LocalPoint
        {
            get => _LocalPoint;
            set
            {
                _LocalPoint = value;
                base.LocalPoint = _LocalPoint.Address;
            }
        }
        public new TAccessPoint RemotePoint
        {
            get => _RemotePoint;
            set
            {
                _RemotePoint = value;
                base.RemotePoint = _RemotePoint.Address;
            }
        }

        protected override void OnConnectionInterrupt()
        {
            ConnectionInterrupted?.Invoke(this);
            base.OnConnectionInterrupt();
        }

        protected override void OnMessageReceive(byte[] message)
        {
            MessageReceived?.Invoke(this, message);
            base.OnMessageReceive(message);
        }
    }
}
