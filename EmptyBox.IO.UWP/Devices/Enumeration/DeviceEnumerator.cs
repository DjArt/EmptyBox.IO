using EmptyBox.IO.Devices.Audio;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Devices.Ethernet;
using EmptyBox.IO.Devices.GPIO;
using EmptyBox.IO.Devices.GPIO.ADC;
using EmptyBox.IO.Devices.GPIO.PWM;
using EmptyBox.IO.Devices.Mobile;
using EmptyBox.IO.Devices.Power;
using EmptyBox.IO.Devices.Sensors;
using EmptyBox.IO.Devices.Serial;
using EmptyBox.IO.Devices.WiFi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;

namespace EmptyBox.IO.Devices.Enumeration
{
    public sealed class DeviceEnumerator : IDeviceEnumerator
    {
        private static readonly DeviceWatcher InternalWatcher = DeviceInformation.CreateWatcher();
        private static readonly Dictionary<string, WeakReference<IDevice>> DeviceCache = new Dictionary<string, WeakReference<IDevice>>();

        internal static event TypedEventHandler<string, ConnectionStatus> ConnectionStatusChangedByID;
        internal static event TypedEventHandler<string, DeviceInformationUpdate> DeviceInformationUpdated;

        public static DeviceEnumerator Enumerator { get; } = new DeviceEnumerator();

        static DeviceEnumerator()
        {
            InternalWatcher.Added += InternalWatcher_Added;
            InternalWatcher.EnumerationCompleted += InternalWatcher_EnumerationCompleted;
            InternalWatcher.Removed += InternalWatcher_Removed;
            InternalWatcher.Stopped += InternalWatcher_Stopped;
            InternalWatcher.Updated += InternalWatcher_Updated;
            InternalWatcher.Start();
        }

        private static void InternalWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            DeviceInformationUpdated?.Invoke(args.Id, args);
        }

        private static void InternalWatcher_Stopped(DeviceWatcher sender, object args)
        {

        }

