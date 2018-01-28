using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Ethernet
{
    public interface IEthernetSocketProvider : IPLSocket
    {
        new MACAddress Address { get; }
        /// Отправляет сообщение по указанному адресу.
        /// </summary>
        /// <param name="host">Адрес доставки.</param>
        /// <param name="data">Доставляемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<SocketOperationStatus> Send(MACAddress host, byte[] data);
    }
}
