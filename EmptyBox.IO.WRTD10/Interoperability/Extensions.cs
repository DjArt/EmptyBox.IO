using EmptyBox.IO.Devices;
using Windows.Devices.Bluetooth;
using EmptyBox.IO.Devices.Radio;
using Windows.Devices.Radios;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network;
using Windows.Networking;
using EmptyBox.IO.Devices.Bluetooth;
using System.Threading.Tasks;
using System;
using EmptyBox.IO.Access;

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

        public static RfcommServiceId ToRfcommServiceID(this BluetoothPort id)
        {
            return RfcommServiceId.FromUuid(id.ID);
        }

        public static BluetoothPort ToBluetoothPort(this RfcommServiceId id)
        {
            return new BluetoothPort(id.Uuid);
        }

        public static HostName ToHostName(this MACAddress address)
        {
            return new HostName(address.ToString());
        }

        public static MACAddress ToMACAddress(this HostName address)
        {
            bool done = MACAddress.TryParse(address.CanonicalName, out MACAddress result);
            if (done)
            {
                return result;
            }
            else
            {
                return new MACAddress();
            }
        }

        public static async Task<string> ToServiceIDString(this BluetoothAccessPoint accesspoint)
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(accesspoint.Port.ToRfcommServiceID()));
            foreach (DeviceInformation device in devices)
            {
                RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(device.Id);
                if (rds != null && rds.ConnectionHostName.ToMACAddress().Equals(accesspoint.Address))
                {
                    return rds.ConnectionServiceName;
                }
            }
            return string.Empty;
        }

        public static BluetoothCacheMode ToBluetoothCacheMode(this BluetoothSDPCacheMode mode)
        {
            switch (mode)
            {
                default:
                case BluetoothSDPCacheMode.Cached:
                    return BluetoothCacheMode.Cached;
                case BluetoothSDPCacheMode.Uncached:
                    return BluetoothCacheMode.Uncached;
            }
        }

        public static BluetoothSDPCacheMode ToBluetoothSDPCacheMode(this BluetoothCacheMode mode)
        {
            switch (mode)
            {
                default:
                case BluetoothCacheMode.Cached:
                    return BluetoothSDPCacheMode.Cached;
                case BluetoothCacheMode.Uncached:
                    return BluetoothSDPCacheMode.Uncached;
            }
        }
    }
}
