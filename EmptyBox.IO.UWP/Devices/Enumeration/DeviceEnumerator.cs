using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Devices.Ethernet;
using EmptyBox.IO.Devices.GPIO;
using EmptyBox.IO.Devices.GPIO.ADC;
using EmptyBox.IO.Devices.GPIO.PWM;
using EmptyBox.IO.Devices.Mobile;
using EmptyBox.IO.Devices.Sensors;
using EmptyBox.IO.Devices.WiFi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;

namespace EmptyBox.IO.Devices.Enumeration
{
    public sealed class DeviceEnumerator : IDeviceEnumerator
    {
        public async Task<IEnumerable<TDevice>> FindAll<TDevice>()
            where TDevice : class, IDevice
        {
            throw new NotImplementedException();
        }

        public async Task<TDevice> GetDefault<TDevice>()
            where TDevice : class, IDevice
        {
            if (typeof(TDevice) == typeof(IBluetoothAdapter))
            {
                Windows.Devices.Bluetooth.BluetoothAdapter @internal = await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync();
                return (TDevice)(IDevice)new BluetoothAdapter(@internal);
            }
            else if (typeof(TDevice) == typeof(IWiFiAdapter))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IEthernetAdapter))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IMobileModem))
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IAccelerometer))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IBarometer))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(ICompass))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IGyrometer))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IInclinometer))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(ILightSensor))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IMagnetometer))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IOrientationSensor))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IPedometer))
            {
                throw new NotImplementedException();
            }
            else if (typeof(TDevice) == typeof(IProximitySensor))
            {
                throw new NotImplementedException();
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
