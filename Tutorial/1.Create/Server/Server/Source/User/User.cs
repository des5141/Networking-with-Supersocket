using System;
using CGD;
using SuperSocket.SocketBase;
using Networking_with_Supersocket;

namespace Server
{
    public partial class NcsUser : AppSession<NcsUser, NcsRequestInfo>
    {
        UserData data = new UserData();
    }
}