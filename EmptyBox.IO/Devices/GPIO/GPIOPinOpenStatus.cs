namespace EmptyBox.IO.Devices.GPIO
{
    public enum GPIOPinOpenStatus : byte
    {
        PinOpened = 0,
        PinUnavailable = 1,
        UnknownError = 2,
        SharingViolation = 3,
        MuxingConflict = 4
    }
}
