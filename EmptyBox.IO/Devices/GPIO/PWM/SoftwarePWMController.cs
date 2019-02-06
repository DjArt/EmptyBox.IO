using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public sealed class SoftwarePWMController : IPWMController
    {
        private List<SoftwarePWMPin> _Pins;
        private Task _Loop;
        private CancellationTokenSource _TokenSource;
        private double _Freqency;

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public double MaxFrequency => 1000;
        public double MinFrequency => 40;
        public double Frequency
        {
            get => _Freqency;
            set
            {
                if (value >= MinFrequency && value <= MaxFrequency)
                {
                    _Freqency = value;
                    _Pins.ForEach(x => x.DutyCycle = x.DutyCycle);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public uint PinCount => GPIOController.PinCount;
        public ConnectionStatus ConnectionStatus => GPIOController.ConnectionStatus;
        public string Name => "Software PWM";
        public IGPIOController GPIOController { get; }

        public SoftwarePWMController(IGPIOController controller)
        {
            _Pins = new List<SoftwarePWMPin>();
            GPIOController = controller;
            Frequency = MinFrequency;
            GPIOController.ConnectionStatusChanged += GPIOController_ConnectionStatusChanged;
        }

        private void GPIOController_ConnectionStatusChanged(IDevice device, ConnectionStatus status)
        {
            ConnectionStatusChanged?.Invoke(this, status);
        }

        public void Dispose()
        {
            _Pins.ForEach(x => x.Dispose());
        }

        public async Task<RefResult<IPWMPin, GPIOPinOpenStatus>> OpenPin(uint number)
        {
            var pin = await GPIOController.OpenPin(number, GPIOPinSharingMode.Exclusive);
            if (pin.Status == GPIOPinOpenStatus.PinOpened)
            {
                SoftwarePWMPin _pin = new SoftwarePWMPin(this, pin.Result);
                _Pins.Add(_pin);
                _pin.PinStatusChanged += _pin_PinStatusChanged;
                _pin.PinClosed += _pin_PinClosed;
                return new RefResult<IPWMPin, GPIOPinOpenStatus>(_pin, GPIOPinOpenStatus.PinOpened, null);
            }
            else
            {
                return new RefResult<IPWMPin, GPIOPinOpenStatus>(null, pin.Status, pin.Exception);
            }
        }

        private void _pin_PinClosed(SoftwarePWMPin obj)
        {
            _Pins.Remove(obj);
        }

        private void _pin_PinStatusChanged()
        {
            if (_Pins.Any(x => x.IsActive))
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void Start()
        {
            if (_TokenSource == null)
            {
                _TokenSource = new CancellationTokenSource();
                _Loop = Loop(_TokenSource.Token);
            }
        }

        private void Stop()
        {
            if (_TokenSource != null)
            {
                _TokenSource.Cancel();
                _TokenSource = null;
            }
        }

        private async Task Loop(CancellationToken token)
        {
            await Task.Yield();
            Stopwatch a = new Stopwatch();
            double wait = Stopwatch.Frequency / Math.Pow(Frequency, 2);
            a.Start();
            while (!token.IsCancellationRequested)
            {
                long curr = a.ElapsedTicks;
                while (curr < wait)
                {
                    curr = a.ElapsedTicks;
                }
                List<SoftwarePWMPin> pins = _Pins.FindAll(x => x.IsActive);
                foreach (SoftwarePWMPin pin in pins)
                {
                    if (pin.RequiredFrequency >= pin.CurrentFrequency)
                    {
                        pin.Pin.SetValue(pin.InvertedPolarity ? GPIOPinValue.Low : GPIOPinValue.High);
                        pin.ActiveCounter++;
                    }
                    else
                    {
                        pin.Pin.SetValue(pin.InvertedPolarity ? GPIOPinValue.High : GPIOPinValue.Low);
                        pin.InactiveCounter++;
                    }
                }
                a.Restart();
            }
        }
    }
}
