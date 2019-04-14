using System.Collections.Generic;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace SuperSocketNetwork
{
    public class NcsMain
    {
        NcsServer ncsServer = new NcsServer();
        public static List<NcsUser> user_list = new List<NcsUser>();

        public NcsMain(ServerConfig config, SessionHandler<NcsUser> NewSessionConnected, SessionHandler<NcsUser, CloseReason> SessionClosed, RequestHandler<NcsUser, NcsRequestInfo> NewRequestReceived)
        {
            ncsServer.Setup(new RootConfig(), config);
            NcsTemplateBuffer.SetTempBuffer();
            ncsServer.NewSessionConnected += new SessionHandler<NcsUser>(NewSessionConnected);
            ncsServer.SessionClosed += new SessionHandler<NcsUser, CloseReason>(SessionClosed);
            ncsServer.NewRequestReceived += new RequestHandler<NcsUser, NcsRequestInfo>(NewRequestReceived);
            ncsServer.Start();
        }
    }
}
