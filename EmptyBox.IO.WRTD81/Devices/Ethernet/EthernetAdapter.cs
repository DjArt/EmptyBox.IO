using System;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Ethernet;
using EmptyBox.IO.Network.IP;
using Windows.Networking.Connectivity;

namespace EmptyBox.IO.Devices.Ethernet
{
    public sealed class EthernetAdapter : IEthernetAdapter
    {
        #region IEthernetAdapter interface properties
        ITCPConnectionProvider IEthernetAdapter.TCPProvider => throw new NotImplementedException();
        IUDPSocketProvider IEthernetAdapter.UDPProvider => throw new NotImplementedException();
        IEthernetSocketProvider IEthernetAdapter.EthernetProvider => throw new NotImplementedException();
        #endregion

        #region Private objects
        private NetworkAdapter Adapter;
        #endregion

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public event DeviceConnectionStatusHandler ConnectionStatusEvent;

        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        public MACAddress HardwareAddress => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public ConnectionStatus NetworkStatus => throw new NotImplementedException();
        public ulong MaxInboundSpeed => Adapter.InboundMaxBitsPerSecond;
        public ulong MaxOutboundSpeed => Adapter.OutboundMaxBitsPerSecond;
        #endregion

        #region Constructors
        internal EthernetAdapter(NetworkAdapter adapter)
        {
            Adapter = adapter;
        }
        #endregion

        #region Public functions
        public void Dispose()
        {

        }
        #endregion
    }
}