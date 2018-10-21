using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Network.Mobile;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Mobile
{
    public interface IMobileModem : IRadio
    {
        MobileNetwork ConnectedNetwork { get; }

        VoidResult<ConnectionStatus> Connect(IAPN modem);
    }
}
