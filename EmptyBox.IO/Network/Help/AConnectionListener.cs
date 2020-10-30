using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Help
{
    public abstract class AConnectionListener<TConnectionProvider> : IConnectionListener
        where TConnectionProvider : IConnectionProvider
    {
        IConnectionProvider IConnectionListener.ConnectionProvider => ConnectionProvider;

        public event ConnectionReceiveHandler? ConnectionReceived;

        public virtual TConnectionProvider ConnectionProvider { get; protected set; }
        public virtual bool IsActive { get; protected set; }

        protected virtual void OnConnectionReceive(IConnection connection)
        {
            ConnectionReceived?.Invoke(this, connection);
        }

        public abstract Task<bool> Start();
        public abstract Task<bool> Stop();
    }

    public abstract class AConnectionListener<TPort, TConnectionProvider> : AConnectionListener<TConnectionProvider>, IConnectionListener<TPort>
        where TPort : IPort
        where TConnectionProvider : IConnectionProvider<TPort>
    {
        IConnectionProvider<TPort> IConnectionListener<TPort>.ConnectionProvider => ConnectionProvider;

        public new event ConnectionReceiveHandler<TPort>? ConnectionReceived;

        public virtual TPort ListenerPoint { get; protected set; }

        protected void OnConnectionReceive(IConnection<TPort> connection)
        {
            ConnectionReceived?.Invoke(this, connection);
            base.OnConnectionReceive(connection);
        }
    }
}
