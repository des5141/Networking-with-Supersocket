using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CGD;

namespace Networking_with_Supersocket
{
    public static class NcsTemplateBuffer
    {
        public static buffer HeartbeatBuffer1 = new buffer(16);
        public static buffer HeartbeatBuffer2 = new buffer(16);

        public static void SetTempBuffer()
        {
            NcsTemplateBuffer.HeartbeatBuffer1.append<UInt32>(0);
            NcsTemplateBuffer.HeartbeatBuffer1.append<ushort>(1);
            NcsTemplateBuffer.HeartbeatBuffer1.set_front<UInt32>(HeartbeatBuffer1.Count);

            NcsTemplateBuffer.HeartbeatBuffer2.append<UInt32>(0);
            NcsTemplateBuffer.HeartbeatBuffer2.append<ushort>(2);
            NcsTemplateBuffer.HeartbeatBuffer2.set_front<UInt32>(HeartbeatBuffer2.Count);
        }

    }
}