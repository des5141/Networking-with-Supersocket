using System;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;

namespace Networking_with_Supersocket
{
    public class NcsReceiveFilter : FixedHeaderReceiveFilter<NcsRequestInfo>
    {
        public NcsReceiveFilter() : base(4) { }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return (int)header[offset] +
                   (int)header[offset + 1] * 256 +
                   (int)header[offset + 2] * 65535 +
                   (int)header[offset + 3] * 16777216 - 4;
        }
        protected override NcsRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var byteTmp = bodyBuffer.CloneRange(offset, length);
            return new NcsRequestInfo(byteTmp, NcsByteFunction.Combine(header.Array, byteTmp));
        }
    }
}
