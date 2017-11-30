using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices;
using Windows.Devices.Bluetooth;
using EmptyBox.IO.Devices.Radio;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Interoperability
{
    public static class Extensions
    {
        public static AccessStatus ToAccessStatus(this DeviceAccessStatus status)
        {
            switch (status)
            {
                case DeviceAccessStatus.Allowed:
                    return AccessStatus.Allowed;
                case DeviceAccessStatus.DeniedBySystem:
                    return AccessStatus.DeniedBySystem;
                case DeviceAccessStatus.DeniedByUser:
                    return AccessStatus.DeniedByUser;
                default:
                case DeviceAccessStatus.Unspecified:
                    return AccessStatus.Unknown;
            }
        }
        
        public static DeviceAccessStatus ToDeviceAccessStatus(this AccessStatus status)
        {
            switch (status)
            {
                case AccessStatus.Allowed:
                    return DeviceAccessStatus.Allowed;
                case AccessStatus.DeniedBySystem:
                    return DeviceAccessStatus.DeniedBySystem;
                case AccessStatus.DeniedByUser:
                    return DeviceAccessStatus.DeniedByUser;
                default:
                case AccessStatus.Unknown:
                    return DeviceAccessStatus.Unspecified;
            }
        }

        public static RfcommServiceId ToRfcommServiceID(this BluetoothServiceID id)
        {
            return RfcommServiceId.FromShortId(id.ShortID);
        }

        public static BluetoothServiceID ToBluetoothServiceID(this RfcommServiceId id)
        {
            return new BluetoothServiceID() { ShortID = id.AsShortId() };
        }
    }
}
