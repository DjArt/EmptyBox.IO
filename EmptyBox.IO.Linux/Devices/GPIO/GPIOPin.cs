using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EmptyBox.IO.Access;
using System.IO;
using EmptyBox.ScriptRuntime;

namespace EmptyBox.IO.Devices.GPIO
{
    public sealed class GPIOPin : IGPIOPin
    {
        #region Static internal objects
        internal const string GPIO_PIN_ACTIVE_LOW = "/active_low";
        internal const string GPIO_PIN_VALUE = "/value";
        internal const string GPIO_PIN_DIRECTION = "/direction";
        internal const string GPIO_PIN_DIRECTION_IN = "in";
        internal const string GPIO_PIN_DIRECTION_OUT = "out";
        internal const string GPIO_PIN_EDGE = "/edge";
        internal const string GPIO_PIN_EDGE_NONE = "none";
        internal const string GPIO_PIN_EDGE_BOTH = "both";
        internal const string GPIO_PIN_EDGE_FALLING = "falling";
        internal const string GPIO_PIN_EDGE_RISING = "rising";
        #endregion

        #region Private events
        private event GPIOPinEvent _ValueChanged;
        private event DeviceConnectionStatusHandler _ConnectionStatusChanged;
        #endregion

        #region Private objects
        private bool Disposed;
        private GPIOPinSharingMode _SharingMode;
        private GPIOPinSharingMode _OpenMode;
        private GPIOPinMode Mode;
        private GPIOPinValue Value;
        private Task EventGenerator;
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
        public string Name => "GPIO" + PinNumber;
        public uint PinNumber { get; private set; }
        public ConnectionStatus ConnectionStatus { get; private set; }
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
            PinNumber = number;
            _SharingMode = sharingMode;
            _OpenMode = openMode;
            Mode = GPIOPinMode.Input;
            PinPath = GPIO.GPIO_PATH + "/gpio" + PinNumber;
            ConnectionStatus = ConnectionStatus.Connected;
            switch (_OpenMode)
            {
                case GPIOPinSharingMode.Exclusive:
                    try
                    {
                        File.WriteAllText(PinPath + GPIO_PIN_DIRECTION, GPIO_PIN_DIRECTION_IN);
                        File.WriteAllText(PinPath + GPIO_PIN_VALUE, "0");
                        File.WriteAllText(PinPath + GPIO_PIN_ACTIVE_LOW, "0");
                        File.WriteAllText(PinPath + GPIO_PIN_EDGE, "both");
                    }
                    catch
                    {
                        Close(false);
                    }
                    break;
                default:
                case GPIOPinSharingMode.ReadOnlySharedAccess:

                    break;
            }
        }
        #endregion

        #region Destructor
        ~GPIOPin()
        {
            Close(false);
        }
        #endregion

