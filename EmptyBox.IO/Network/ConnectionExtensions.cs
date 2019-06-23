using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public static class ConnectionExtensions
    {
        public delegate bool ComputationalPredicate<TIn, TOut>(TIn @in, out TOut @out);

        public static async Task<byte[]> WaitAnswer(this ICommunicationElement com, Func<byte[], bool> check, TimeSpan span)
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            byte[] result = null;
            IConnection connection = com as IConnection;

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

            com.MessageReceived += Handler;
            if (connection != null)
            {
                connection.ConnectionInterrupted += Interrupted;
            }
            if (span == null)
            {
                resetEvent.Wait();
            }
            else
            {
                resetEvent.Wait(span);
            }
            com.MessageReceived -= Handler;
            if (connection != null)
            {
                connection.ConnectionInterrupted -= Interrupted;
            }
            return result;
        }

        public static async Task<(T Value, bool Success)> WaitAnswer<T>(this ICommunicationElement com, ComputationalPredicate<byte[], T> check, TimeSpan span)
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            T result = default;
            bool success = false;
            IConnection connection = com as IConnection;

            void Handler(ICommunicationElement connection, byte[] message)
            {
                if (check(message, out result))
                {
                    success = true;
                    resetEvent.Set();
                }
            }

            void Interrupted(IConnection connection)
            {
                resetEvent.Set();
            }

            com.MessageReceived += Handler;
            if (connection != null)
            {
                connection.ConnectionInterrupted += Interrupted;
            }
            resetEvent.Wait(span);
            com.MessageReceived -= Handler;
            if (connection != null)
            {
                connection.ConnectionInterrupted -= Interrupted;
            }
            return (result, success);
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

        public static async Task<(T Value, bool Success)> WaitAnswer<T, TAddress, TPort>(this IPointedSocket<TAddress, TPort> socket, Func<IAccessPoint<TAddress, TPort>, bool> checkSender, ComputationalPredicate<byte[], T> checkMessage, TimeSpan span)
            where TAddress : IAddress
            where TPort : IPort
        {
            await Task.Yield();
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            T result = default;
            bool success = false;

            void Handler(IPointedSocket<TAddress, TPort> socket, IAccessPoint<TAddress, TPort> sender, byte[] message)
            {
                if (checkSender(sender) && checkMessage(message, out result))
                {
                    success = true;
                    resetEvent.Set();
                }
            }

            socket.MessageReceived += Handler;
            resetEvent.Wait(span);
            socket.MessageReceived -= Handler;
            return (result, success);
        }
    }
}
