using EmptyBox.IO.Devices.Serial;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Help;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace EmptyBox.IO.Network.Serial
{
    public sealed class SerialSocket : ASocket<SerialPort>, ISerialSocket
    {
        private DataWriter Output;
        private DataReader Input;
        private Task ReceiveLoopTask;

        ISerialPort ISerialSocket.SocketProvider => SocketProvider;

        public uint BaudRate { get => SerialDevice.BaudRate; set => SerialDevice.BaudRate = value; }
        public bool BreakSignalState { get => SerialDevice.BreakSignalState; set => SerialDevice.BreakSignalState = value; }
        public bool CarrierDetectState => SerialDevice.CarrierDetectState;
        public bool ClearToSendState => SerialDevice.ClearToSendState;
        public ushort DataBits { get => SerialDevice.DataBits; set => SerialDevice.DataBits = value; }
        public bool DataSetReadyState => SerialDevice.DataSetReadyState;
        public SerialHandshake Handshake { get => SerialDevice.Handshake.ToSerialHandshake(); set => SerialDevice.Handshake = value.ToSerialHandshake(); }
        public bool IsDataTerminalReadyEnabled { get => SerialDevice.IsDataTerminalReadyEnabled; set => SerialDevice.IsDataTerminalReadyEnabled = value; }
        public bool IsRequestToSendEnabled { get => SerialDevice.IsRequestToSendEnabled; set => SerialDevice.IsRequestToSendEnabled = value; }
        public SerialParity Parity { get => SerialDevice.Parity.ToSerialParity(); set => SerialDevice.Parity = value.ToSerialParity(); }
        public TimeSpan ReadTimeout { get => SerialDevice.ReadTimeout; set => SerialDevice.ReadTimeout = value; }
        public SerialStopBitCount StopBits { get => SerialDevice.StopBits.ToSerialStopBitCount(); set => SerialDevice.StopBits = value.ToSerialStopBitCount(); }
        public TimeSpan WriteTimeout { get => SerialDevice.WriteTimeout; set => SerialDevice.WriteTimeout = value; }
        public SerialDevice SerialDevice { get; }

        internal SerialSocket(SerialPort provider, SerialDevice device)
        {
            SocketProvider = provider;
            SerialDevice = device;
        }

        private async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    int count = Input.ReadInt32();
                    if (count > 0)
                    {
                        byte[] buffer = new byte[count];
                        Input.ReadBytes(buffer);
                        OnMessageReceive(buffer);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2147467259)
                    {
                        await Close();
                    }
                    //FOR DEBUG
                    else
                    {
                        throw ex;
                    }
                }
            }
        }

        public override async Task<VoidResult<SocketOperationStatus>> Close()
        {
            await Task.Yield();
            IsActive = false;
            ReceiveLoopTask.Wait(100);
            Input.Dispose();
            Output.Dispose();
            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Unknown, null);
        }

        public override async Task<VoidResult<SocketOperationStatus>> Open()
        {
            await Task.Yield();
            IsActive = true;
            Input = new DataReader(SerialDevice.InputStream);
            Output = new DataWriter(SerialDevice.OutputStream);
            ReceiveLoopTask = Task.Run(ReceiveLoop);
            return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Unknown, null);
        }

        public override async Task<VoidResult<SocketOperationStatus>> Send(byte[] data)
        {
            if (IsActive)
            {
                try
                {
                    Output.WriteBytes(data);
                    await SerialDevice.OutputStream.FlushAsync();
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.Success, null);
                }
                catch (Exception ex)
                {
                    return new VoidResult<SocketOperationStatus>(SocketOperationStatus.UnknownError, ex);
                }
            }
            else
            {
                return new VoidResult<SocketOperationStatus>(SocketOperationStatus.ConnectionIsClosed, null);
            }
        }
    }
}
