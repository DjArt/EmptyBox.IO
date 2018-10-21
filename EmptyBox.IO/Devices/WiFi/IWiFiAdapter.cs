using EmptyBox.IO.Devices.Ethernet;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Network.WiFi;
using System.Collections.Generic;

namespace EmptyBox.IO.Devices.WiFi
{
    public interface IWiFiAdapter : IRadio, IEthernetAdapter
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
