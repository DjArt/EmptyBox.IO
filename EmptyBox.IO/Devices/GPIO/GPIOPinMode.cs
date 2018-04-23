using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.GPIO
{
    /// <summary>
    /// Описывает, настроен ли контакт ввода-вывода общего назначения (GPIO) на ввод или на вывод и как значения передаются в контакт.
    /// </summary>
    public enum GPIOPinMode : byte
    {
        /// <summary>
        /// Настраивает контакт GPIO в плавающем режиме с высоким импедансом.
        /// </summary>
        Input,
        /// <summary>
        /// Настраивает контакт GPIO как контакт с высоким импедансом и заземленным понижающим резистором.
        /// </summary>
        InputPullDown,
        /// <summary>
        /// Настраивает контакт GPIO как контакт с высоким импедансом и повышающим резистором, подключенным к выходу VCC.
        /// </summary>
        InputPullUp,
        /// <summary>
        /// Настраивает контакт GPIO в усиленном режиме передачи с низким импедансом.
        /// </summary>
        Output,
        /// <summary>
        /// Настраивает GPIO в режиме открытого стока.
        /// </summary>
        OutputOpenDrain,
        /// <summary>
        /// Настраивает контакт GPIO в режиме открытого стока с повышающим резистором.
        /// </summary>
        OutputOpenDrainPullUp,
        /// <summary>
        /// Настраивает контакт GPIO в режиме открытого коллектора.
        /// </summary>
        OutputOpenSource,
        /// <summary>
        /// Настраивает контакт GPIO в режиме открытого коллектора с понижающим резистором.
        /// </summary>
        OutputOpenSourcePullDown
    }
}
