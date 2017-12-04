using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network
{
    public enum SocketOperationStatus
    {
        Unknown,
        UnknownError,
        Success,
        ConnectionIsAlreadyClosed,
        ConnectionIsAlreadyOpen,
        ConnectionIsClosed,
        ListenerIsAlreadyStarted,
        ListenerIsAlreadyClosed,
    }
}
