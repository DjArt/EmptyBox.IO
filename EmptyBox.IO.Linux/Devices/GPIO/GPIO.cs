using EmptyBox.IO.Interoperability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EmptyBox.IO.Access;

namespace EmptyBox.IO.Devices.GPIO
{
    public class GPIO : IGPIO
    {
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

        #region Public static functions
        [StandardRealization]
        public static async Task<RefResult<GPIO, AccessStatus>> GetDefault()
        {
            await Task.Yield();
            if (Directory.Exists(GPIO_PATH) && File.Exists(GPIO_EXPORT_PATH) && File.Exists(GPIO_UNEXPORT_PATH))
            {
                return new RefResult<GPIO, AccessStatus>();
            }
            else
            {
                return new RefResult<GPIO, AccessStatus>(null, AccessStatus.NotAvailable, new FileNotFoundException("Файлы драйвера GPIO не найден.", GPIO_PATH));
            }
        }
        #endregion

        internal List<GPIOPin> OpenedPins { get; private set; }

        public string Name => throw new NotImplementedException();

        #region Constructors
        internal GPIO()
        {
            OpenedPins = new List<GPIOPin>();
        }
        #endregion

        #region Interface IGPIO functions
        async Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> IGPIO.OpenPin(uint number)
        {
            var res = await OpenPin(number);
            return new RefResult<IGPIOPin, GPIOPinOpenStatus>(res.Result, res.Status, res.Exception);
        }

        async Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> IGPIO.OpenPin(uint number, GPIOPinSharingMode shareMode)
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<RefResult<GPIOPin, GPIOPinOpenStatus>> OpenPin(uint number)
        {
            await Task.Yield();
            GPIOPin pin = OpenedPins.Find(x => x.Number == number);
            if (pin != null)
            {
                return new RefResult<GPIOPin, GPIOPinOpenStatus>(pin, GPIOPinOpenStatus.PinOpened, null);
            }
            File.WriteAllText(GPIO_EXPORT_PATH, number.ToString());
            string pinPath = GPIO_PATH + "/gpio" + number;
            if (Directory.Exists(pinPath))
            {
                pin = new GPIOPin(number);
                OpenedPins.Add(pin);
                return new RefResult<GPIOPin, GPIOPinOpenStatus>(pin, GPIOPinOpenStatus.PinOpened, null);
            }
            else
            {
                return new RefResult<GPIOPin, GPIOPinOpenStatus>(null, GPIOPinOpenStatus.PinUnavailable, new FileNotFoundException("Папка заданного контакта GPIO не была задана.", pinPath));
            }
        }
    }
}
