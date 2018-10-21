using EmptyBox.IO.Devices.Radio;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Radio
{
    public interface IFMRadio : IRadio
    {
        double Frequency { get; set; }
        double SignalStrength { get; set; }
    }
}
