using EmptyBox.IO.Interoperability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;

namespace EmptyBox.IO.Devices.GPIO
{
    public sealed class GPIOController : IGPIOController
    {
        #region Static internal objects
        /// <summary>
        /// Путь до папки драйвера GPIO
        /// </summary>
        internal const string GPIO_PATH = "/sys/class/gpio";
        /// <summary>
        /// Путь до файла отключения контактов GPIO
        /// </summary>
        internal const string GPIO_UNEXPORT_PATH = "/sys/class/gpio/unexport";
        /// <summary>
        /// Путь до файла подключения контактов GPIO
        /// </summary>
        internal const string GPIO_EXPORT_PATH = "/sys/class/gpio/export";
        #endregion

        #region Static public functions
        [StandardRealization]
        public static async Task<RefResult<GPIOController, AccessStatus>> GetDefault()
        {
            await Task.Yield();
            if (Directory.Exists(GPIO_PATH) && File.Exists(GPIO_EXPORT_PATH) && File.Exists(GPIO_UNEXPORT_PATH))
            {
                return new RefResult<GPIOController, AccessStatus>();
            }
            else
            {
                return new RefResult<GPIOController, AccessStatus>(null, AccessStatus.NotAvailable, new FileNotFoundException("Файлы драйвера GPIO не найден.", GPIO_PATH));
            }
        }
        #endregion

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public string Name => throw new NotImplementedException();
        public ConnectionStatus ConnectionStatus => ConnectionStatus.Connected;
        #endregion

        #region Constructors
        internal GPIOController()
        {

        }
        #endregion

        #region Interface IGPIO functions
        async Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> IGPIOController.OpenPin(uint pin)
        {
            var res = await OpenPin(pin);
            return new RefResult<IGPIOPin, GPIOPinOpenStatus>(res.Result, res.Status, res.Exception);
        }

        async Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> IGPIOController.OpenPin(uint pin, GPIOPinSharingMode shareMode)
        {
            var res = await OpenPin(pin, shareMode);
            return new RefResult<IGPIOPin, GPIOPinOpenStatus>(res.Result, res.Status, res.Exception);
        }
        #endregion
        
        #region Private functions
        private void Close(bool unexcepted)
        {
            ConnectionStatusChanged?.Invoke(this, ConnectionStatus.Disconnected);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            Close(false);
        }

        /// <summary>
        /// Пытается открыть ранее не открытый контакт GPIO в режиме монопольного доступа. Если контакт был открыт ранее, пытается  открыть его в режиме общего доступа для чтения.
        /// </summary>
        /// <param name="pin">Номер контакта GPIO.</param>
        /// <returns></returns>
        public async Task<RefResult<GPIOPin, GPIOPinOpenStatus>> OpenPin(uint pin)
        {
            await Task.Yield();
            string pinPath = GPIO_PATH + "/gpio" + pin;
            if (Directory.Exists(pinPath))
            {
                bool accessible = true;
                try
                {
                    File.ReadAllText(pinPath + "/value");
                }
                catch
                {
                    accessible = false;
                }
                if (accessible)
                {
                    return new RefResult<GPIOPin, GPIOPinOpenStatus>(new GPIOPin(pin, GPIOPinSharingMode.ReadOnlySharedAccess, GPIOPinSharingMode.ReadOnlySharedAccess), GPIOPinOpenStatus.PinOpened, null);
                }
                else
                {
                    return new RefResult<GPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.SharingViolation, new AccessViolationException("Контакт GPIO заблокирован другим приложением."));
                }
            }
            else
            {
                File.WriteAllText(GPIO_EXPORT_PATH, pin.ToString());
                if (Directory.Exists(pinPath))
                {
                    return new RefResult<GPIOPin, GPIOPinOpenStatus>(new GPIOPin(pin, GPIOPinSharingMode.Exclusive, GPIOPinSharingMode.Exclusive), GPIOPinOpenStatus.PinOpened, null);
                }
                else
                {
                    return new RefResult<GPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.PinUnavailable, new FileNotFoundException("Папка заданного контакта GPIO не была найдена.", pinPath));
                }
            }
        }

        /// <summary>
        /// Пытается открыть контакт GPIO в заданном режиме.
        /// </summary>
        /// <param name="pin">Номер контакта GPIO.</param>
        /// <param name="shareMode">Режим открытия контакта GPIO.</param>
        /// <returns></returns>
        public async Task<RefResult<GPIOPin, GPIOPinOpenStatus>> OpenPin(uint pin, GPIOPinSharingMode shareMode)
        {
            await Task.Yield();
            string pinPath = GPIO_PATH + "/gpio" + pin;
            switch (shareMode)
            {
                default:
                case GPIOPinSharingMode.Exclusive:
                    if (Directory.Exists(pinPath))
                    {
                        return new RefResult<GPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.MuxingConflict, new FileLoadException("Контакт GPIO был открыт ранее", pinPath));
                    }
                    else
                    {
                        File.WriteAllText(GPIO_EXPORT_PATH, pin.ToString());
                        if (Directory.Exists(pinPath))
                        {
                            return new RefResult<GPIOPin, GPIOPinOpenStatus>(new GPIOPin(pin, GPIOPinSharingMode.Exclusive, GPIOPinSharingMode.Exclusive), GPIOPinOpenStatus.PinOpened, null);
                        }
                        else
                        {
                            return new RefResult<GPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.PinUnavailable, new FileNotFoundException("Папка заданного контакта GPIO не была найдена.", pinPath));
                        }
                    }
                case GPIOPinSharingMode.ReadOnlySharedAccess:
                    if (Directory.Exists(pinPath))
                    {
                        bool accessible = true;
                        try
                        {
                            File.ReadAllText(pinPath + "/value");
                        }
                        catch
                        {
                            accessible = false;
                        }
                        if (accessible)
                        {
                            return new RefResult<GPIOPin, GPIOPinOpenStatus>(new GPIOPin(pin, GPIOPinSharingMode.ReadOnlySharedAccess, GPIOPinSharingMode.ReadOnlySharedAccess), GPIOPinOpenStatus.PinOpened, null);
                        }
                        else
                        {
                            return new RefResult<GPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.SharingViolation, new AccessViolationException("Контакт GPIO заблокирован другим приложением."));
                        }
                    }
                    else
                    {
                        File.WriteAllText(GPIO_EXPORT_PATH, pin.ToString());
                        if (Directory.Exists(pinPath))
                        {
                            return new RefResult<GPIOPin, GPIOPinOpenStatus>(new GPIOPin(pin, GPIOPinSharingMode.ReadOnlySharedAccess, GPIOPinSharingMode.Exclusive), GPIOPinOpenStatus.PinOpened, null);
                        }
                        else
                        {
                            return new RefResult<GPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.PinUnavailable, new FileNotFoundException("Папка заданного контакта GPIO не была найдена.", pinPath));
                        }
                    }
            }
        }
        #endregion
    }
}
