using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Network.Help
{
    public interface IStreamBasedConnection
    {
        Stream? Input { get; }
        Stream? Output { get; }
    }
}
