using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public interface IConnectionListener
    {
        IConnectionProvider ConnectionProvider { get; }
        /// <summary>
        /// Порт на локальной машине.
        /// </summary>
        IPort Port { get; }
        bool IsActive { get; }
        event ConnectionReceivedDelegate ConnectionSocketReceived;
        Task<SocketOperationStatus> Start();
        Task<SocketOperationStatus> Stop();
    }
}
