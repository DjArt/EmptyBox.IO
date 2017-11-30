using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices;
using Windows.Devices.Bluetooth;
using EmptyBox.IO.Devices.Radio;
using Windows.Devices.Radios;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Interoperability
{
    public static class Extensions
    {
        public static ConnectionStatus ToConnectionStatus(this BluetoothConnectionStatus status)
        {
            switch (status)
            {
                case BluetoothConnectionStatus.Connected:
                    return ConnectionStatus.Connected;
                case BluetoothConnectionStatus.Disconnected:
                    return ConnectionStatus.Disconnected;
                default:
                    return ConnectionStatus.Unknow;
            }
        }

        public static BluetoothConnectionStatus ToBluetoothConnectionStatus(this ConnectionStatus status)
        {
            switch (status)
            {
                case ConnectionStatus.Connected:
                    return BluetoothConnectionStatus.Connected;
                default:
                case ConnectionStatus.Disconnected:
                    return BluetoothConnectionStatus.Disconnected;
            }
        }

        public static RadioStatus ToRadioStatus(this RadioState status)
        {
            switch (status)
            {
                case RadioState.On:
                    return RadioStatus.On;
                case RadioState.Off:
                    return RadioStatus.Off;
                case RadioState.Disabled:
                    return RadioStatus.Disabled;
                default:
                case RadioState.Unknown:
                    return RadioStatus.Unknown;
            }
        }

        public static RadioState ToRadioState(this RadioStatus status)
        {
            switch (status)
            {
                case RadioStatus.On:
                    return RadioState.On;
                case RadioStatus.Off:
                    return RadioState.Off;
                case RadioStatus.Disabled:
                    return RadioState.Disabled;
                default:
                case RadioStatus.Unknown:
                    return RadioState.Unknown;
            }
        }

        public static AccessStatus ToAccessStatus(this RadioAccessStatus status)
        {
            switch (status)
            {
                case RadioAccessStatus.Allowed:
                    return AccessStatus.Allowed;
                case RadioAccessStatus.DeniedBySystem:
                    return AccessStatus.DeniedBySystem;
                case RadioAccessStatus.DeniedByUser:
                    return AccessStatus.DeniedByUser;
                default:
                case RadioAccessStatus.Unspecified:
                    return AccessStatus.Unknown;
            }
        }

        public static RadioAccessStatus ToRadioAccessStatus(this AccessStatus status)
        {
            switch (status)
            {
                case AccessStatus.Allowed:
                    return RadioAccessStatus.Allowed;
                case AccessStatus.DeniedBySystem:
                    return RadioAccessStatus.DeniedBySystem;
                case AccessStatus.DeniedByUser:
                    return RadioAccessStatus.DeniedByUser;
                default:
                case AccessStatus.Unknown:
                    return RadioAccessStatus.Unspecified;
            }
        }

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
