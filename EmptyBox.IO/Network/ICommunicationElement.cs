﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public interface ICommunicationElement
    {
        /// <summary>
        /// Событие, уведомляющее о приёме сообщения.
        /// </summary>
        event MessageReceiveHandler<ICommunicationElement> MessageReceived;

        bool IsActive { get; }

        /// <summary>
        /// Отправляет сообщение.
        /// </summary>
        /// <param name="data">Передаваемое сообщение.</param>
        /// <returns>Статус доставки сообщения, если применимо к протоколу.</returns>
        Task<bool> Send(byte[] data);
        /// <summary>
        /// Начинает обмен сообщениями.
        /// </summary>
        /// <returns>Результат запуска.</returns>
        Task<bool> Open();
        /// <summary>
        /// Завершает обмен сообщениями.
        /// </summary>
        /// <returns>Результат закрытия.</returns>
        Task<bool> Close();
    }
}
