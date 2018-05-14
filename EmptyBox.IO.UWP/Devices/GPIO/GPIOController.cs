using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.ScriptRuntime;
using Windows.Devices.Gpio;

namespace EmptyBox.IO.Devices.GPIO
{
    public sealed class GPIOController : IGPIOController
    {
        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public GpioController InternalDevice { get; private set; }
        public ConnectionStatus ConnectionStatus => ConnectionStatus.Connected;
        public string Name => throw new NotImplementedException();
        #endregion

        #region Constructors
        public GPIOController(GpioController controller)
        {
            InternalDevice = controller;
        }
        #endregion

        #region Private functions
        private void Close(bool unexcepted)
        {

        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            Close(false);
        }

        public async Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number)
        {
            await Task.Yield();
            try
            {
                bool success = InternalDevice.TryOpenPin((int)number, GpioSharingMode.SharedReadOnly, out GpioPin pin, out GpioOpenStatus status);
                if (success)
                {
                    return new RefResult<IGPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.PinOpened, null);
                }
                else
                {
                    return new RefResult<IGPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.UnknownError, null);
                }
            }
            catch (Exception ex)
            {
                return new RefResult<IGPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.UnknownError, ex);
            }
        }

        public async Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number, GPIOPinSharingMode shareMode)
        {
            await Task.Yield();
            try
            {
                bool success = InternalDevice.TryOpenPin((int)number, GpioSharingMode.SharedReadOnly, out GpioPin pin, out GpioOpenStatus status);
                if (success)
                {
                    return new RefResult<IGPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.PinOpened, null);
                }
                else
                {
                    return new RefResult<IGPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.UnknownError, null);
                }
            }
            catch (Exception ex)
            {
                return new RefResult<IGPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.UnknownError, ex);
            }
        }
        #endregion
    }
}
