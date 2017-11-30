using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmptyBox.IO.Network
{
    public interface ILinkLevelAddress
    {
        byte[] Address { get; set; }
    }
}