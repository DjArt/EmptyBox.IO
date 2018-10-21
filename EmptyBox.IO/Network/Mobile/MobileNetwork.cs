using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Mobile
{
    public struct MobileNetwork
    {
        public string OperatorName { get; private set; }
        public double SignalStrength { get; private set; }
    }
}
