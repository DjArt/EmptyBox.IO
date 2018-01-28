using EmptyBox.IO.Network;
using EmptyBox.IO.Network.IP;
using EmptyBox.IO.Network.Ethernet;

namespace EmptyBox.IO.Devices.Ethernet
{
    public interface IEthernetAdapter : IRemovableDevice
    {
        MACAddress HardwareAddress { get; }
        ConnectionStatus NetworkStatus { get; }
        ulong MaxInboundSpeed { get; }
        ulong MaxOutboundSpeed { get; }
        ITCPConnectionProvider TCPProvider { get; }
        IUDPSocketProvider UDPProvider { get; }
        IEthernetSocketProvider EthernetProvider { get; }
    }
}
