using EmptyBox.IO.Devices.Serial;

namespace EmptyBox.IO.Network.Serial
{
    public interface ISerialSocket : ISocket
    {
        new ISerialPort SocketProvider { get; }
    }
}
