using EmptyBox.IO.Network;
using EmptyBox.IO.Network.IP;
using EmptyBox.IO.Network.Ethernet;
using System.Collections.Generic;

namespace EmptyBox.IO.Devices.Ethernet
{
    public interface IEthernetAdapter : INetworkAdapter
    {
        MACAddress HardwareAddress { get; }
        ConnectionStatus NetworkStatus { get; }
        ulong MaxInboundSpeed { get; }
        ulong MaxOutboundSpeed { get; }
        IEnumerable<ITCPConnectionProvider> TCPProviders { get; }
        IEnumerable<IUDPSocketProvider> UDPProviders { get; }
        IEthernetSocketProvider EthernetProvider { get; }
    }
}
