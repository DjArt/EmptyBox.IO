using EmptyBox.IO.Devices.Serial;
using System;

namespace EmptyBox.IO.Network.Serial
{
    public interface ISerialSocket : ISocket
    {
        new ISerialPort SocketProvider { get; }
        uint BaudRate { get; set; }
        bool BreakSignalState { get; set; }
        bool CarrierDetectState { get; }
        bool ClearToSendState { get; }
        ushort DataBits { get; set; }
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
