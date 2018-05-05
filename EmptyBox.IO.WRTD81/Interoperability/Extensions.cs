using System;
using System.Threading.Tasks;
using EmptyBox.IO.Devices;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Network.Bluetooth;
using Windows.Networking;
using EmptyBox.IO.Network;
using EmptyBox.IO.Access;

namespace EmptyBox.IO.Interoperability
{
    public static class Extensions
    {
        public static AccessStatus ToAccessStatus(this DeviceAccessStatus status)
        {
            switch (status)
            {
                case DeviceAccessStatus.Allowed:
                    return AccessStatus.Success;
                case DeviceAccessStatus.DeniedBySystem:
                    return AccessStatus.DeniedBySystem;
                case DeviceAccessStatus.DeniedByUser:
                    return AccessStatus.DeniedByUser;
                default:
                case DeviceAccessStatus.Unspecified:
                    return AccessStatus.UnknownError;
            }
        }
        
        public static DeviceAccessStatus ToDeviceAccessStatus(this AccessStatus status)
        {
            switch (status)
            {
                case AccessStatus.Success:
                    return DeviceAccessStatus.Allowed;
                case AccessStatus.DeniedBySystem:
                    return DeviceAccessStatus.DeniedBySystem;
                case AccessStatus.DeniedByUser:
                    return DeviceAccessStatus.DeniedByUser;
                default:
                case AccessStatus.UnknownError:
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
                try
                {
                    RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(device.Id);
                    if (rds != null && rds.ConnectionHostName.ToMACAddress().Equals(accesspoint.Address))
                    {
                        return rds.ConnectionServiceName;
                    }
                }
                catch
                {

                }
            }
            return string.Empty;
        }
    }
}
