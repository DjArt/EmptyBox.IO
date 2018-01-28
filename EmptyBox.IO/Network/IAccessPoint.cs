namespace EmptyBox.IO.Network
{
    public interface IAccessPoint
    {
        IAddress Address { get; }
        IPort Port { get; }
    }
}
