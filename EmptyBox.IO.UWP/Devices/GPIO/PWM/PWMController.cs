using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Pwm;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public sealed class PWMController : IPWMController
    {

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public double MaxFrequency => InternalDevice.MaxFrequency;
        public double MinFrequency => InternalDevice.MinFrequency;
        public double Frequency
        {
            get => InternalDevice.ActualFrequency;
            set => InternalDevice.SetDesiredFrequency(value);
        }
        public uint PinCount => (uint)InternalDevice.PinCount;
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public IDevice Parent => throw new NotImplementedException();
        public PwmController InternalDevice { get; }

        internal PWMController(PwmController @internal)
        {
            InternalDevice = @internal;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IPWMPin> OpenPin(uint number)
        {
            await Task.Yield();
            PwmPin pin = InternalDevice.OpenPin((int)number);
            PWMPin _pin = new PWMPin(this, pin, number);
            return _pin;
        }
    }
}
