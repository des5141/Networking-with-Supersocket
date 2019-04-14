using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocketNetwork;

namespace Source
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
                Name = "NcsMain"
            };
            NcsMain ncsServer = new NcsMain(mConfig, ServerNewSessionConnected.func, ServerSessionClosed.func, ServerNewRequestReceived.func);
            KeyWaiting.func();
        }
    }
}
