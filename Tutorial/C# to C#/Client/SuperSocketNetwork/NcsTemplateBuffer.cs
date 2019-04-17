using CGD;

namespace SuperSocketNetwork_Console
{
    public static class NcsTemplateBuffer
    {
        public static buffer HeartbeatBuffer1 = new buffer(16);
        public static buffer HeartbeatBuffer2 = new buffer(16);

        public static void SetTempBuffer()
        {
            NcsTemplateBuffer.HeartbeatBuffer1.append<ushort>(0);
            NcsTemplateBuffer.HeartbeatBuffer1.append<ushort>(1);
            NcsTemplateBuffer.HeartbeatBuffer1.set_front<ushort>(HeartbeatBuffer1.Count);

            NcsTemplateBuffer.HeartbeatBuffer2.append<ushort>(0);
            NcsTemplateBuffer.HeartbeatBuffer2.append<ushort>(2);
            NcsTemplateBuffer.HeartbeatBuffer2.set_front<ushort>(HeartbeatBuffer2.Count);
        }

    }
}
