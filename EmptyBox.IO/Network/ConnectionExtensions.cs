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

            void Handler(IConnection connection, byte[] message)
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
            con.ConnectionInterrupt += Interrupted;
            if (span == null)
            {
                resetEvent.Wait();
            }
            else
            {
                resetEvent.Wait(span.Value);
            }
            con.MessageReceived -= Handler;
            con.ConnectionInterrupt -= Interrupted;
            return result;
        }
    }
}
