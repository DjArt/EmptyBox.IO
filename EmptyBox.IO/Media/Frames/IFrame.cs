using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Frames
{
    public interface IFrame : IDisposable
    {
        unsafe byte* GetBuffer();
    }
}
