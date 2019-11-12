using System.Threading.Tasks;

namespace EmptyBox.IO.Network.IP
{
    public interface IUDPSocket : IPointedSocket<IPAddress, IPPort>
    {
        /// <summary>
        /// Интерфейс для приёма и отправки сообщений.
        /// </summary>
        new IUDPSocketProvider SocketProvider { get; }
    }
}
