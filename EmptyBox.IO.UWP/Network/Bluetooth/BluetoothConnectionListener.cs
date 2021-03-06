﻿using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth.Classic;
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothConnectionListener : IBluetoothConnectionListener
    {
        event PointedConnectionReceiveHandler<IBluetoothDevice> IPointedConnectionListener<IBluetoothDevice>.ConnectionReceived
        {
            add => _ConnectionReceived2 += value;
            remove => _ConnectionReceived2 -= value;
        }

        event ConnectionReceiveHandler<BluetoothPort> IConnectionListener<BluetoothPort>.ConnectionReceived
        {
            add => _ConnectionReceived1 += value;
            remove => _ConnectionReceived1 -= value;
        }

        event ConnectionReceiveHandler IConnectionListener.ConnectionReceived
        {
            add => _ConnectionReceived0 += value;
            remove => _ConnectionReceived0 -= value;
        }

        event PointedConnectionReceiveHandler<IBluetoothDevice, BluetoothPort> IPointedConnectionListener<IBluetoothDevice, BluetoothPort>.ConnectionReceived
        {
            add => ConnectionReceived += value;
            remove => ConnectionReceived -= value;
        }

        IPointedConnectionProvider<IBluetoothDevice, BluetoothPort> IPointedConnectionListener<IBluetoothDevice, BluetoothPort>.ConnectionProvider => ConnectionProvider;
        IAccessPoint<IBluetoothDevice, BluetoothPort> IPointedConnectionListener<IBluetoothDevice, BluetoothPort>.ListenerPoint => ListenerPoint;

        IPointedConnectionProvider<IBluetoothDevice> IPointedConnectionListener<IBluetoothDevice>.ConnectionProvider => ConnectionProvider;
        IBluetoothDevice IPointedConnectionListener<IBluetoothDevice>.ListenerPoint => ListenerPoint.Address;

        IConnectionProvider<BluetoothPort> IConnectionListener<BluetoothPort>.ConnectionProvider => ConnectionProvider;
        BluetoothPort IConnectionListener<BluetoothPort>.ListenerPoint => ListenerPoint.Port;

        IBluetoothConnectionProvider IBluetoothConnectionListener.ConnectionProvider => ConnectionProvider;

        IConnectionProvider IConnectionListener.ConnectionProvider => ConnectionProvider;

        #region Private events
        private event ConnectionReceiveHandler _ConnectionReceived0;
        private event ConnectionReceiveHandler<BluetoothPort> _ConnectionReceived1;
        private event PointedConnectionReceiveHandler<IBluetoothDevice> _ConnectionReceived2;
        #endregion

        #region Private objects
        private StreamSocketListener _ConnectionListener { get; set; }
        private RfcommServiceProvider _ServiceProvider { get; set; }
        #endregion

        #region Public events
        public event PointedConnectionReceiveHandler<IBluetoothDevice, BluetoothPort> ConnectionReceived;
        #endregion

        #region Public objects
        public BluetoothAdapter ConnectionProvider { get; }
        public BluetoothClassicAccessPoint ListenerPoint { get; }
        public bool IsActive { get; private set; }
        #endregion

        #region Constructors
        public BluetoothConnectionListener(BluetoothAdapter adapter, BluetoothPort port)
        {
            ConnectionProvider = adapter;
            ListenerPoint = new BluetoothClassicAccessPoint(adapter, port);
            IsActive = false;
            _ConnectionListener = new StreamSocketListener();
            _ConnectionListener.ConnectionReceived += _ConnectionListener_ConnectionReceived;
        }
        #endregion

        #region Private functions
        private async void _ConnectionListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            IBluetoothDevice device = await ConnectionProvider.TryGetFromMAC(args.Socket.Information.RemoteHostName.ToMACAddress());
            if (device != null)
            {
                BluetoothClassicAccessPoint remotehost = new BluetoothClassicAccessPoint(device, new BluetoothPort(Guid.Parse(args.Socket.Information.RemoteServiceName)));
                BluetoothConnection connection = new BluetoothConnection(ConnectionProvider, args.Socket, ListenerPoint.Port, remotehost);
                ConnectionReceived?.Invoke(this, connection);
                _ConnectionReceived0?.Invoke(this, connection);
                _ConnectionReceived1?.Invoke(this, connection);
                _ConnectionReceived2?.Invoke(this, connection);
            }
        }
        #endregion

        #region Public functions
        public async Task<bool> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                _ServiceProvider = await RfcommServiceProvider.CreateAsync(ListenerPoint.Port.ToRfcommServiceID());
                await _ConnectionListener.BindServiceNameAsync(ListenerPoint.Port.ToRfcommServiceID().AsString());
                _ServiceProvider.StartAdvertising(_ConnectionListener);
                IsActive = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Stop()
        {
            await Task.Yield();
            if (IsActive)
            {
                _ServiceProvider.StopAdvertising();
                IsActive = false;
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