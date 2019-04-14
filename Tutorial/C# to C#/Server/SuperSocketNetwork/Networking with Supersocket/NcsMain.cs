using System.Collections.Generic;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace SuperSocketNetwork.Ncs
{
    public class NcsMain
    {
        NcsServer ncsServer = new NcsServer();
        public static List<NcsUser> user_list = new List<NcsUser>();

        public NcsMain(ServerConfig config)
        {
            ncsServer.Setup(new RootConfig(), config);
            NcsTemplateBuffer.SetTempBuffer();
            ncsServer.NewSessionConnected += new SessionHandler<NcsUser>(ServerNewSessionConnected.func);
            ncsServer.SessionClosed += new SessionHandler<NcsUser, CloseReason>(ServerSessionClosed.func);
            ncsServer.NewRequestReceived += new RequestHandler<NcsUser, NcsRequestInfo>(ServerNewRequestReceived.func);
            ncsServer.Start();
        }
    }
}
