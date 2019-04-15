using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using Networking_with_Supersocket;

namespace Source
{
    class Program
    {
        public static AsyncLock TaskLock = new AsyncLock();
        public static UserData testing_instance = new UserData();

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
            NcsMain ncsServer = new NcsMain(mConfig, ServerStarted.func, ServerNewSessionConnected.func, ServerSessionClosed.func, ServerNewRequestReceived.func);
            KeyWaiting.func();
        }
    }
}
