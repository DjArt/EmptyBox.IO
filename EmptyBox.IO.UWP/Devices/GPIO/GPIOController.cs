using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Interoperability;
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
        public uint PinCount => (uint)InternalDevice.PinCount;

        public IDevice Parent => throw new NotImplementedException();
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

        public async Task<IGPIOPin> OpenPin(uint number)
        {
            await Task.Yield();
            bool success = InternalDevice.TryOpenPin((int)number, GpioSharingMode.Exclusive, out GpioPin pin, out GpioOpenStatus status);
            if (success)
            {
                return new GPIOPin(pin, this);
            }
            else
            {
                return null;
            }
        }

        public async Task<IGPIOPin> OpenPin(uint number, GPIOPinSharingMode shareMode)
        {
            await Task.Yield();
            bool success = InternalDevice.TryOpenPin((int)number, GpioSharingMode.Exclusive, out GpioPin pin, out GpioOpenStatus status);
            if (success)
            {
                return new GPIOPin(pin, this);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
