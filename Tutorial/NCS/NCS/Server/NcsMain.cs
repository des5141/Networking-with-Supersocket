using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CGD;
using Ncs.Routing;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace Ncs.Server
{
    public class NcsMain
    {
        NcsServer ncsServer = new NcsServer();

        public static NcsOption Option;
        public NcsMain(ServerConfig config)
        {
            NcsScan.StartScan();
            Option = new NcsOption();
            ncsServer.Setup(new RootConfig(), config);
            ncsServer.Start();

            ncsServer.NewSessionConnected += new SessionHandler<NcsUser>(NcsServer_NewUserConnected);
            ncsServer.SessionClosed += new SessionHandler<NcsUser, CloseReason>(NcsServer_UserClosed);
            ncsServer.NewRequestReceived += new RequestHandler<NcsUser, NcsRequestInfo>(NcsServer_NewRequestReceived);
        }

        public NcsMain(ServerConfig config, NcsOption option)
        {
            NcsScan.StartScan();
            Option = option;
            ncsServer.Setup(new RootConfig(), config);
            ncsServer.Start();

            ncsServer.NewSessionConnected += new SessionHandler<NcsUser>(NcsServer_NewUserConnected);
            ncsServer.SessionClosed += new SessionHandler<NcsUser, CloseReason>(NcsServer_UserClosed);
            ncsServer.NewRequestReceived += new RequestHandler<NcsUser, NcsRequestInfo>(NcsServer_NewRequestReceived);
        }

        void NcsServer_NewUserConnected(NcsUser user)
        {
            NcsModule.NewSessionConnected.Invoke(user);
        }

        void NcsServer_UserClosed(NcsUser user, CloseReason reason)
        {
            NcsModule.SessionClosed.Invoke(user, reason);
        }

        void NcsServer_NewRequestReceived(NcsUser user, NcsRequestInfo requestInfo)
        {
            NcsModule.NewRequestReceived.Invoke(user, requestInfo);
            if (Packet.BufferDictionary.ContainsKey(requestInfo.Key))
            {
                var action = Packet.BufferDictionary[requestInfo.Key] as Action<NcsUser, NcsRequestInfo>;
                action(user, requestInfo);
            }
        }

    }
}
