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
        /// Signal
        public const int signal_heartbeat_first = 1;
        public const int signal_heartbeat_second = 2;
        public const int signal_login = 3;

        // Send Type
        public const int SendToClient = 1;
        public const int SendToServer = 2;
        public const int MySpace = -1;
        public const int AllSpace = -2;

        // Server Setting
        public static ServerConfig mConfig = new ServerConfig()
        {
            Port = 65535,
            Ip = "Any",
            MaxConnectionNumber = 5000,
            Mode = SocketMode.Tcp,
            Name = "NcsMain",
        };

        public const int space_max = 10;

        // Server Run
        public static NcsMain ncsServer = new NcsMain(mConfig);
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ReadLine();
            }
        }

    }
}
