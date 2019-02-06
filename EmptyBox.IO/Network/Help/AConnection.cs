﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Network.Help
{
    public abstract class AConnection<TConnectionProvider> : IConnection
        where TConnectionProvider : IConnectionProvider
    {
        event MessageReceiveHandler<ICommunicationElement> ICommunicationElement.MessageReceived
        {
            add => MessageReceived += value;
            remove => MessageReceived -= value;
        }

        IConnectionProvider IConnection.ConnectionProvider => ConnectionProvider;

        public event ConnectionInterruptHandler<IConnection> ConnectionInterrupted;
        public event MessageReceiveHandler<IConnection> MessageReceived;

        public virtual TConnectionProvider ConnectionProvider { get; protected set; }
        public virtual bool IsActive { get; protected set; }

        protected virtual void OnConnectionInterrupt()
        {
            ConnectionInterrupted?.Invoke(this);
        }

        protected virtual void OnMessageReceive(byte[] message)
        {
            MessageReceived?.Invoke(this, message);
        }

        public abstract Task<VoidResult<SocketOperationStatus>> Close();
        public abstract Task<VoidResult<SocketOperationStatus>> Open();
        public abstract Task<VoidResult<SocketOperationStatus>> Send(byte[] data);
    }

    public abstract class AConnection<TPort, TConnectionProvider, TInherited> : AConnection<TConnectionProvider>, IConnection<TPort>
        where TPort : IPort
        where TConnectionProvider : IConnectionProvider<TPort>
        where TInherited : AConnection<TPort, TConnectionProvider, TInherited>
    {
        IConnectionProvider<TPort> IConnection<TPort>.ConnectionProvider => ConnectionProvider;

        public new event MessageReceiveHandler<IConnection<TPort>> MessageReceived;
        public new event ConnectionInterruptHandler<IConnection<TPort>> ConnectionInterrupted;

        public TPort LocalPoint { get; protected set; }
        public TPort RemotePoint { get; protected set; }

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
