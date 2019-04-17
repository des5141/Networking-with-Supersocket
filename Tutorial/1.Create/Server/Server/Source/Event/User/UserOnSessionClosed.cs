using SuperSocket.SocketBase;

namespace Server
{
    partial class NcsUser
    {
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
