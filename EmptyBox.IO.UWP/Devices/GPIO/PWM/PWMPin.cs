using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Pwm;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public sealed class PWMPin : IPWMPin
    {
        //IDevice IDevice.Parent => Parent;

        IPWMController IPWMPin.Parent => Parent;

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public double DutyCycle
        {
            get => InternalDevice.GetActiveDutyCyclePercentage();
            set => InternalDevice.SetActiveDutyCyclePercentage(value);
        }
        public uint PinNumber { get; }
        public bool InvertedPolarity
        {
            get => InternalDevice.Polarity == PwmPulsePolarity.ActiveLow;
            set => InternalDevice.Polarity = value ? PwmPulsePolarity.ActiveLow : PwmPulsePolarity.ActiveHigh;
        }
        public PWMController Parent { get; }
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public PwmPin InternalDevice { get; }

        internal PWMPin(PWMController controller, PwmPin @internal, uint pinNumber)
        {
            Parent = controller;
            InternalDevice = @internal;
            PinNumber = pinNumber;
            Parent.ConnectionStatusChanged += Parent_ConnectionStatusChanged;
        }

        private void Parent_ConnectionStatusChanged(IDevice device, ConnectionStatus status)
        {
            ConnectionStatusChanged?.Invoke(this, status);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            InternalDevice.Start();
        }

        public void Stop()
        {
            InternalDevice.Stop();
        }
    }
}
