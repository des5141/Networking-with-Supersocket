using System;

namespace Ncs.Server
{
    public class NcsOption
    {
        internal Func<CGD.buffer, dynamic> TypeFunc;
        internal Func<byte[], int, int, dynamic> ReceiveFunc;
        internal int HeaderSize;

        /// <summary>
        /// 기본 NcsOption을 만듭니다
        /// </summary>
        public NcsOption()
        {
            TypeFunc = buffer => buffer.get_front_ushort(4);

            ReceiveFunc = (byte[] header, int offset, int length) =>
            {
                return (int)header[offset] +
                       (int)header[offset + 1] * 256 +
                       (int)header[offset + 2] * 65535 +
                       (int)header[offset + 3] * 16777216;
            };

            HeaderSize = 6;
        }

        /// <summary>
        /// typeFunc, headerSize를 정의한 NcsOption 을 만듭니다
        /// </summary>
        /// <param name="typeFunc">타입를 지정하는 값으로 Routing 에 사용됩니다</param>
        /// <param name="headerSize">해더의 크기로 두 함수의 길이를 더한 값입니다.
        /// 기본적으로 TypeFunc는 2의 길이 ReceiveFunc는 4의 길이를 가지고 있습니다</param>
        public NcsOption(Func<CGD.buffer, dynamic> typeFunc, int headerSize)
        {
            TypeFunc = typeFunc;

            ReceiveFunc = (byte[] header, int offset, int length) =>
            {
                return (int)header[offset] +
                       (int)header[offset + 1] * 256 +
                       (int)header[offset + 2] * 65535 +
                       (int)header[offset + 3] * 16777216;
            };

            HeaderSize = headerSize;
        }

        /// <summary>
        /// receiveFunc, headerSize를 정의한 NcsOption 을 만듭니다
        /// </summary>
        /// <param name="receiveFunc">패킷을 받을때 사용하는 값으로 패킷의 길이를 가져옵니다.</param>
        /// <param name="headerSize">해더의 크기로 두 함수의 길이를 더한 값입니다.
        /// 기본적으로 TypeFunc는 2의 길이 ReceiveFunc는 4의 길이를 가지고 있습니다</param>
        public NcsOption(Func<byte[], int, int, dynamic> receiveFunc, int headerSize)
        {
            TypeFunc = buffer => buffer.get_front_ushort(4);

            ReceiveFunc = receiveFunc;

            HeaderSize = headerSize;
        }

        /// <summary>
        /// typeFunc, receiveFunc, headerSize를 정의한 NcsOption 을 만듭니다
        /// </summary>
        /// <param name="typeFunc">타입를 지정하는 값으로 Routing 에 사용됩니다</param>
        /// <param name="receiveFunc">패킷을 받을때 사용하는 값으로 패킷의 길이를 가져옵니다.</param>
        /// <param name="headerSize">해더의 크기로 두 함수의 길이를 더한 값입니다.
        /// 기본적으로 TypeFunc는 2의 길이 ReceiveFunc는 4의 길이를 가지고 있습니다</param>
        public NcsOption(Func<CGD.buffer, dynamic> typeFunc, Func<byte[], int, int, dynamic> receiveFunc, int headerSize)
        {
            TypeFunc = typeFunc;

            ReceiveFunc = receiveFunc;

            HeaderSize = headerSize;
        }
    }
}