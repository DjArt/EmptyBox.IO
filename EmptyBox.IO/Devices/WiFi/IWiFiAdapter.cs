using EmptyBox.IO.Devices.Ethernet;
using EmptyBox.IO.Devices.Radio;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi
{
    public interface IWiFiAdapter : IRadio, IEthernetAdapter, IDeviceProvider<WiFiNetwork>
    {
        event WiFiAdapterBandChanged BandChanged;
        event WiFiAdapterBandwidthChanged BandwidthChanged;
        event WiFiAdapterChannelChanged ChannelChanged;
        event WiFiAdapterNetworkConnectionChanged NetworkConnectionChanged;

        WiFiBands SupportedBands { get; }
        IEnumerable<WiFiBandwidths> SupportedBandwidths { get; }
        IEnumerable<WiFiChannels> SupportedChannels { get; }
        WiFiEncryptionMode SupportedEncryptionModes { get; }
        WiFiBands Band { get; }
        WiFiBandwidths Bandwidth { get; }
        WiFiChannels Channel { get; }
        WiFiNetwork Network { get; }
    }
}
