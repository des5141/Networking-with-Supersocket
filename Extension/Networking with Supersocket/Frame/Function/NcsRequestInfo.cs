using SuperSocket.SocketBase.Protocol;

namespace Networking_with_Supersocket
{
    public class NcsRequestInfo : RequestInfo<NcsRequestInfo>
    {
        public int offset = 0;
        public new byte[] Body { get; }
        public CGD.buffer Buffer { get; }
        public NcsRequestInfo(byte[] body, byte[] buffer)
        {
            this.Body = body;
            Buffer = new CGD.buffer(buffer, 0, buffer.Length);
        }
    }
}
