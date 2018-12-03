using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public static class ConnectionExtensions
    {
        public static async Task<byte[]> WaitAnswer(this IConnection con, Func<byte[], bool> check, TimeSpan? span = null)
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            byte[] result = null;

            void Handler(ICommunicationElement connection, byte[] message)
            {
                if (check(message))
                {
                    result = message;
                    resetEvent.Set();
                }
            }

            void Interrupted(IConnection connection)
            {
                resetEvent.Set();
            }

            con.MessageReceived += Handler;
            con.ConnectionInterrupted += Interrupted;
            if (span == null)
            {
                resetEvent.Wait();
            }
            else
            {
                resetEvent.Wait(span.Value);
            }
            con.MessageReceived -= Handler;
            con.ConnectionInterrupted -= Interrupted;
            return result;
        }

        public static async Task<byte[]> WaitAnswer(this ISocket socket, Func<byte[], bool> check, TimeSpan span)
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            byte[] result = null;

            void Handler(ICommunicationElement connection, byte[] message)
            {
                if (check(message))
                {
                    result = message;
                    resetEvent.Set();
                }
            }

            socket.MessageReceived += Handler;
            resetEvent.Wait(span);
            socket.MessageReceived -= Handler;
            return result;
        }

        public static async Task<byte[]> WaitAnswer<TPort>(this ISocket<TPort> socket, Func<TPort, bool> checkSender, Func<byte[], bool> checkMessage, TimeSpan span)
            where TPort : IPort
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            byte[] result = null;

            void Handler(ISocket<TPort> connection, TPort sender, byte[] message)
            {
                if (checkSender(sender) && checkMessage(message))
                {
                    result = message;
                    resetEvent.Set();
                }
            }

            socket.MessageReceived += Handler;
            resetEvent.Wait(span);
            socket.MessageReceived -= Handler;
            return result;
        }

        public static async Task<byte[]> WaitAnswer<TAddress>(this IPointedSocket<TAddress> socket, Func<TAddress, bool> checkSender, Func<byte[], bool> checkMessage, TimeSpan span)
            where TAddress : IAddress
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            byte[] result = null;

            void Handler(IPointedSocket<TAddress> connection, TAddress sender, byte[] message)
            {
                if (checkSender(sender) && checkMessage(message))
                {
                    result = message;
                    resetEvent.Set();
                }
            }

            socket.MessageReceived += Handler;
            resetEvent.Wait(span);
            socket.MessageReceived -= Handler;
            return result;
        }

        public static async Task<byte[]> WaitAnswer<TAddress, TPort>(this IPointedSocket<TAddress, TPort> socket, Func<IAccessPoint<TAddress, TPort>, bool> checkSender, Func<byte[], bool> checkMessage, TimeSpan span)
            where TAddress : IAddress
            where TPort : IPort
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            byte[] result = null;

            void Handler(IPointedSocket<TAddress, TPort> connection, IAccessPoint<TAddress, TPort> sender, byte[] message)
            {
                if (checkSender(sender) && checkMessage(message))
                {
                    result = message;
                    resetEvent.Set();
                }
            }

            socket.MessageReceived += Handler;
            resetEvent.Wait(span);
            socket.MessageReceived -= Handler;
            return result;
        }
    }
}
