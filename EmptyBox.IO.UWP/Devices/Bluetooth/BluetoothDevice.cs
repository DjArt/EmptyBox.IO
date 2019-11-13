using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using EmptyBox.IO.Access;
using System.Text;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using EmptyBox.IO.Network.Bluetooth.Classic;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDevice : IBluetoothDevice
    {
        //IDevice IDevice.Parent => Parent;

        IBluetoothAdapter IBluetoothDevice.Parent => Parent;

        private Windows.Devices.Bluetooth.BluetoothDevice _Device;
        private Windows.Devices.Bluetooth.BluetoothLEDevice _LEDevice;

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public string Name { get; private set; }
        public Windows.Devices.Bluetooth.BluetoothDevice Device
        {
            get => _Device;
            internal set
            {
                _Device = value;
                Mode |= _Device != null ? BluetoothMode.Classic : BluetoothMode.Unknown;
            }
        }
        public Windows.Devices.Bluetooth.BluetoothLEDevice LEDevice
        {
            get => _LEDevice;
            internal set
            {
                _LEDevice = value;
                Mode |= _Device != null ? BluetoothMode.LowEnergy : BluetoothMode.Unknown;
            }
        }
        public ConnectionStatus ConnectionStatus => Device.ConnectionStatus.ToConnectionStatus();
        public MACAddress HardwareAddress { get; }
        public DevicePairStatus PairStatus => Device.DeviceInformation.Pairing.IsPaired ? DevicePairStatus.Paired : DevicePairStatus.Unpaired;
        public BluetoothClass DeviceClass => Device == null ? BluetoothClass.Unknown : (BluetoothClass)Device.ClassOfDevice.RawValue;
        public BluetoothMode Mode { get; private set; }
        public BluetoothAdapter Parent { get; }
        #endregion

        #region Constructors
        internal BluetoothDevice(BluetoothAdapter parent, Windows.Devices.Bluetooth.BluetoothDevice device)
        {
            Parent = parent;
            Device = device;
            Name = Device.Name;
            Device.ConnectionStatusChanged += Device_ConnectionStatusChanged;
            Device.NameChanged += Device_NameChanged;
            HardwareAddress = new MACAddress(Device.BluetoothAddress);
        }

        internal BluetoothDevice(BluetoothAdapter parent, Windows.Devices.Bluetooth.BluetoothLEDevice device)
        {
            Parent = parent;
            LEDevice = device;
            Name = LEDevice.Name;
            LEDevice.NameChanged += LEDevice_NameChanged;
            LEDevice.ConnectionStatusChanged += LEDevice_ConnectionStatusChanged; ;
            HardwareAddress = new MACAddress(LEDevice.BluetoothAddress);
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

        private void Device_NameChanged(Windows.Devices.Bluetooth.BluetoothDevice sender, object args)
        {
            Name = sender.Name;
        }

        private void LEDevice_NameChanged(Windows.Devices.Bluetooth.BluetoothLEDevice sender, object args)
        {
            Name = sender.Name;
        }

        private void Device_ConnectionStatusChanged(Windows.Devices.Bluetooth.BluetoothDevice sender, object args)
        {
            ConnectionStatusChanged?.Invoke(this, sender.ConnectionStatus.ToConnectionStatus());
        }

        private void LEDevice_ConnectionStatusChanged(Windows.Devices.Bluetooth.BluetoothLEDevice sender, object args)
        {
            ConnectionStatusChanged?.Invoke(this, sender.ConnectionStatus.ToConnectionStatus());
        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            Close(false);
        }

        public async Task<IEnumerable<BluetoothClassicAccessPoint>> GetClassicServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            RfcommDeviceServicesResult services = await Device.GetRfcommServicesAsync(cacheMode.ToBluetoothCacheMode());
            IEnumerable<BluetoothClassicAccessPoint> result = services.Services.Select(x => new BluetoothClassicAccessPoint(this, x.ServiceId.ToBluetoothPort())).ToList();
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(Name);
            result.Append('(');
            result.Append(HardwareAddress);
            result.Append(')');
            return result.ToString();
        }

        public bool Equals(IAddress other)
        {
            return this == other;
        }

        //public Task<IEnumerable<BluetoothClassicAccessPoint>> GetGATTServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
