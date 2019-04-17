using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSocket.ClientEngine;

namespace SuperSocketNetwork_WindowsForms.Ncs
{
    public class NcsMain
    {
        public int ping = 0;

        private AsyncTcpSession tcpSession;
        private Stopwatch pingStopwatch = new Stopwatch();

        #region NcsMain_init

        public NcsMain(Form1 form)
        {
            NcsTemplateBuffer.SetTempBuffer();

            tcpSession = new AsyncTcpSession(new IPEndPoint(IPAddress.Parse("192.168.0.17"), 65535));

            tcpSession.Connected += tcpSession_Connected;
            tcpSession.Closed += tcpSession_Closed;
            tcpSession.DataReceived += tcpSession_DataReceived;
            tcpSession.Error += new EventHandler<ErrorEventArgs>(tcpSession_Error);
            tcpSession.Connect();
        }

        public NcsMain(string ip, string port, Form1 form)
        {
            NcsTemplateBuffer.SetTempBuffer();

            tcpSession = new AsyncTcpSession(new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port)));

            tcpSession.Connected += form.tcpSession_Connected;
            tcpSession.Closed += form.tcpSession_Closed;
            tcpSession.DataReceived += form.tcpSession_DataReceived;
            tcpSession.Error += new EventHandler<ErrorEventArgs>(form.tcpSession_Error);
            tcpSession.Connect();
        }

        public NcsMain(string ip, int port, Form1 form)
        {
            NcsTemplateBuffer.SetTempBuffer();

            tcpSession = new AsyncTcpSession(new IPEndPoint(IPAddress.Parse(ip), port));

            tcpSession.Connected += tcpSession_Connected;
            tcpSession.Closed += tcpSession_Closed;
            tcpSession.DataReceived += tcpSession_DataReceived;
            tcpSession.Error += new EventHandler<ErrorEventArgs>(tcpSession_Error);
            tcpSession.Connect();
        }


        public NcsMain(IPEndPoint ipEndPoint, Form1 form)
        {
            NcsTemplateBuffer.SetTempBuffer();

            tcpSession = new AsyncTcpSession(ipEndPoint);

            tcpSession.Connected += tcpSession_Connected;
            tcpSession.Closed += tcpSession_Closed;
            tcpSession.DataReceived += tcpSession_DataReceived;
            tcpSession.Error += new EventHandler<ErrorEventArgs>(tcpSession_Error);
            tcpSession.Connect();
        }

        #endregion

        private void tcpSession_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "오류");
        }

        private void tcpSession_DataReceived(object sender, DataEventArgs e)
        {
            AsyncTcpSession session = sender as AsyncTcpSession;

            byte[] tmpBuffer = e.Data;
            var buffer = new CGD.buffer(e.Data, 0, e.Length);

            int bufferLength = (int)buffer.extract_uint();
            ushort bufferType = (ushort)buffer.extract_byte();
            
            switch (bufferType)
            {
                case 1:
                    pingStopwatch.Start();
                    session.Send(NcsTemplateBuffer.HeartbeatBuffer1);
                    break;
                case 2:
                    ping = (int)pingStopwatch.ElapsedMilliseconds;
                    pingStopwatch.Reset();
                    break;
            }
        }

        private void tcpSession_Closed(object sender, EventArgs e)
        {

        }

        private void tcpSession_Connected(object sender, EventArgs e)
        {

        }
    }
}
