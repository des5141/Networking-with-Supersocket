using System;
using System.Threading.Tasks;
using SuperSocket.SocketBase;

namespace Ncs.Server
{
    public class NcsUser<T> : AppSession<T, NcsRequestInfo> where T : AppSession<T, NcsRequestInfo>, new()
    {
        ///  Override Functions
        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }

        protected override void HandleException(Exception e)
        {
            Console.WriteLine("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }

        public void Send(CGD.buffer buffer)
        {
            this.Send(buffer.buf, 0, buffer.len);
        }
    }
}