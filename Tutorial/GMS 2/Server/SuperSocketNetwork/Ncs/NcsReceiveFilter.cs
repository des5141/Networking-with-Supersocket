using System;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using SuperSocketNetwork.Function;

namespace SuperSocketNetwork.Ncs
{
    public class NcsReceiveFilter : FixedHeaderReceiveFilter<NcsRequestInfo>
    {
        public NcsReceiveFilter() : base(6) // header size = 6
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return (int) header[offset] +
                   (int) header[offset + 1] * 256 +
                   (int) header[offset + 2] * 65535 +
                   (int) header[offset + 3] * 16777216 - 6;
        }

        protected override NcsRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var byteTmp = bodyBuffer.CloneRange(offset, length);
            return new NcsRequestInfo((int)header.Array[header.Offset + 4] + (int)header.Array[header.Offset + 5] * 256, byteTmp, ByteFunction.Combine(header.Array, byteTmp));
        }
    }
}