        private static void InternalWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            ConnectionStatusChangedByID?.Invoke(args.Id, ConnectionStatus.Disconnected);
        }

        private static void InternalWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {

        }

        private static void InternalWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            ConnectionStatusChangedByID?.Invoke(args.Id, ConnectionStatus.Connected);
        }

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public async Task<IEnumerable<TDevice>> FindAll<TDevice>()
            where TDevice : class, IDevice
        {
            if (typeof(TDevice) == typeof(IBluetoothAdapter))
            {
                DeviceInformationCollection adapters = await DeviceInformation.FindAllAsync(Windows.Devices.Bluetooth.BluetoothAdapter.GetDeviceSelector());
                List<TDevice> devices = new List<TDevice>();
                foreach (DeviceInformation info in adapters)
                {
                    if (!DeviceCache.ContainsKey(info.Id) || !DeviceCache[info.Id].TryGetTarget(out IDevice device))
                    {
                        device = new BluetoothAdapter(await Windows.Devices.Bluetooth.BluetoothAdapter.FromIdAsync(info.Id));
                        DeviceCache[info.Id] = new WeakReference<IDevice>(device);
                    }
                    devices.Add((TDevice)device);
                }
                return devices;
            }
            else if (typeof(TDevice) == typeof(IWiFiAdapter))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IEthernetAdapter))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IMobileModem))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IGPIOController) || typeof(TDevice) == typeof(IPWMController) || typeof(TDevice) == typeof(IADCController))
            {
                List<TDevice> devices = new List<TDevice>();
                TDevice @default = await GetDefault<TDevice>();
                if (@default != null)
                {
                    devices.Add(@default);
                }
                return devices;
            }
            else if (typeof(TDevice) == typeof(IAccelerometer))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IBarometer))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(ICompass))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IGyrometer))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IInclinometer))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(ILightSensor))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IMagnetometer))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IOrientationSensor))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IPedometer))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IProximitySensor))
            {
                return new List<TDevice>();
            }
            else if (typeof(TDevice) == typeof(IAudioInputDevice))
            {
                DeviceInformationCollection infos = await DeviceInformation.FindAllAsync(DeviceClass.AudioCapture);
                List<TDevice> devices = new List<TDevice>();
                foreach (DeviceInformation info in infos)
                {
                    if (!DeviceCache.ContainsKey(info.Id) || !DeviceCache[info.Id].TryGetTarget(out IDevice device))
                    {
                        device = new AudioInputDevice(info);
                        DeviceCache[info.Id] = new WeakReference<IDevice>(device);
                    }
                    devices.Add((TDevice)device);
                }
                return devices;
            }
            else if (typeof(TDevice) == typeof(IAudioOutputDevice))
            {
                DeviceInformationCollection infos = await DeviceInformation.FindAllAsync(DeviceClass.AudioRender);
                List<TDevice> devices = new List<TDevice>();
                foreach (DeviceInformation info in infos)
                {
                    if (!DeviceCache.ContainsKey(info.Id) || !DeviceCache[info.Id].TryGetTarget(out IDevice device))
                    {
                        device = new AudioInputDevice(info);
                        DeviceCache[info.Id] = new WeakReference<IDevice>(device);
                    }
                    devices.Add((TDevice)device);
                }
                return devices;
            }
            else if (typeof(TDevice) == typeof(ISerialPort))
            {
                DeviceInformationCollection infos = await DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector());
                List<TDevice> devices = new List<TDevice>();
                foreach(DeviceInformation info in infos)
                {
                    if (!DeviceCache.ContainsKey(info.Id) || !DeviceCache[info.Id].TryGetTarget(out IDevice device))
                    {
                        device = new SerialPort(info);
                        DeviceCache[info.Id] = new WeakReference<IDevice>(device);
                    }
                    devices.Add((TDevice)device);
                }
                return devices;
            }
            else if (typeof(TDevice) == typeof(IBattery))
            {
                DeviceInformationCollection infos = await DeviceInformation.FindAllAsync(Windows.Devices.Power.Battery.GetDeviceSelector());
                List<TDevice> devices = new List<TDevice>();
                foreach(DeviceInformation info in infos)
                {
                    if (!DeviceCache.ContainsKey(info.Id) || !DeviceCache[info.Id].TryGetTarget(out IDevice device))
                    {
                        device = new Battery(await Windows.Devices.Power.Battery.FromIdAsync(info.Id));
                        DeviceCache[info.Id] = new WeakReference<IDevice>(device);
                    }
                    devices.Add((TDevice)device);
                }
                return devices;
            }
            else if (typeof(TDevice) == typeof(IDevice))
            {
                List<IDevice> devices = new List<IDevice>();
                devices.AddRange(await FindAll<IBluetoothAdapter>());
                devices.AddRange(await FindAll<IWiFiAdapter>());
                devices.AddRange(await FindAll<IMobileModem>());
                devices.AddRange(await FindAll<IGPIOController>());
                devices.AddRange(await FindAll<IPWMController>());
                devices.AddRange(await FindAll<IADCController>());
                devices.AddRange(await FindAll<IAccelerometer>());
                devices.AddRange(await FindAll<IBarometer>());
                devices.AddRange(await FindAll<ICompass>());
                devices.AddRange(await FindAll<IGyrometer>());
                devices.AddRange(await FindAll<IInclinometer>());
                devices.AddRange(await FindAll<ILightSensor>());
                devices.AddRange(await FindAll<IMagnetometer>());
                devices.AddRange(await FindAll<IOrientationSensor>());
                devices.AddRange(await FindAll<IPedometer>());
                devices.AddRange(await FindAll<IProximitySensor>());
                devices.AddRange(await FindAll<IAudioInputDevice>());
                devices.AddRange(await FindAll<IAudioOutputDevice>());
                devices.AddRange(await FindAll<ISerialPort>());
                devices.AddRange(await FindAll<IBattery>());
                return devices.Cast<TDevice>();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public async Task<TDevice> GetDefault<TDevice>()
            where TDevice : class, IDevice
        {
            if (typeof(TDevice) == typeof(IBluetoothAdapter))
            {
                Windows.Devices.Bluetooth.BluetoothAdapter @internal = await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync();
                if (!DeviceCache.ContainsKey(@internal.DeviceId) || !DeviceCache[@internal.DeviceId].TryGetTarget(out IDevice device))
                {
                    device = new BluetoothAdapter(@internal);
                    DeviceCache[@internal.DeviceId] = new WeakReference<IDevice>(device);
                }
                return (TDevice)device;
            }
            else if (typeof(TDevice) == typeof(IWiFiAdapter))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IEthernetAdapter))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IMobileModem))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IGPIOController))
            {
                GpioController @internal = await GpioController.GetDefaultAsync();
                return (TDevice)(IDevice)new GPIOController(@internal);
            }
            else if (typeof(TDevice) == typeof(IPWMController))
            {
                PwmController @internal = await PwmController.GetDefaultAsync();
                return (TDevice)(IDevice)new PWMController(@internal);
            }
            else if (typeof(TDevice) == typeof(IADCController))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IAccelerometer))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IBarometer))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(ICompass))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IGyrometer))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IInclinometer))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(ILightSensor))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IMagnetometer))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IOrientationSensor))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IPedometer))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IProximitySensor))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IAudioInputDevice))
            {
                DeviceInformation @internal = (await DeviceInformation.FindAllAsync(DeviceClass.AudioCapture)).FirstOrDefault(x => x.IsDefault);
                if (@internal != null)
                {
                    if (!DeviceCache.ContainsKey(@internal.Id) || !DeviceCache[@internal.Id].TryGetTarget(out IDevice device))
                    {
                        device = new AudioInputDevice(@internal);
                        DeviceCache[@internal.Id] = new WeakReference<IDevice>(device);
                    }
                    return (TDevice)device;
                }
                else
                {
                    return null;
                }
            }
            else if (typeof(TDevice) == typeof(IAudioOutputDevice))
            {
                DeviceInformation @internal = (await DeviceInformation.FindAllAsync(DeviceClass.AudioRender)).FirstOrDefault(x => x.IsDefault);
                if (@internal != null)
                {
                    if (!DeviceCache.ContainsKey(@internal.Id) || !DeviceCache[@internal.Id].TryGetTarget(out IDevice device))
                    {
                        device = new AudioOutputDevice(@internal);
                        DeviceCache[@internal.Id] = new WeakReference<IDevice>(device);
                    }
                    return (TDevice)device;
                }
                else
                {
                    return null;
                }
            }
            else if (typeof(TDevice) == typeof(ISerialPort))
            {
                return null;
            }
            else if (typeof(TDevice) == typeof(IBattery))
            {
                return (TDevice)(IDevice)new Battery(Windows.Devices.Power.Battery.AggregateBattery);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public async Task<IDevice> GetRoot()
        {
            throw new NotImplementedException();
        }
    }
}
