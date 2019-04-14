using SuperSocket.SocketBase;

namespace SuperSocketNetwork
{
    class ServerSessionClosed
    {
        public static void func(NcsUser user, CloseReason reason)
        {
            user.die = true;
            NcsMain.user_list.Remove(user);
        }
    }
}
