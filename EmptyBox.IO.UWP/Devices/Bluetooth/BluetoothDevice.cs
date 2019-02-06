using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using EmptyBox.ScriptRuntime;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDevice : IBluetoothDevice
    {
        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public string Name => Device.Name;
        public Windows.Devices.Bluetooth.BluetoothDevice Device { get; private set; }
        public ConnectionStatus ConnectionStatus => Device.ConnectionStatus.ToConnectionStatus();
        public MACAddress Address { get; private set; }
        public DevicePairStatus PairStatus => Device.DeviceInformation.Pairing.IsPaired ? DevicePairStatus.Paired : DevicePairStatus.Unpaired;
        public BluetoothClass DeviceClass => throw new NotImplementedException();
        public BluetoothMode Mode => BluetoothMode.Unknown;
        #endregion
        
        #region Constructors
        public BluetoothDevice(Windows.Devices.Bluetooth.BluetoothDevice device)
        {
            Device = device;
            device.ConnectionStatusChanged += Device_ConnectionStatusChanged;
            Address = new MACAddress(Device.BluetoothAddress);
        }
        #endregion

        #region Destructor
        ~BluetoothDevice()
        {
            Close(false);
        }
        #endregion

        #region Private functions
        private void Close(bool unexcepted)
        {

        }

        private void Device_ConnectionStatusChanged(Windows.Devices.Bluetooth.BluetoothDevice sender, object args)
        {
            ConnectionStatusChanged?.Invoke(this, sender.ConnectionStatus.ToConnectionStatus());
        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            Close(false);
        }

        public async Task<RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            try
            {
                RfcommDeviceServicesResult services = await Device.GetRfcommServicesAsync(cacheMode.ToBluetoothCacheMode());
                IEnumerable<BluetoothAccessPoint> result = services.Services.Select(x => new BluetoothAccessPoint(this, x.ServiceId.ToBluetoothPort()));
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
