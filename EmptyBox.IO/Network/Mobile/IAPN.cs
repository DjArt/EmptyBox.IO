using EmptyBox.IO.Devices;
using EmptyBox.IO.Devices.Mobile;
using EmptyBox.IO.Network.IP;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Mobile
{
    public interface IAPN : ITCPConnectionProvider, IUDPSocketProvider
    {
        string Name { get; }
        string AccessPoint { get; }
        string Username { get; }
        string Password { get; }
        bool IsConnected { get; }
        bool IsLTEAccessPoint { get; }
        APNType Type { get; }
        APNAuthenficationType AuthenficationType { get; }
    }
}