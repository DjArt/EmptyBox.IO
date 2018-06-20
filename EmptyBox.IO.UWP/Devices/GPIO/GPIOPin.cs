using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Interoperability;
using EmptyBox.ScriptRuntime;
using Windows.Devices.Gpio;

namespace EmptyBox.IO.Devices.GPIO
{
    public sealed class GPIOPin : IGPIOPin
    {
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
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        public string Name => PinNumber.ToString();
        public GpioPin InternalDevice { get; private set; }
        #endregion

        #region Constructors
        public GPIOPin(GpioPin internalDevice)
        {
            InternalDevice = internalDevice;
            InternalDevice.ValueChanged += InternalDevice_ValueChanged;
        }
        #endregion

        #region Private functions
        private void InternalDevice_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            ValueChanged?.Invoke(this, args.Edge.ToGPIOPinEdge());
        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<ValResult<GPIOPinMode, AccessStatus>> GetMode()
        {
            await Task.Yield();
            try
            {
                return new ValResult<GPIOPinMode, AccessStatus>(InternalDevice.GetDriveMode().ToGPIOPinMode(), AccessStatus.Success, null);
            }
            catch (Exception ex)
            {
                return new ValResult<GPIOPinMode, AccessStatus>(null, AccessStatus.UnknownError, ex);
            }
        }

        public async Task<ValResult<GPIOPinValue, AccessStatus>> GetValue()
        {
            await Task.Yield();
            try
            {
                return new ValResult<GPIOPinValue, AccessStatus>(InternalDevice.Read().ToGPIOPinValue(), AccessStatus.Success, null);
            }
            catch (Exception ex)
            {
                return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.UnknownError, ex);
            }
        }

        public async Task<VoidResult<AccessStatus>> SetMode(GPIOPinMode mode)
        {
            await Task.Yield();
            try
            {
                InternalDevice.SetDriveMode(mode.ToGpioPinDriveMode());
                return new VoidResult<AccessStatus>(AccessStatus.Success, null);
            }
            catch (Exception ex)
            {
                return new VoidResult<AccessStatus>(AccessStatus.UnknownError, ex);
            }
        }

        public async Task<VoidResult<AccessStatus>> SetValue(GPIOPinValue value)
        {
            await Task.Yield();
            try
            {
                InternalDevice.Write(value.ToGpioPinValue());
                return new VoidResult<AccessStatus>(AccessStatus.Success, null);
            }
            catch (Exception ex)
            {
                return new VoidResult<AccessStatus>(AccessStatus.Success, ex);
            }
        }

        public async Task<ValResult<GPIOPinEdge, AccessStatus>> SupportedEventModes()
        {
            await Task.Yield();
            return new ValResult<GPIOPinEdge, AccessStatus>(GPIOPinEdge.FallingEdge | GPIOPinEdge.RisingEdge, AccessStatus.Success, null);
        }
        #endregion
    }
}
