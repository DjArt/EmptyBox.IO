using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network
{
    public interface IConnectionProvider
    {
        IAddress Address { get; }
        IConnection CreateConnection(IAccessPoint accessPoint);
        IConnectionListener CreateConnectionListener(IPort port);
    }

    public interface IConnectionProvider<TAddress, TPort, TAccessPoint> : IConnectionProvider where TAddress : IAddress where TPort : IPort where TAccessPoint : IAccessPoint<TAddress, TPort>
    {
        new TAddress Address { get; }
        new IConnection<TAddress, TPort, TAccessPoint, IConnectionProvider<TAddress, TPort, TAccessPoint>> CreateConnection(TAccessPoint accessPoint);
        new IConnectionListener<TAddress, TPort, TAccessPoint, IConnectionProvider<TAddress, TPort, TAccessPoint>> CreateConnectionListener(TPort port);
    }
}
