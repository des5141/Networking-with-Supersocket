using System;
using System.Text;

namespace Networking_with_Supersocket
{
    class ServerNewRequestReceived
    {
        public static void func(NcsUser user, NcsRequestInfo requestInfo)
        {
            for (int i = 0; i < requestInfo.Buffer.len; i++)
                Console.WriteLine(requestInfo.Buffer.buf[i]);
            var buffer = requestInfo.Buffer;
            UInt32 bufferLength = buffer.extract_uint();
            ushort signal = buffer.extract_ushort();
            switch (signal)
            {
                case Signal.signal_heartbeat_first:
                    user.Send(NcsTemplateBuffer.HeartbeatBuffer2);
                    user.heartbeat = true;
                    //Encoding.UTF8.GetString(buffer.extract<byte[]>())
                    break;

                default:
                    Console.WriteLine("unvaild : " + signal);
                    break;
            }
        }
    }
}
