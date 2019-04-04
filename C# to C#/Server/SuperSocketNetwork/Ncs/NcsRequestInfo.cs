using SuperSocket.SocketBase.Protocol;
using System;
using CGD;

namespace SuperSocketNetwork.Ncs
{
    public class NcsRequestInfo : RequestInfo<NcsRequestInfo>
    {
        public int offset = 0;

        public int Key { get; }

        public byte[] Body { get; }
        public CGD.buffer Buffer { get; }

        public NcsRequestInfo(int key, byte[] body, byte[] buffer)
        {
            this.Key = key;
            this.Body = body;
            Buffer = new CGD.buffer(buffer, 0, buffer.Length);
        }
    }
}
