using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGD;

namespace Ncs.ConsoleTest
{
    public static class NcsTemplateBuffer
    {
        public static buffer HeartbeatBuffer1 = new buffer(32);
        public static buffer HeartbeatBuffer2 = new buffer(32);

        public static void SetTempBuffer()
        {
            NcsTemplateBuffer.HeartbeatBuffer1.append<uint>(0);
            NcsTemplateBuffer.HeartbeatBuffer1.append<ushort>(1);
            NcsTemplateBuffer.HeartbeatBuffer1.set_front<uint>(HeartbeatBuffer1.Count);

            NcsTemplateBuffer.HeartbeatBuffer2.append<uint>(0);
            NcsTemplateBuffer.HeartbeatBuffer2.append<ushort>(2);
            NcsTemplateBuffer.HeartbeatBuffer2.set_front<uint>(HeartbeatBuffer2.Count);
        }

    }
}
