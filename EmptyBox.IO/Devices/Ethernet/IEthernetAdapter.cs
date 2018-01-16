using EmptyBox.IO.Network.MAC;
using EmptyBox.IO.Network.IP;

namespace EmptyBox.IO.Devices.Ethernet
{
    public interface IEthernetAdapter : IRemovableDevice, ITCPConnectionProvider, IUDPSocketProvider
    {
        MACAddress HardwareAddress { get; }
    }
}
