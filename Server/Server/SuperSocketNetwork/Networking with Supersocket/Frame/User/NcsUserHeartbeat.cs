using SuperSocket.SocketBase;
using System.Threading.Tasks;

namespace SuperSocketNetwork
{
    partial class NcsUser : AppSession<NcsUser, NcsRequestInfo>
    {
        public bool heartbeat = false;
        short heartbeat_count = 0;
        public void heartbeat_start()
        {
            new Task(async () =>
            {
                if (heartbeat_count >= 3)
                    heartbeat = false;
                else
                    heartbeat_count++;
                Send(NcsTemplateBuffer.HeartbeatBuffer1);
                await Task.Delay(1000);
                if ((heartbeat == false) && (heartbeat_count >= 3) || (die == true))
                    this.Close();
                else
                    heartbeat_start();
                if (heartbeat_count >= 3)
                    heartbeat_count = 0;
            }).Start();
        }
    }
}
