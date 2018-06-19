using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi
{
    public struct WiFiAdapterChannelChangedEventArgs
    {
        public WiFiChannels OldChannel { get; private set; }
        public WiFiChannels NewChannel { get; private set; }

        public WiFiAdapterChannelChangedEventArgs(WiFiChannels old, WiFiChannels @new)
        {
            OldChannel = old;
            NewChannel = @new;
        }
    }

    public delegate void WiFiAdapterChannelChanged(IWiFiAdapter adapter, WiFiAdapterChannelChangedEventArgs args);
}
