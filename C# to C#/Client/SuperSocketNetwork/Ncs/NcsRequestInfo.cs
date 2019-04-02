using SuperSocket.SocketBase.Protocol;
using System;

namespace SuperSocketNetwork.Ncs
{
    public class NcsRequestInfo : RequestInfo<NcsRequestInfo>
    {
        public int offset = 0;

        public int Key { get; }

        public byte[] Body { get; }

        public byte[] Buffer { get; }

        public NcsRequestInfo(int key, byte[] body, byte[] buffer = null)
        {
            this.Key = key;
            this.Body = body;
            this.Buffer = buffer;
        }
    }
}
