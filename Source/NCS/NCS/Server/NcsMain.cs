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
    public class NcsMain<T> where T : AppSession<T, NcsRequestInfo>, new()
    {
        NcsServer<T> ncsServer = new NcsServer<T>();


        public NcsMain(ServerConfig config)
        {
            NcsScan.StartScan();
            NcsDefine.Option = new NcsOption();
            ncsServer.Setup(new RootConfig(), config);
            ncsServer.Start();

            ncsServer.NewSessionConnected += new SessionHandler<T>(NcsServer_NewUserConnected);
            ncsServer.SessionClosed += new SessionHandler<T, CloseReason>(NcsServer_UserClosed);
            ncsServer.NewRequestReceived += new RequestHandler<T, NcsRequestInfo>(NcsServer_NewRequestReceived);
        }

        public NcsMain(ServerConfig config, NcsOption option)
        {
            NcsScan.StartScan();
            NcsDefine.Option = option;
            ncsServer.Setup(new RootConfig(), config);
            ncsServer.Start();

            ncsServer.NewSessionConnected += new SessionHandler<T>(NcsServer_NewUserConnected);
            ncsServer.SessionClosed += new SessionHandler<T, CloseReason>(NcsServer_UserClosed);
            ncsServer.NewRequestReceived += new RequestHandler<T, NcsRequestInfo>(NcsServer_NewRequestReceived);
        }

        void NcsServer_NewUserConnected(T user)
        {
            NcsModule<T>.NewSessionConnected.Invoke(user);
        }

        void NcsServer_UserClosed(T user, CloseReason reason)
        {
            NcsModule<T>.SessionClosed.Invoke(user, reason);
        }

        void NcsServer_NewRequestReceived(T user, NcsRequestInfo requestInfo)
        {
            NcsModule<T>.NewRequestReceived.Invoke(user, requestInfo);
            if (Packet<T>.BufferDictionary.ContainsKey(requestInfo.Key))
            {
                var action = Packet<T>.BufferDictionary[requestInfo.Key] as Action<T, NcsRequestInfo>;
                action(user, requestInfo);
            }
        }

    }
}
