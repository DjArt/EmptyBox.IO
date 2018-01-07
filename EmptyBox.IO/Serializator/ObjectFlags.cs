using System;

namespace EmptyBox.IO.Serializator
{
    [Flags]
    public enum ObjectFlags : byte
    {
        None = 0b00000000,
        IsNull = 0b00000001,
    }
}
