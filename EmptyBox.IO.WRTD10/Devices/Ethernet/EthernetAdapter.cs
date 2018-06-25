using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Ethernet;
using EmptyBox.IO.Network.IP;
using Windows.Networking.Connectivity;

namespace EmptyBox.IO.Devices.Ethernet
{
    public sealed class EthernetAdapter : IEthernetAdapter
    {
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public MACAddress HardwareAddress => throw new NotImplementedException();
        public ConnectionStatus NetworkStatus => throw new NotImplementedException();
        public ulong MaxInboundSpeed => InternalAdapter.InboundMaxBitsPerSecond;
        public ulong MaxOutboundSpeed => InternalAdapter.OutboundMaxBitsPerSecond;
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public NetworkAdapter InternalAdapter { get; private set; }

        public IEnumerable<ITCPConnectionProvider> TCPProviders => throw new NotImplementedException();

        public IEnumerable<IUDPSocketProvider> UDPProviders => throw new NotImplementedException();

        public IEthernetSocketProvider EthernetProvider => throw new NotImplementedException();

        public IANAInterfaceType IANAInterfaceType => throw new NotImplementedException();

        internal EthernetAdapter(NetworkAdapter internalAdapter)
        {
            async void Initialization()
            {
                ConnectionProfile profile = await InternalAdapter.GetConnectedProfileAsync();
                //Windows.Networking.Connectivity.NetworkInformation.GetHostNames()[0].IPInformation.NetworkAdapter
            }

            InternalAdapter = internalAdapter;
            Initialization();
            //Name = InternalAdapter.
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
