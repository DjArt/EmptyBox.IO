using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Serial;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Serial
{
    public interface ISerialPort : IDevice, ISocketProvider
    {
        uint BaudRate { get; set; }
        bool BreakSignalState { get; set; }
        bool CarrierDetectState { get; }
        bool ClearToSendState { get; }
        ushort DataBits { get; }
        bool DataSetReadyState { get; }
        SerialHandshake Handshake { get; set; }
        bool IsDataTerminalReadyEnabled { get; set; }
        bool IsRequestToSendEnabled { get; set; }
        SerialParity Parity { get; set; }
        TimeSpan ReadTimeout { get; set; }
        SerialStopBitCount StopBits { get; set; }
        TimeSpan WriteTimeout { get; set; }
    }
}