        #region Private functions
        private void ReaderLoop()
        {
            while(!Disposed)
            {
                try
                {
                    string value = File.ReadAllText(PinPath + GPIO_PIN_VALUE);
                    string invert = File.ReadAllText(PinPath + GPIO_PIN_ACTIVE_LOW);
                    GPIOPinValue _value;
                    switch (value)
                    {
                        case "0":
                            _value = invert == "0" ? GPIOPinValue.Low : GPIOPinValue.High;
                            if (Value != _value)
                            {
                                Value = _value;
                                _ValueChanged?.Invoke(this, GPIOPinEdge.FallingEdge);
                            }
                            break;
                        case "1":
                            _value = invert == "0" ? GPIOPinValue.High : GPIOPinValue.Low;
                            if (Value != _value)
                            {
                                Value = _value;
                                _ValueChanged?.Invoke(this, GPIOPinEdge.RisingEdge);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException)
                    {
                        Close(true);
                    }
                }
            }
        }

        private void Close(bool unexcepted)
        {
            if (!Disposed)
            {
                Disposed = true;
                _ConnectionStatusChanged?.Invoke(this, ConnectionStatus.Disconnected);
                GC.SuppressFinalize(this);
            }
        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            Close(false);
            EventGenerator.Dispose();
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
                                    if (ex is FileNotFoundException)
                                    {
                                        Close(true);
                                        return new ValResult<bool, AccessStatus>(false, AccessStatus.DeniedBySystem, ex);
                                    }
                                    else
                                    {
                                        return new ValResult<bool, AccessStatus>(false, AccessStatus.UnknownError, ex);
                                    }
                                }
                            default:
                                return new ValResult<bool, AccessStatus>(false, AccessStatus.NotSupported, new NotImplementedException("Указанный режим работы контакта GPIO не поддерживается."));
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
                ValResult<GPIOPinEdge, AccessStatus> eventMode = await SupportedEventModes();
                switch (eventMode.Status)
                {
                    case AccessStatus.Success:
                        switch (eventMode.Result)
                        {
                            case GPIOPinEdge.None:
                                try
                                {
                                    string value = File.ReadAllText(PinPath + GPIO_PIN_VALUE);
                                    string invert = File.ReadAllText(PinPath + GPIO_PIN_ACTIVE_LOW);
                                    if (invert != "0" && invert != "1")
                                    {
                                        return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.NotSupported, new FileLoadException("Было считано неизвестное значение настройки вывода контакта GPIO.", PinPath + GPIO_PIN_ACTIVE_LOW));
                                    }
                                    switch (value)
                                    {
                                        case "0":
                                            return new ValResult<GPIOPinValue, AccessStatus>(invert == "0" ? GPIOPinValue.Low : GPIOPinValue.High, AccessStatus.Success, null);
                                        case "1":
                                            return new ValResult<GPIOPinValue, AccessStatus>(invert == "0" ? GPIOPinValue.High : GPIOPinValue.Low, AccessStatus.Success, null);
                                        default:
                                            return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.NotSupported, new FileLoadException("Было считано неизвестное значение контакта GPIO.", PinPath + GPIO_PIN_VALUE));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (ex is FileNotFoundException)
                                    {
                                        Close(true);
                                        return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.DeniedBySystem, ex);
                                    }
                                    else
                                    {
                                        return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.UnknownError, ex);
                                    }
                                }
                            default:
                                return new ValResult<GPIOPinValue, AccessStatus>(Value, AccessStatus.Success, null);
                        }
                    case AccessStatus.NotSupported:
                        try
                        {
                            string value = File.ReadAllText(PinPath + GPIO_PIN_VALUE);
                            string invert = File.ReadAllText(PinPath + GPIO_PIN_ACTIVE_LOW);
                            if (invert != "0" && invert != "1")
                            {
                                return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.NotSupported, new FileLoadException("Было считано неизвестное значение настройки вывода контакта GPIO.", PinPath + GPIO_PIN_ACTIVE_LOW));
                            }
                            switch (value)
                            {
                                case "0":
                                    return new ValResult<GPIOPinValue, AccessStatus>(invert == "0" ? GPIOPinValue.Low : GPIOPinValue.High, AccessStatus.Success, null);
                                case "1":
                                    return new ValResult<GPIOPinValue, AccessStatus>(invert == "0" ? GPIOPinValue.High : GPIOPinValue.Low, AccessStatus.Success, null);
                                default:
                                    return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.NotSupported, new FileLoadException("Было считано неизвестное значение контакта GPIO.", PinPath + GPIO_PIN_VALUE));
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex is FileNotFoundException)
                            {
                                Close(true);
                                return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.DeniedBySystem, ex);
                            }
                            else
                            {
                                return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.UnknownError, ex);
                            }
                        }
                    default:
                        return new ValResult<GPIOPinValue, AccessStatus>(null, AccessStatus.UnknownError, eventMode.Exception);
                }
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
                                    File.WriteAllText(PinPath + GPIO_PIN_DIRECTION, GPIO_PIN_DIRECTION_IN);
                                    File.WriteAllText(PinPath + GPIO_PIN_ACTIVE_LOW, "0");
                                    File.WriteAllText(PinPath + GPIO_PIN_EDGE, "both");
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
                                    File.WriteAllText(PinPath + GPIO_PIN_DIRECTION, GPIO_PIN_DIRECTION_OUT);
                                    File.WriteAllText(PinPath + GPIO_PIN_VALUE, "0");
                                    File.WriteAllText(PinPath + GPIO_PIN_ACTIVE_LOW, "0");
                                    File.WriteAllText(PinPath + GPIO_PIN_EDGE, "none");
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
                                case GPIO_PIN_DIRECTION_IN:
                                    return new ValResult<GPIOPinMode, AccessStatus>(GPIOPinMode.Input, AccessStatus.Success, null);
                                case GPIO_PIN_DIRECTION_OUT:
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

        public async Task<ValResult<GPIOPinEdge, AccessStatus>> SupportedEventModes()
        {
            if (!Disposed)
            {
                await Task.Yield();
                try
                {
                    string eventMode = File.ReadAllText(PinPath + GPIO_PIN_EDGE);
                    switch (eventMode)
                    {
                        case "none":
                            return new ValResult<GPIOPinEdge, AccessStatus>(GPIOPinEdge.None, AccessStatus.Success, null);
                        case "rising":
                            return new ValResult<GPIOPinEdge, AccessStatus>(GPIOPinEdge.RisingEdge, AccessStatus.Success, null);
                        case "falling":
                            return new ValResult<GPIOPinEdge, AccessStatus>(GPIOPinEdge.FallingEdge, AccessStatus.Success, null);
                        case "both":
                            return new ValResult<GPIOPinEdge, AccessStatus>(GPIOPinEdge.FallingEdge | GPIOPinEdge.RisingEdge, AccessStatus.Success, null);
                        default:
                            return new ValResult<GPIOPinEdge, AccessStatus>(null, AccessStatus.NotSupported, new NotSupportedException("Текущий режим контакта GPIO не поддерживается."));
                    }
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException)
                    {
                        Close(true);
                        return new ValResult<GPIOPinEdge, AccessStatus>(null, AccessStatus.DeniedBySystem, ex);
                    }
                    else
                    {
                        return new ValResult<GPIOPinEdge, AccessStatus>(null, AccessStatus.UnknownError, ex);
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