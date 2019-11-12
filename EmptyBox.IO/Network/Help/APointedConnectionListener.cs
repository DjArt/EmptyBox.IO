using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Help
{
    public abstract class APointedConnectionListener<TAddress, TPointedConnectionProvider> : AConnectionListener<TPointedConnectionProvider>, IPointedConnectionListener<TAddress>
        where TAddress : IAddress
        where TPointedConnectionProvider : IPointedConnectionProvider<TAddress>
    {
        IPointedConnectionProvider<TAddress> IPointedConnectionListener<TAddress>.ConnectionProvider => ConnectionProvider;

        public new event PointedConnectionReceiveHandler<TAddress> ConnectionReceived;

        public TAddress ListenerPoint { get; protected set; }

        protected virtual void OnConnectionReceive(IPointedConnection<TAddress> connection)
        {
            ConnectionReceived?.Invoke(this, connection);
            base.OnConnectionReceive(connection);
        }
    }

    public abstract class APointedConnectionListener<TAddress, TPort, TPointedConnectionProvider> : APointedConnectionListener<TAddress, TPointedConnectionProvider>, IPointedConnectionListener<TAddress, TPort>
        where TAddress : IAddress
        where TPort : IPort
        where TPointedConnectionProvider : IPointedConnectionProvider<TAddress, TPort>
    {
        event ConnectionReceiveHandler<TPort> IConnectionListener<TPort>.ConnectionReceived
        {
            add => _ConnectionReceived += value;
            remove => _ConnectionReceived -= value;
        }

        IPointedConnectionProvider<TAddress, TPort> IPointedConnectionListener<TAddress, TPort>.ConnectionProvider => ConnectionProvider;
        IAccessPoint<TAddress, TPort> IPointedConnectionListener<TAddress, TPort>.ListenerPoint => ListenerPoint;

        IConnectionProvider<TPort> IConnectionListener<TPort>.ConnectionProvider => ConnectionProvider;
        TPort IConnectionListener<TPort>.ListenerPoint => ListenerPoint.Port;

        private event ConnectionReceiveHandler<TPort> _ConnectionReceived;

        public new event PointedConnectionReceiveHandler<TAddress, TPort> ConnectionReceived;

        private IAccessPoint<TAddress, TPort> _ListenerPoint;

        public new IAccessPoint<TAddress, TPort> ListenerPoint
        {
            get => _ListenerPoint;
            set
            {
                _ListenerPoint = value;
                base.ListenerPoint = _ListenerPoint.Address;
            }
        }

        protected void OnConnectionReceive(IPointedConnection<TAddress, TPort> connection)
        {
            ConnectionReceived?.Invoke(this, connection);
            _ConnectionReceived?.Invoke(this, connection);
            base.OnConnectionReceive(connection);
        }
    }
}
