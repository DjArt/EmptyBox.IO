namespace EmptyBox.IO.Devices.WiFi
{
    public struct WiFiAdapterBandChangedEventArgs
    {
        public WiFiBands OldBand { get; private set; }
        public WiFiBands NewBand { get; private set; }

        public WiFiAdapterBandChangedEventArgs(WiFiBands old, WiFiBands @new)
        {
            OldBand = old;
            NewBand = @new;
        }
    }

    public delegate void WiFiAdapterBandChanged(IWiFiAdapter adapter, WiFiAdapterBandChangedEventArgs args);
}
