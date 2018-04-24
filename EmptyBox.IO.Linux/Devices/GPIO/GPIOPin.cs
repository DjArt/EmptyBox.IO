using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EmptyBox.IO.Access;
using System.IO;

namespace EmptyBox.IO.Devices.GPIO
{
    public sealed class GPIOPin : IGPIOPin
    {
        internal const string GPIO_PIN_ACTIVE_LOW = "/active_low";
        internal const string GPIO_PIN_DIRECTION = "/direction";
        internal const string GPIO_PIN_VALUE = "/value";

        #region Private events
        private event GPIOPinEvent _ValueChanged;
        private event DeviceConnectionStatusHandler _ConnectionStatusChanged;
        #endregion

        #region Private objects
        private bool Disposed;
        private uint _PinNumber;
        private GPIOPinSharingMode _SharingMode;
        private GPIOPinSharingMode _OpenMode;
        private GPIOPinMode Mode;
        private Task EventGenerator;
        private GPIOPinValue Value;
        private CancellationTokenSource GeneratorToken;
        private string PinPath;
        private TimeSpan _DebounceTime;
        #endregion

        #region Public events
        public event GPIOPinEvent ValueChanged
        {
            add
            {
                if (!Disposed)
                {
                    _ValueChanged += value;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
            remove
            {
                if (!Disposed)
                {
                    _ValueChanged -= value;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        public event DeviceConnectionStatusHandler ConnectionStatusChanged
        {
            add
            {
                if (!Disposed)
                {
                    _ConnectionStatusChanged += value;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
            remove
            {
                if (!Disposed)
                {
                    _ConnectionStatusChanged -= value;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        #endregion

        #region Public objects
        public string Name
        {
            get
            {
                if (!Disposed)
                {
                    return "GPIO" + _PinNumber;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        public uint PinNumber
        {
            get
            {
                if (!Disposed)
                {
                    return _PinNumber;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        public ConnectionStatus ConnectionStatus
        {
            get
            {
                if (!Disposed)
                {
                    return ConnectionStatus.Connected;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        public GPIOPinSharingMode SharingMode
        {
            get
            {
                if (!Disposed)
                {
                    return _SharingMode;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        public GPIOPinSharingMode OpenMode
        {
            get
            {
                if (!Disposed)
                {
                    return _OpenMode;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        public TimeSpan DebounceTime
        {
            get
            {
                if (!Disposed)
                {
                    return _DebounceTime;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
            set
            {
                if (!Disposed)
                {
                    _DebounceTime = value;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        public IEnumerable<GPIOPinMode> SupportedModes
        {
            get
            {
                if (!Disposed)
                {
                    //Нужно опрашивать устройство, но как?
                    yield return GPIOPinMode.Input;
                    yield return GPIOPinMode.Output;
                }
                else
                {
                    throw new ObjectDisposedException(ToString());
                }
            }
        }
        #endregion

        #region Constructors
        internal GPIOPin(uint number, GPIOPinSharingMode sharingMode, GPIOPinSharingMode openMode)
        {
            Disposed = false;
            _PinNumber = number;
            _SharingMode = sharingMode;
            _OpenMode = openMode;
            Mode = GPIOPinMode.Input;
            PinPath = GPIO.GPIO_PATH + "/gpio" + _PinNumber;
            //EventGenerator = new Task(ReaderLoop);
            //EventGenerator.Start();
        }
        #endregion

        #region Destructor
        ~GPIOPin()
        {
            Close();
        }
        #endregion

        #region IDisposable interface functions
        void IDisposable.Dispose()
        {
            Close();
        }
        #endregion

        #region Private functions
        private void ReaderLoop()
        {
            while(!Disposed && GeneratorToken.Token.IsCancellationRequested)
            {

            }
        }
        #endregion

        #region Public functions
        public void Close()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ValResult<bool, AccessStatus>> SetValue(GPIOPinValue value)
        {
            if (!Disposed)
            {
                await Task.Yield();
                switch (_OpenMode)
                {
                    case GPIOPinSharingMode.Exclusive:
                        switch (Mode)
                        {
                            case GPIOPinMode.Output:
                                try
                                {
                                    File.WriteAllText(PinPath + GPIO_PIN_VALUE, ((byte)value).ToString());
                                    return new ValResult<bool, AccessStatus>(true, AccessStatus.Success, null);
                                }
                                catch (Exception ex)
                                {
                                    //TODO: реализовать разбор ошибок, если возможно.
                                    return new ValResult<bool, AccessStatus>(false, AccessStatus.UnknownError, ex);
                                }
                            default:
                                return new ValResult<bool, AccessStatus>(false, AccessStatus.NotSupported, new NotImplementedException("Данный режим работы контакта GPIO не поддерживается."));
                        }
                    default:
                    case GPIOPinSharingMode.ReadOnlySharedAccess:
                        return new ValResult<bool, AccessStatus>(false, AccessStatus.DeniedBySystem, new NotSupportedException("Контакт GPIO открыт в режиме \"Только считывание\"."));

                }
            }
            else
            {
                throw new ObjectDisposedException(ToString());
            }
        }

        public async Task<ValResult<GPIOPinValue, AccessStatus>> GetValue()
        {
            if (!Disposed)
            {
                await Task.Yield();
                //switch (_OpenMode)
                //{
                //    case GPIOPinSharingMode.Exclusive:
                //        switch (Mode)
                //        {
                //            case GPIOPinMode.Input:
                //                return new ValResult<GPIOPinMode, AccessStatus>(Mode, AccessStatus.Success, null);
                //            case GPIOPinMode.Output:
                //                try
                //                {
                //                    string direction = File.ReadAllText(PinPath + GPIO_PIN_DIRECTION);
                //                    switch (direction)
                //                    {
                //                        case "in":
                //                            return new ValResult<GPIOPinMode, AccessStatus>(GPIOPinMode.Input, AccessStatus.Success, null);
                //                        case "out":
                //                            return new ValResult<GPIOPinMode, AccessStatus>(GPIOPinMode.Output, AccessStatus.Success, null);
                //                        default:
                //                            return new ValResult<GPIOPinMode, AccessStatus>(null, AccessStatus.UnknownError, new FileLoadException("В файле конфигурации контакта GPIO оказались неподдерживаемые параметры.", PinPath + GPIO_PIN_DIRECTION));
                //                    }
                //                }
                //                catch (Exception ex)
                //                {
                //                    return new ValResult<GPIOPinMode, AccessStatus>(null, AccessStatus.DeniedBySystem, ex);
                //                }
                //        }
                //    case GPIOPinSharingMode.ReadOnlySharedAccess:
                //        switch (Mode)
                //        {

                //        }
                //}
                throw new NotImplementedException();
            }
            else
            {
                throw new ObjectDisposedException(ToString());
            }
        }

        public async Task<ValResult<bool, AccessStatus>> SetMode(GPIOPinMode mode)
        {
            if (!Disposed)
            {
                await Task.Yield();
                switch (OpenMode)
                {
                    case GPIOPinSharingMode.Exclusive:
                        switch (mode)
                        {
                            case GPIOPinMode.Input:
                                try
                                {
                                    File.WriteAllText(PinPath + GPIO_PIN_DIRECTION, "in");
                                    return new ValResult<bool, AccessStatus>(true, AccessStatus.Success, null);
                                }
                                catch (Exception ex)
                                {
                                    //TODO: реализовать разбор ошибок, если возможно.
                                    return new ValResult<bool, AccessStatus>(false, AccessStatus.UnknownError, ex);
                                }
                            case GPIOPinMode.Output:
                                try
                                {
                                    File.WriteAllText(PinPath + GPIO_PIN_DIRECTION, "in");
                                    File.WriteAllText(PinPath + GPIO_PIN_VALUE, "0");
                                    return new ValResult<bool, AccessStatus>(true, AccessStatus.Success, null);
                                }
                                catch (Exception ex)
                                {
                                    //TODO: реализовать разбор ошибок, если возможно.
                                    return new ValResult<bool, AccessStatus>(false, AccessStatus.UnknownError, ex);
                                }
                            default:
                                return new ValResult<bool, AccessStatus>(false, AccessStatus.NotSupported, new NotImplementedException("Данный режим работы контакта GPIO не поддерживается."));
                        }
                    default:
                        return new ValResult<bool, AccessStatus>(false, AccessStatus.DeniedBySystem, new NotSupportedException("Данный режим работы контакта GPIO не поддерживается."));
                }
            }
            else
            {
                throw new ObjectDisposedException(ToString());
            }
        }

        public async Task<ValResult<GPIOPinMode, AccessStatus>> GetMode()
        {
            if (!Disposed)
            {
                await Task.Yield();
                switch (_OpenMode)
                {
                    case GPIOPinSharingMode.Exclusive:
                        return new ValResult<GPIOPinMode, AccessStatus>(Mode, AccessStatus.Success, null);
                    default:
                    case GPIOPinSharingMode.ReadOnlySharedAccess:
                        try
                        {
                            string direction = File.ReadAllText(PinPath + GPIO_PIN_DIRECTION);
                            switch (direction)
                            {
                                case "in":
                                    return new ValResult<GPIOPinMode, AccessStatus>(GPIOPinMode.Input, AccessStatus.Success, null);
                                case "out":
                                    return new ValResult<GPIOPinMode, AccessStatus>(GPIOPinMode.Output, AccessStatus.Success, null);
                                default:
                                    return new ValResult<GPIOPinMode, AccessStatus>(null, AccessStatus.UnknownError, new FileLoadException("В файле конфигурации контакта GPIO оказались неподдерживаемые параметры.", PinPath + GPIO_PIN_DIRECTION));
                            }
                        }
                        catch (Exception ex)
                        {
                            return new ValResult<GPIOPinMode, AccessStatus>(null, AccessStatus.DeniedBySystem, ex);
                        }
                }
            }
            else
            {
                throw new ObjectDisposedException(ToString());
            }
        }
        #endregion
    }
}
