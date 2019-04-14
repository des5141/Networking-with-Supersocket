using SuperSocket.SocketBase;

namespace SuperSocketNetwork.Ncs
{
    partial class NcsUser : AppSession<NcsUser, NcsRequestInfo>
    {
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
