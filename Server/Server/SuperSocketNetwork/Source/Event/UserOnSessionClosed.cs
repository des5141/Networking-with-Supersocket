using SuperSocket.SocketBase;

namespace SuperSocketNetwork
{
    partial class NcsUser : AppSession<NcsUser, NcsRequestInfo>
    {
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
