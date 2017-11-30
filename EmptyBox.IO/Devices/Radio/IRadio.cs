using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Radio
{
    public interface IRadio : IDevice
    {
        RadioStatus RadioStatus { get; }
        Task<AccessStatus> SetRadioStatus(RadioStatus state);
    }
}
