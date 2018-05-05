using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Network.Bluetooth;
using Windows.Devices.Enumeration;
using EmptyBox.IO.Network;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDevice : IBluetoothDevice
    {
        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public string Name { get; private set; }
        public MACAddress Address { get; private set; }
        public DevicePairStatus PairStatus => DevicePairStatus.Paired;
        public ConnectionStatus ConnectionStatus => ConnectionStatus.Unknow;
        public BluetoothClass DeviceClass => throw new NotImplementedException();
        #endregion

        #region Constructors
        internal BluetoothDevice(MACAddress address, string name)
        {
            Address = address;
            Name = name;
        }
        #endregion

        #region Destructors
        ~BluetoothDevice()
        {
            Close(false);
        }
        #endregion

        #region Private functions
        private void Close(bool unexcepted)
        {

        }
        #endregion

        #region Public function
        public void Dispose()
        {
            Close(false);
        }

        public async Task<RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            try
            {
                DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(Constants.AQS_CLASS_GUID + Constants.AQS_BLUETOOTH_GUID);
                List<BluetoothAccessPoint> result = new List<BluetoothAccessPoint>();
                foreach (DeviceInformation device in devices)
                {
                    try
                    {
                        RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(device.Id);
                        if (rds != null)
                        {
                            result.Add(new BluetoothAccessPoint(this, rds.ServiceId.ToBluetoothPort()));
                        }
                    }
                    catch
                    {

                    }
                }
                return new RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>(result, AccessStatus.Success, null);
            }
            catch (Exception ex)
            {
                return new RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>(null, AccessStatus.UnknownError, ex);
            }
        }
        #endregion
    }
}
