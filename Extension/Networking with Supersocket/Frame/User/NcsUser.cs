using System;
using CGD;
using SuperSocket.SocketBase;

namespace Networking_with_Supersocket
{
    public partial class NcsUser : AppSession<NcsUser, NcsRequestInfo>
    {
        public bool die = false;
        protected override void HandleException(Exception e)
        {
            Console.WriteLine("Application error: {0}", e.Message);
        }
        public void Send(buffer buffer)
        {
            this.Send(buffer.buf, 0, buffer.len);
        }
    }
}