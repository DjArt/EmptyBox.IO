using EmptyBox.IO.Devices.Serial;

namespace EmptyBox.IO.Network.Serial
{
    public interface ISerialConnection : IConnection
    {
        new ISerialPort ConnectionProvider { get; }
    }
}
