//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EmptyBox.IO.Network;
//using EmptyBox.IO.Network.IP;
//using EmptyBox.ScriptRuntime.Automation;

//namespace EmptyBox.IO.Automation
//{
//    public class ConnectionsManager : IPipelineInput<Connection>, IPipelineInput<byte[]>, IPipelineOutput<byte[]>, IPipelineOutputInformer<byte[], ConnectionsManager.MessageState>, IPipelineOutputInformer<Connection, ConnectionsManager.ConnectionEvent>
//    {
//        public enum ConnectionEvent
//        {
//            Added,
//            Disconnected
//        }
//        public enum MessageState
//        {
//            Received,
//            Sended,
//            Failed
//        }
//        protected Dictionary<ulong, Connection> _Connections;
//        protected event InformerOutputHandleDelegate<byte[], MessageState> _OH0;
//        protected event InformerOutputHandleDelegate<Connection, ConnectionEvent> _OH1;
//        public event OutputHandleDelegate<byte[]> OutputHandle;
//        event InformerOutputHandleDelegate<byte[], MessageState> IPipelineOutputInformer<byte[], MessageState>.InformerOutputHandle
//        {
//            add
//            {
//                _OH0 += value;
//            }
//            remove
//            {
//                _OH0 -= value;
//            }
//        }
//        event InformerOutputHandleDelegate<Connection, ConnectionEvent> IPipelineOutputInformer<Connection, ConnectionEvent>.InformerOutputHandle
//        {
//            add
//            {
//                _OH1 += value;
//            }

//            remove
//            {
//                _OH1 -= value;
//            }
//        }

//        public ConnectionsManager()
//        {
//            _Connections = new Dictionary<ulong, Connection>();
//        }

//        public void Input(IPipelineOutput<Connection> sender, ulong taskID, Connection output)
//        {
//            if (!_Connections.ContainsKey(taskID))
//            {
//                _Connections[taskID] = output;
//                output.Received += ReceiveHandler;
//                output.Interrupted += InterruptHandler;
//                _OH1?.Invoke(this, taskID, output, ConnectionEvent.Added);
//            }
//            else
//            {
//                //FOR DEBUG
//                throw new Exception("Оппа, несовпадение по taskID");
//            }
//        }

//        public async void Input(IPipelineOutput<byte[]> sender, ulong taskID, byte[] output)
//        {
//            if (_Connections.ContainsKey(taskID))
//            {
//                bool done = await _Connections[taskID].Send(output);
//                if (done)
//                {
//                    _OH0?.Invoke(this, taskID, output, MessageState.Sended);
//                }
//                else
//                {
//                    _OH0?.Invoke(this, taskID, output, MessageState.Failed);
//                }
//            }
//            else
//            {
//                _OH0?.Invoke(this, taskID, output, MessageState.Failed);
//            }
//        }

//        protected async void ReceiveHandler(Connection connection, byte[] message)
//        {
//            int index = _Connections.Values.ToList().FindIndex(x => x == connection);
//            if (index > -1)
//            {
//                OutputHandle?.Invoke(this, _Connections.Keys.ElementAt(index), message);
//                _OH0?.Invoke(this, _Connections.Keys.ElementAt(index), message, MessageState.Received);
//            }
//            else
//            {
//                //FOR DEBUG
//                throw new Exception("Ну, это нужно переделать, тут сообщения обрабатываются");
//            }
//        }

//        protected void InterruptHandler(Connection connection)
//        {
//            int index = _Connections.Values.ToList().FindIndex(x => x == connection);
//            if (index > -1)
//            {
//                _Connections.Remove(_Connections.Keys.ElementAt(index));
//                connection.Received -= ReceiveHandler;
//                connection.Interrupted -= InterruptHandler;
//            }
//            else
//            {
//                //FOR DEBUG
//                throw new Exception("Ну, это нужно переделать, тут прерывания обрабатываются");
//            }
//        }

//        public void LinkConnectionInput(IPipelineOutput<Connection> output)
//        {
//            output.OutputHandle += Input;
//        }

//        public void LinkMessageSenderInput(IPipelineOutput<byte[]> output)
//        {
//            output.OutputHandle += Input;
//        }

//        public void LinkMessageReceiverOutput(IPipelineInput<byte[]> input)
//        {
//            OutputHandle += input.Input;
//        }

//        public void UnlinkConnectionInput(IPipelineOutput<Connection> output)
//        {
//            output.OutputHandle -= Input;
//        }

//        public void UnlinkMessageSenderInput(IPipelineOutput<byte[]> output)
//        {
//            output.OutputHandle -= Input;
//        }

//        public void UnlinkMessageReceiverOutput(IPipelineInput<byte[]> input)
//        {
//            OutputHandle -= input.Input;
//        }
//    }
//}
