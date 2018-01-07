using EmptyBox.IO.Devices.Radio;

namespace EmptyBox.IO.Interoperability
{
    public static class Extensions
    {
        public static RadioStatus ToRadioStatus(this Android.Bluetooth.State status)
        {
            switch (status)
            {
                case Android.Bluetooth.State.On:
                    return RadioStatus.On;
                case Android.Bluetooth.State.TurningOff:
                case Android.Bluetooth.State.TurningOn:
                case Android.Bluetooth.State.Off:
                    return RadioStatus.Off;
                default:
                    return RadioStatus.Unknown;
            }
        }

        public static Android.Bluetooth.State ToState(this RadioStatus status)
        {
            switch (status)
            {
                case RadioStatus.On:
                    return Android.Bluetooth.State.On;
                case RadioStatus.Disabled:
                case RadioStatus.Off:
                default:
                    return Android.Bluetooth.State.Off;
            }
        }
    }
}