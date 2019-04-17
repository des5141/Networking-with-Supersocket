using System;

namespace Networking_with_Supersocket
{
    class ServerNewRequestReceived
    {
        public static void func(NcsUser user, NcsRequestInfo requestInfo)
        {
            var buffer = requestInfo.Buffer;
            ushort bufferLength = buffer.extract_ushort();
            ushort signal = buffer.extract_ushort();
            switch (signal)
            {
                case Signal.signal_heartbeat_first:
                    user.Send(NcsTemplateBuffer.HeartbeatBuffer2);
                    user.heartbeat = true;
                    break;

                default:
                    Console.WriteLine("unvaild : " + signal);
                    break;
            }
        }
    }
}
