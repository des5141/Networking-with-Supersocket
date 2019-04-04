using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGD;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace SuperSocketNetwork.Ncs
{
    public partial class NcsMain
    {
        NcsServer ncsServer = new NcsServer();
        static List<List<NcsUser>> user_list = new List<List<NcsUser>>();

        public NcsMain(ServerConfig config)
        {
            // Set buffer
            NcsTemplateBuffer.SetTempBuffer();

            for (int i = 0; i < Program.space_max; i++)
            {
                user_list.Add(new List<NcsUser>());
            }

            ncsServer.Setup(new RootConfig(), config);
            ncsServer.Start();

            ncsServer.NewSessionConnected += new SessionHandler<NcsUser>(NcsServer_NewUserConnected);
            ncsServer.SessionClosed += new SessionHandler<NcsUser, CloseReason>(NcsServer_UserClosed);
            ncsServer.NewRequestReceived += new RequestHandler<NcsUser, NcsRequestInfo>(NcsServer_NewRequestReceived);
        }

        void UserSpace(NcsUser user, int space)
        {
            lock (user_list)
            {
                for(int i = 0; i < Program.space_max; i++)
                {
                    user_list[i].Remove(user);
                }
                user_list[space].Add(user);
                user.space = space;
            }
        }

        void NcsServer_NewUserConnected(NcsUser user)
        {
            user.heartbeat_start();
        }

        void NcsServer_UserClosed(NcsUser user, CloseReason reason)
        {
            user.instance_die = true;
            lock (user_list)
            {
                for (int i = 0; i < Program.space_max; i++)
                {
                    user_list[i].Remove(user);
                }
            }
        }

        void NcsServer_NewRequestReceived(NcsUser user, NcsRequestInfo requestInfo)
        {
            var buffer = requestInfo.Buffer;

            uint bufferLength = buffer.extract_uint();
            ushort signal = buffer.extract_ushort();

            switch (signal)
            {
                case Program.signal_heartbeat_first:
                {
                    user.Send(NcsTemplateBuffer.HeartbeatBuffer2);
                    user.heartbeat = true;
                }
                    break;

                default:
                {
                    Console.WriteLine("unvaild : " + signal);
                }
                    break;
            }
        }

    }
}
