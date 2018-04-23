using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.GPIO
{
    /// <summary>
    /// Описывает возможные типы изменений, которые могут произойти со значением контакта ввода-вывода общего назначения (GPIO) для события GpioPin.ValueChanged.
    /// </summary>
    public enum GPIOPinEdge : byte
    {
        /// <summary>
        /// Значение контакта GPIO изменилось от высокого к низкому.
        /// </summary>
        FallingEdge = 0,
        /// <summary>
        /// Значение контакта GPIO изменилось от низкого к высокому.
        /// </summary>
        RisingEdge = 1
    }
}
