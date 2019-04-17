using System;
using System.Linq;
using System.Text;
using Ncs.Server;
using SuperSocket.SocketBase;

namespace Ncs
{
    public class NcsModule : IHideObjectMembers
    {
        public Packet packet = new Packet();

        public static Action<NcsUser> NewSessionConnected = _ => { };
        public static Action<NcsUser, CloseReason> SessionClosed =(_,__)=> { };
        public static Action<NcsUser, NcsRequestInfo> NewRequestReceived = (_, __) => { };
        public static Action<Exception> HandleException = _ => { };
    }
}
