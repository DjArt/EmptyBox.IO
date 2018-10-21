namespace EmptyBox.IO.Network.WiFi.Encryption
{
    public struct WEPEncryption : IWiFiEncryption
    {
        public WEPEncryptionMode Mode { get; private set; }
        public string Key { get; private set; }

        public WEPEncryption(WEPEncryptionMode mode, string key)
        {
            Mode = mode;
            Key = key;
        }
    }
}
