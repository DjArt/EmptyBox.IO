﻿using EmptyBox.IO.Network;
using EmptyBox.IO.Network.IP;
using EmptyBox.IO.Network.Ethernet;

namespace EmptyBox.IO.Devices.Ethernet
{
    public interface IEthernetAdapter : IDevice
    {
        MACAddress HardwareAddress { get; }
        IANAInterfaceType IANAInterfaceType { get; }
        ConnectionStatus NetworkStatus { get; }
        ulong MaxInboundSpeed { get; }
        ulong MaxOutboundSpeed { get; }
        ITCPConnectionProvider TCPProvider { get; }
        IUDPSocketProvider UDPProvider { get; }
        IEthernetSocketProvider EthernetProvider { get; }
    }
}
