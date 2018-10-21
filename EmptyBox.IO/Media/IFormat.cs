using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media
{
    public interface IFormat
    {
        bool CheckHeader(byte[] data);
    }
}
