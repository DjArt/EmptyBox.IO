using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmptyBox.Collections.Generic;
using EmptyBox.Collections.ObjectModel;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public sealed class SoftwarePWMController : IPWMController
    {
        private List<SoftwarePWMPin> _Pins;
        private Task? _Loop;
        private CancellationTokenSource? _TokenSource;
        private double _Freqency;
        private double _Wait;

        //IDevice IDevice.Parent => Parent;
        
        IEnumerable<ITreeNode<IDevice>> ITreeNode<IDevice>.Items => Items;
        IEnumerable<ITreeNode<IPWMPin>> ITreeNode<IPWMPin>.Items => Items;

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        public event ObservableTreeNodeItemChangeHandler<IPWMPin> ItemAdded;
        public event ObservableTreeNodeItemChangeHandler<IPWMPin> ItemRemoved;

        public ReadOnlyCollection<SoftwarePWMPin> Items => _Pins.AsReadOnly();
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
                    _Wait = Stopwatch.Frequency / Math.Pow(Frequency, 2);
                    _Pins.ForEach(x => x.UpdateRequiredFrequency());
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public uint PinCount => Parent.PinCount;
        public ConnectionStatus ConnectionStatus => Parent.ConnectionStatus;
        public string Name => "Software PWM";
        public IGPIOController Parent { get; }

        public SoftwarePWMController(IGPIOController controller)
        {
            _Pins = new List<SoftwarePWMPin>();
            Parent = controller;
            Frequency = MinFrequency;
            Parent.ConnectionStatusChanged += GPIOController_ConnectionStatusChanged;
        }

        private void GPIOController_ConnectionStatusChanged(IDevice device, ConnectionStatus status)
        {
            ConnectionStatusChanged?.Invoke(this, status);
        }

        public void Dispose()
        {
            _Pins.ForEach(x => x.Dispose());
        }

        public async Task<IPWMPin> OpenPin(uint number)
        {
            IGPIOPin pin = await Parent.OpenPin(number, GPIOPinSharingMode.Exclusive);
            SoftwarePWMPin _pin = new SoftwarePWMPin(this, pin);
            _Pins.Add(_pin);
            _pin.PinStatusChanged += _pin_PinStatusChanged;
            _pin.PinClosed += _pin_PinClosed;
            ItemAdded?.Invoke(null, this, _pin);
            return _pin;
        }

        private void _pin_PinClosed(SoftwarePWMPin obj)
        {
            _Pins.Remove(obj);
            ItemRemoved?.Invoke(null, this, obj);
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
                _Loop = Task.Run(() => Loop(_TokenSource.Token));
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
            a.Start();
            while (!token.IsCancellationRequested)
            {
                List<SoftwarePWMPin> pins = _Pins.FindAll(x => x.IsActive);
                long curr = a.ElapsedTicks;
                long prev = curr;
                while (curr < _Wait && curr - prev < _Wait - curr)
                {
                    prev = curr;
                    curr = a.ElapsedTicks;
                }
                a.Restart();
                foreach (SoftwarePWMPin pin in pins)
                {
                    if (pin.RequiredFrequency > pin.CurrentFrequency)
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
            }
        }
    }
}
