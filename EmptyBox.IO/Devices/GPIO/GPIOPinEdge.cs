using System;

namespace EmptyBox.IO.Devices.GPIO
{
    /// <summary>
    /// Описывает возможные типы изменений, которые могут произойти со значением контакта ввода-вывода общего назначения (GPIO) для события GpioPin.ValueChanged.
    /// </summary>
    [Flags]
    public enum GPIOPinEdge : byte
    {
        None = 0b00,
        /// <summary>
        /// Значение контакта GPIO изменилось от высокого к низкому.
        /// </summary>
        FallingEdge = 0b01,
        /// <summary>
        /// Значение контакта GPIO изменилось от низкого к высокому.
        /// </summary>
        RisingEdge = 0b10
    }
}
