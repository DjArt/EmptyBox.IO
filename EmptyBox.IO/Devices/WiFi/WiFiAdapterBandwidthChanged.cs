using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi
{
    public struct WiFiAdapterBandwidthChangedEventArgs
    {
        public WiFiBandwidths OldBandwidth { get; private set; }
        public WiFiBandwidths NewBandwidth { get; private set; }

        public WiFiAdapterBandwidthChangedEventArgs(WiFiBandwidths old, WiFiBandwidths @new)
        {
            OldBandwidth = old;
            NewBandwidth = @new;
        }
    }

    public delegate void WiFiAdapterBandwidthChanged(IWiFiAdapter adapter, WiFiAdapterBandwidthChangedEventArgs args);
}
