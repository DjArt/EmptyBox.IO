using EmptyBox.IO.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi
{
    public sealed class WiFiNetwork
    {
        /// <summary>
        /// Name of Wi-Fi Network. Also known as SSID.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Address of network owner device.
        /// </summary>
        public MACAddress BSSID { get; private set; }
        public ConnectionStatus ConnectionStatus { get; private set; }
        public WiFiEncryptionMode EncryptionMode { get; private set; }
    }
}
