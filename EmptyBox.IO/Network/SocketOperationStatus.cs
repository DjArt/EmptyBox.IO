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
