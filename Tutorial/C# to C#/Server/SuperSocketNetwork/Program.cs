using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Config;
using SuperSocketNetwork.Ncs;

namespace SuperSocketNetwork
{
    class Program
    {

        // Send Type
        public const int SendToClient = 1;
        public const int SendToServer = 2;
        public const int MySpace = -1;
        public const int AllSpace = -2;
        static void Main(string[] args)
        {
            ServerConfig mConfig = new ServerConfig()
            {
                Port = 65535,
                Ip = "Any",
                MaxConnectionNumber = 5000,
                Mode = SocketMode.Tcp,
                Name = "NcsMain",
            };
            NcsMain ncsServer = new NcsMain(mConfig);
            while (true)
            {
                Console.ReadLine();
            }
        }

    }
}
