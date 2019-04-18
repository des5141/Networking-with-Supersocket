using System;
using Ncs.Function;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;

namespace Ncs.Server
{
    public class NcsReceiveFilter : FixedHeaderReceiveFilter<NcsRequestInfo>
    {
        public NcsReceiveFilter() : base(NcsDefine.Option.HeaderSize)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return NcsDefine.Option.ReceiveFunc(header, offset, length) - NcsDefine.Option.HeaderSize;
        }

        protected override NcsRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var byteTmp = bodyBuffer.CloneRange(offset, length);
            return new NcsRequestInfo(byteTmp, ByteFunction.Combine(header.Array, byteTmp));
        }
    }
}