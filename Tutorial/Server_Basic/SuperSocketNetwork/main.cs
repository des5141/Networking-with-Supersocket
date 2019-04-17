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


/*
 * 
            Test().Wait();
            Test2().Wait();

        public static async Task Test()
        {
            using (var releaser = await TaskLock.LockAsync())
            {
                new Task(() =>
                {
                    lock (testing_instance)
                    {
                        Console.WriteLine("Task!!");
                        Task.Delay(1000).Wait();
                        Test().Wait();
                    }
                }).Start();
            }
        }

        public static async Task Test2()
        {
            using (var releaser = await TaskLock.LockAsync())
            {
                new Task(() =>
                {
                    lock (testing_instance)
                    {
                        Console.WriteLine("-- this is second");
                        Task.Delay(1000).Wait();
                        Test2().Wait();
                    }
                }).Start();
            }
        }
*/