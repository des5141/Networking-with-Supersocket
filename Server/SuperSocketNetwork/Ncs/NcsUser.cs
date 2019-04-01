using System;
using System.IO;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketNetwork.Ncs
{
    public class NcsUser : AppSession<NcsUser, NcsRequestInfo>
    {
        /// Variables Init
        public bool heartbeat = false;
        private short heartbeat_count = 0;
        public bool instance_die = false;
        public string Pid = String.Empty;
        public int space = -1; // -1 is Null
        public bool authentication = false;

        ///  Override Functions
        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }

        protected override void HandleException(Exception e)
        {
            Console.WriteLine("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
        

        ///  Not Override Functions
        public void heartbeat_start()
        {
            new Task(async () =>
            {
                if (heartbeat_count >= 3)
                    heartbeat = false;
                else
                    heartbeat_count++;

                NcsBuffer buffer = new NcsBuffer(Program.signal_heartbeat_first, Program.SendToClient, Program.MySpace);
                buffer.push_size();
                Send(buffer.write_buffer, 0, buffer.write_offset);

                await Task.Delay(3000);

                if ((heartbeat == false) && (heartbeat_count >= 3) || (instance_die == true))
                {
                    this.Close();
                }
                else
                    heartbeat_start();
                
                if (heartbeat_count >= 3)
                    heartbeat_count = 0;
            }).Start();
        }
    }
}