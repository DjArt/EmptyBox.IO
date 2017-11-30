using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmptyBox.IO.Network;
using EmptyBox.ScriptRuntime.Automation;

namespace EmptyBox.IO.Automation
{
    //public class ConnectionListener : IPipelineInput<INetworkServer>, IPipelineOutput<Connection>, IPipelineInputInformer<object, bool>
    //{
    //    public event OutputHandleDelegate<Connection> OutputHandle;
    //    protected Dictionary<ulong, INetworkServer> _Listeners;

    //    public ConnectionListener()
    //    {
    //        _Listeners = new Dictionary<ulong, INetworkServer>();
    //    }

    //    public void InformInput(IPipelineInput<object> sender, ulong? taskID, object input, bool state)
    //    {

    //    }

    //    public void Input(IPipelineOutput<INetworkServer> sender, ulong taskID, INetworkServer output)
    //    {
    //        if (!_Listeners.ContainsKey(taskID))
    //        {
    //            _Listeners[taskID] = output;
    //            _Listeners[taskID].ConnectionReceived += ConnectionReceived;
    //        }
    //        else
    //        {
    //            //FOR DEBUG
    //            throw new Exception("бля, тут новый прослушиватель с тем же ID, нехорошо получилось.");
    //        }
    //    }

    //    private void ConnectionReceived(Connection connection)
    //    {
    //        int index = _Listeners.Values.ToList().FindIndex(x => x == connection);
    //        if (index > -1)
    //        {
    //            OutputHandle?.Invoke(this, _Connections.Keys.ElementAt(index), message);
    //            _OH0?.Invoke(this, _Connections.Keys.ElementAt(index), message, MessageState.Received);
    //        }
    //        else
    //        {
    //            //FOR DEBUG
    //            throw new Exception("Ну, это нужно переделать, тут сообщения обрабатываются");
    //        }
    //    }
    //}
}
