using System;
using System.Threading.Tasks;
using CGD;
using SuperSocket.SocketBase;

namespace SuperSocketNetwork.Ncs
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