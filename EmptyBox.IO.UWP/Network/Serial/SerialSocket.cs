using EmptyBox.IO.Devices.Serial;
using EmptyBox.IO.Network.Help;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;

namespace EmptyBox.IO.Network.Serial
{
    public sealed class SerialSocket : ASocket<SerialPort>, ISerialSocket
    {
        ISerialPort ISerialSocket.SocketProvider => SocketProvider;

        public uint BaudRate { get; set; }
        public bool BreakSignalState { get; set; }
        public bool CarrierDetectState => throw new NotImplementedException();
        public bool ClearToSendState => throw new NotImplementedException();
        public ushort DataBits => throw new NotImplementedException();
        public bool DataSetReadyState => throw new NotImplementedException();
        public SerialHandshake Handshake { get; set; }
        public bool IsDataTerminalReadyEnabled { get; set; }
        public bool IsRequestToSendEnabled { get; set; }
        public SerialParity Parity { get; set; }
        public TimeSpan ReadTimeout { get; set; }
        public SerialStopBitCount StopBits { get; set; }
        public TimeSpan WriteTimeout { get; set; }
        public SerialDevice SerialDevice { get; private set; }

        internal SerialSocket(SerialDevice device)
        {
            SerialDevice = device;
        }

        public override async Task<VoidResult<SocketOperationStatus>> Close()
        {
            await Task.Yield();
            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Unknown, null);
        }

        public override async Task<VoidResult<SocketOperationStatus>> Open()
        {
            await Task.Yield();
            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Unknown, null);
        }

        public override async Task<VoidResult<SocketOperationStatus>> Send(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
