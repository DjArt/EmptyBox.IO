using EmptyBox.IO.Devices.Ethernet;
using EmptyBox.IO.Devices.Radio;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi
{
    public interface IWiFiAdapter : IRadio, IEthernetAdapter
    {
    }
}
