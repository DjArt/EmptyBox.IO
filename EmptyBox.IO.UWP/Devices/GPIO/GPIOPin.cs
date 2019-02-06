using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Interoperability;
using EmptyBox.ScriptRuntime;
using EmptyBox.ScriptRuntime.Results;
using Windows.Devices.Gpio;

namespace EmptyBox.IO.Devices.GPIO
{
    public sealed class GPIOPin : IGPIOPin
    {
        IGPIOController IGPIOPin.Controller => Controller;

        #region Public events
        public event GPIOPinValueChanged ValueChanged;
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public TimeSpan DebounceTime { get => InternalDevice.DebounceTimeout; set => InternalDevice.DebounceTimeout = value; }
        public uint PinNumber => (uint)InternalDevice.PinNumber;
        public IEnumerable<GPIOPinMode> SupportedModes => throw new NotImplementedException();
        public GPIOPinSharingMode SharingMode => throw new NotImplementedException();
        public GPIOPinSharingMode OpenMode => throw new NotImplementedException();
        public ConnectionStatus ConnectionStatus => ConnectionStatus.Connected;
        public string Name => PinNumber.ToString();
        public GpioPin InternalDevice { get; private set; }
        public GPIOController Controller { get; private set; }
        public bool EventChecking { get; set; }
        #endregion

        #region Constructors
        internal GPIOPin(GpioPin internalDevice, GPIOController controller)
        {
            Controller = controller;
            Controller.ConnectionStatusChanged += Controller_ConnectionStatusChanged;
            InternalDevice = internalDevice;
            EventChecking = false;
            InternalDevice.ValueChanged += InternalDevice_ValueChanged;
        }
        #endregion

        #region Private functions
        private void Controller_ConnectionStatusChanged(IDevice device, ConnectionStatus status)
        {
            ConnectionStatusChanged?.Invoke(this, status);
        }

        private void InternalDevice_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (!EventChecking || args.Edge == GpioPinEdge.FallingEdge && sender.Read() == GpioPinValue.Low || args.Edge == GpioPinEdge.RisingEdge && sender.Read() == GpioPinValue.High)
            {
                ValueChanged?.Invoke(this, args.Edge.ToGPIOPinEdge());
            }
        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public GPIOPinMode GetMode() => InternalDevice.GetDriveMode().ToGPIOPinMode();
        public GPIOPinValue GetValue() => InternalDevice.Read().ToGPIOPinValue();
        public void SetMode(GPIOPinMode mode) => InternalDevice.SetDriveMode(mode.ToGpioPinDriveMode());
        public void SetValue(GPIOPinValue value) => InternalDevice.Write(value.ToGpioPinValue());
        #endregion
    }
}
