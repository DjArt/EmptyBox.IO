using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public sealed class SoftwarePWMPin : IPWMPin
    {
        private double _DutyCycle;

        internal event Action PinStatusChanged;
        internal event Action<SoftwarePWMPin> PinClosed;
        
        internal ulong ActiveCounter { get; set; }
        internal ulong InactiveCounter { get; set; }
        internal double RequiredFrequency { get; private set; }
        internal double CurrentFrequency => (double)ActiveCounter / InactiveCounter;

        IPWMController IPWMPin.Controller => Controller;

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public double DutyCycle
        {
            get => _DutyCycle;
            set
            {
                _DutyCycle = value;
                _UpdateRequiredFrequency();
            }
        }
        public uint PinNumber => Pin.PinNumber;
        public bool InvertedPolarity { get; set; }
        public bool IsActive { get; private set; }
        public ConnectionStatus ConnectionStatus => Pin.ConnectionStatus;
        public string Name => Pin.Name;
        public IGPIOPin Pin { get; }
        public SoftwarePWMController Controller { get; }

        public SoftwarePWMPin(SoftwarePWMController controller, IGPIOPin pin)
        {
            InvertedPolarity = false;
            IsActive = false;
            Controller = controller;
            Pin = pin;
            Pin.ConnectionStatusChanged += Pin_ConnectionStatusChanged;
        }

        private void _UpdateRequiredFrequency()
        {
            double activeApproximation = Controller.Frequency * _DutyCycle;
            double inactiveApproximation = Controller.Frequency * (1 - _DutyCycle);
            RequiredFrequency = inactiveApproximation == 0 ? 1 : activeApproximation / inactiveApproximation;
            ActiveCounter = (ulong)(10 * RequiredFrequency);
            InactiveCounter = 1;
        }

        private void Pin_ConnectionStatusChanged(IDevice device, ConnectionStatus status)
        {
            ConnectionStatusChanged?.Invoke(this, status);
        }

        public void Dispose()
        {
            PinClosed?.Invoke(this);
        }

        public void Start()
        {
            Pin.SetMode(GPIOPinMode.Output);
            Pin.SetValue(GPIOPinValue.Low);
            IsActive = true;
            PinStatusChanged?.Invoke();
        }

        public void Stop()
        {
            Pin.SetMode(GPIOPinMode.Output);
            Pin.SetValue(GPIOPinValue.Low);
            IsActive = false;
            PinStatusChanged?.Invoke();
        }
    }
}
