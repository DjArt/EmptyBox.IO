using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Network.Mobile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Mobile
{
    public interface IMobileModem : IRadio
    {
        MobileNetwork ConnectedNetwork { get; }

        Task<bool> Connect(IAPN modem);
    }
}
