﻿using EmptyBox.IO.Network.WiFi;

namespace EmptyBox.IO.Devices.WiFi
{
    public delegate void WiFiAdapterNetworkConnectionChanged(IWiFiAdapter adapter, WiFiNetwork network, ConnectionStatus status);
}
