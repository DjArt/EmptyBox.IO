using System.Numerics;

namespace EmptyBox.IO.Devices.HID
{
    public delegate void TouchPointerDetectedEventHandler(ITouch keyboard, IPointer pointer, TouchPointerAction action, Vector2 location);
}