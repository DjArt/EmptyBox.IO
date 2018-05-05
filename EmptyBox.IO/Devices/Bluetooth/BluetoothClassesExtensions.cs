namespace EmptyBox.IO.Devices.Bluetooth
{
    public static class BluetoothClassesExtensions
    {
        public static BluetoothClass GetMajorServiceClass(this BluetoothClass @class)
        {
            return @class & BluetoothClass.MajorServiceClassesMask;
        }

        public static BluetoothClass GetMajorDeviceClass(this BluetoothClass @class)
        {
            return @class & BluetoothClass.MajorDeviceClassesMask;
        }

        public static BluetoothClass GetMinorDeviceClass(this BluetoothClass @class)
        {
            switch (@class.GetMajorDeviceClass())
            {
                case BluetoothClass.Computer:
                    return @class & BluetoothClass.Computer_FieldMask;
                case BluetoothClass.Phone:
                    return @class & BluetoothClass.Phone_FieldMask;
                case BluetoothClass.LAN:
                    return @class & BluetoothClass.AudioVideo;
                case BluetoothClass.Peripheral:
                    return @class & BluetoothClass.Peripheral_FieldMask;
                case BluetoothClass.Imaging:
                    return @class & BluetoothClass.Imaging_FieldMask;
                case BluetoothClass.Wearable:
                    return @class & BluetoothClass.Wearable_FieldMask;
                case BluetoothClass.Toy:
                    return @class & BluetoothClass.Toy_FieldMask;
                case BluetoothClass.Health:
                    return @class & BluetoothClass.Health_FieldMask;
                default:
                    return BluetoothClass.Unknown;
            }
        }

        public static BluetoothClass GetSubMinorDeviceClass(this BluetoothClass @class)
        {
            switch (@class.GetMajorDeviceClass())
            {
                case BluetoothClass.LAN:
                    return @class & BluetoothClass.LAN_SubFieldMask;
                case BluetoothClass.Peripheral:
                    return @class & BluetoothClass.Peripheral_SubFieldMask;
                case BluetoothClass.Imaging:
                    return @class & BluetoothClass.Imaging_SubFieldMask;
                default:
                    return BluetoothClass.Unknown;
            }
        }
    }
}
