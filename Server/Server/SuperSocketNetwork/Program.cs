using System;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocketNetwork.Ncs;

namespace SuperSocketNetwork
{
    class Program
    {
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
