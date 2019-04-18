using System;
using System.Linq;
using System.Text;
using Ncs.Server;
using SuperSocket.SocketBase;

namespace Ncs
{
    public class NcsModule<T> : IHideObjectMembers where T: AppSession<T, NcsRequestInfo>, new()
    {
        public Packet<T> packet = new Packet<T>();

        public static Action<T> NewSessionConnected = _ => { };
        public static Action<T, CloseReason> SessionClosed =(_,__)=> { };
        public static Action<T, NcsRequestInfo> NewRequestReceived = (_, __) => { };
    }
}
