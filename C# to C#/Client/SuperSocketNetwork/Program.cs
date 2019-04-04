using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CGD;
using SuperSocket.ClientEngine;

namespace SuperSocketNetwork_Console
{
    class Program
    {
        private static AsyncTcpSession tcpSession;
        private static int count = 0;
        private static int length = 0;
        private static Stopwatch pingStopwatch = new Stopwatch();
        static void Main(string[] args)
        {
            Console.WriteLine("DataReceived Count  : {0}", count);
            Console.WriteLine("DataReceived Length : {0}", length);
            Console.WriteLine();
            Console.WriteLine("Ping : 0");
            Console.CursorVisible = false;

            tcpSession = new AsyncTcpSession(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65535));

            tcpSession.Connected += tcpSession_Connected;
            tcpSession.Closed += tcpSession_Closed;
            tcpSession.DataReceived += tcpSession_DataReceived;
            tcpSession.Error += new EventHandler<ErrorEventArgs>(tcpSession_Error);
            tcpSession.Connect();

            while (Console.ReadLine() != "q")
            {

            }
        }

        private static void tcpSession_Error(object sender, ErrorEventArgs e)
        {

        }

        private static void tcpSession_DataReceived(object sender, DataEventArgs e)
        {
            AsyncTcpSession session = sender as AsyncTcpSession;

            byte[] tmpBuffer = e.Data;
            var buffer = new CGD.buffer(e.Data, 0, e.Length);

            int bufferLength = (int)buffer.extract_uint();
            ushort bufferType = (ushort)buffer.extract_byte();

            length += bufferLength;
            count++;

            Console.SetCursorPosition(22, 0);
            Console.Write(count);
            Console.SetCursorPosition(22, 1);
            Console.Write(length);

            switch (bufferType)
            {
                case 1:
                    Console.SetCursorPosition(7, 3);
                    Console.Write(pingStopwatch.ElapsedMilliseconds);
                    pingStopwatch.Stop();
                    session.Send(NcsTemplateBuffer.HeartbeatBuffer1);
                    break;
                case 2:
                    new Task( async () =>
                    {
                        await Task.Delay(1000);
                        session.Send(NcsTemplateBuffer.HeartbeatBuffer1);
                        pingStopwatch.Restart();
                    }).Start();
                    break;
            }
            
        }

        private static void tcpSession_Closed(object sender, EventArgs e)
        {

        }

        private static void tcpSession_Connected(object sender, EventArgs e)
        {
            pingStopwatch.Start();
        }
    }
}
