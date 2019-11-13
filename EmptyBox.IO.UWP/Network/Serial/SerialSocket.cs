using EmptyBox.IO.Devices.Serial;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Help;
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

        public override async Task<bool> Close()
        {
            await Task.Yield();
            IsActive = false;
            ReceiveLoopTask.Wait(100);
            Input.Dispose();
            Output.Dispose();
            return true;
        }

        public override async Task<bool> Open()
        {
            await Task.Yield();
            IsActive = true;
            Input = new DataReader(SerialDevice.InputStream);
            Output = new DataWriter(SerialDevice.OutputStream);
            ReceiveLoopTask = Task.Run(ReceiveLoop);
            return true;
        }

        public override async Task<bool> Send(byte[] data)
        {
            if (IsActive)
            {
                Output.WriteBytes(data);
                await SerialDevice.OutputStream.FlushAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
