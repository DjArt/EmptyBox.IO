using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.HID
{
    public interface IKeyboard : IHID
    {
        event KeyboardInputEventHandler KeyEvent;

        uint? KeysCount { get; }
    }
}
