using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Radio
{
    public interface IRadio : IDevice
    {
        Task<RadioStatus> GetRadioStatus();
        Task<bool> SetRadioStatus(RadioStatus state);
    }
}
