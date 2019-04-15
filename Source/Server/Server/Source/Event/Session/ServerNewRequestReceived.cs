using System;
using System.Text;

namespace Networking_with_Supersocket
{
    class ServerNewRequestReceived
    {
        public static void func(NcsUser user, NcsRequestInfo requestInfo)
        {
            var buffer = requestInfo.Buffer;
            UInt32 bufferLength = buffer.extract_uint();
            ushort signal = buffer.extract_ushort();
            switch (signal)
            {
                case 1:
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
