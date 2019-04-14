namespace Networking_with_Supersocket
{
    class ServerNewSessionConnected
    {
        public static void func(NcsUser user)
        {
            NcsMain.user_list.Add(user);
            user.heartbeat_start();
        }
    }
}
