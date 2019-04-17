using System;
using Ncs.Function;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;

namespace Ncs.Server
{
    public class NcsReceiveFilter : FixedHeaderReceiveFilter<NcsRequestInfo>
    {
        public NcsReceiveFilter() : base(NcsMain.Option.HeaderSize)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return NcsMain.Option.ReceiveFunc(header, offset, length) - NcsMain.Option.HeaderSize;
        }

        protected override NcsRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var byteTmp = bodyBuffer.CloneRange(offset, length);
            return new NcsRequestInfo(byteTmp, ByteFunction.Combine(header.Array, byteTmp));
        }
    }
}