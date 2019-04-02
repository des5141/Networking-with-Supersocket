using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            NcsBuffer buffer = new NcsBuffer(requestInfo.Body);
            int space_type = buffer.pop_sint16();
            int signal = buffer.pop_sint16();

            // Send To Server
            if (requestInfo.Key == Program.SendToServer)
            {
                switch (signal)
                {
                    case Program.signal_heartbeat_first:
                        {
                            NcsBuffer heartbeat_buffer = new NcsBuffer(Program.signal_heartbeat_second, Program.SendToClient, Program.MySpace);
                            heartbeat_buffer.push_size();
                            user.Send(heartbeat_buffer.write_buffer, 0, heartbeat_buffer.write_offset);
                            user.heartbeat = true;
                        }
                        break;

                    case Program.signal_login:
                        {
                            Console.WriteLine("Login : " + buffer.pop_string());
                            user.authentication = true;
                            UserSpace(user, 0);

                        }
                        break;

                    default:
                        {
                            Console.WriteLine("unvaild : " + signal);
                        }
                        break;
                }
            }

            // Send To Client
            else if (requestInfo.Key == Program.SendToClient)
            {
                if (user.authentication == true)
                {
                    switch (space_type)
                    {
                        case Program.MySpace:
                            {
                                foreach (NcsUser index in user_list[user.space])
                                {
                                    if (index != user)
                                        index.Send(requestInfo.Buffer, 0, requestInfo.Buffer.Length);
                                }
                            }
                            break;

                        case Program.AllSpace:
                            {
                                for (int i = 0; i < Program.space_max; i++)
                                {
                                    foreach (NcsUser index in user_list[i])
                                    {
                                        if (index != user)
                                            index.Send(requestInfo.Buffer, 0, requestInfo.Buffer.Length);
                                    }
                                }
                            }
                            break;

                        default:
                            {
                                foreach (NcsUser index in user_list[space_type])
                                {
                                    if (index != user)
                                        index.Send(requestInfo.Buffer, 0, requestInfo.Buffer.Length);
                                }
                            }
                            break;
                    }
                }
            }
        }

    }
}
