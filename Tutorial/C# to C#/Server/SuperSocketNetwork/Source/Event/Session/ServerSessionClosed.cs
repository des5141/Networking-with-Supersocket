using SuperSocket.SocketBase;

namespace Networking_with_Supersocket
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
