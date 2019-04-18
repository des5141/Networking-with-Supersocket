using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGD;
using Ncs.Routing;
using Ncs.Server;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace Ncs.ConsoleTest
{
    class Program
    {
        public static Action NewSessionConnected = () => { };
        private static NcsMain<NewUser> server;
        static void Main(string[] args)
        {
            NcsTemplateBuffer.SetTempBuffer();
            server = new NcsMain<NewUser>(new ServerConfig()
            {
                Port = 65535,
                Ip = "Any",
                MaxConnectionNumber = 5000,
                Mode = SocketMode.Tcp,
                Name = "NcsMain",
            });

            while (Console.ReadLine() != "q")
            {
                
            }
        }
    }

    public class NewUser : AppSession<NewUser, NcsRequestInfo>
    {
        public bool heartbeat = false;
    }

    public class Test : NcsModule<NewUser>
    {
        public Test()
        {
            packet[(ushort)1] = (user, info) =>
            {
                user.heartbeat = true;
                user.Send(NcsTemplateBuffer.HeartbeatBuffer2);
            };

            NewSessionConnected = user =>
            {
                Console.WriteLine("NewSessionConnected");
                new Task(async () =>
                {
                    int heartbeat_count = 0;
                    while (true)
                    {
                        if (heartbeat_count >= 10)
                        {
                            user.heartbeat = false;
                        }
                        else
                        {
                            heartbeat_count++;
                        }
                        user.Send(NcsTemplateBuffer.HeartbeatBuffer1);
                        await Task.Delay(1000);
                        if ((user.heartbeat == false) && (heartbeat_count >= 10))
                        {
                            user.Close();
                        }
                        else
                        {
                            continue;
                        }
                        if (heartbeat_count >= 10)
                        {
                            heartbeat_count = 0;
                        }
                    }
                  
                }).Start();
            };
        }
    }
}
