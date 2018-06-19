using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi
{
    public delegate void WiFiAdapterNetworkConnectionChanged(IWiFiAdapter adapter, WiFiNetwork network, ConnectionStatus status);
}
