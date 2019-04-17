using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncs.Routing;
using Ncs.Server;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace Ncs.ConsoleTest
{
    class Program
    {
        public static Action NewSessionConnected = () => { };
        private static NcsMain server;
        static void Main(string[] args)
        {
            NcsTemplateBuffer.SetTempBuffer();
            server = new NcsMain(new ServerConfig()
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

    public class Test : NcsModule
    {
        bool heartbeat = false;
        public Test()
        {
            packet[(ushort)1] = (user, info) =>
            {
                heartbeat = true;
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
                            heartbeat = false;
                        }
                        else
                        {
                            heartbeat_count++;
                        }
                        user.Send(NcsTemplateBuffer.HeartbeatBuffer1);
                        await Task.Delay(1000);
                        if ((heartbeat == false) && (heartbeat_count >= 10))
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
