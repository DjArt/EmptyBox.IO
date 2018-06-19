namespace EmptyBox.IO.Devices.WiFi.Encryption
{
    public struct WPAEncryption : IWiFiEncryption
    {
        public WPAEncryptionMode Mode { get; private set; }
        public WPAEncryptionVersion Version { get; private set; }
        public string Key { get; private set; }

        public WPAEncryption(WPAEncryptionMode mode, WPAEncryptionVersion version, string key)
        {
            Mode = mode;
            Version = version;
            Key = key;
        }
    }
}
