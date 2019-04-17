using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSocket.ClientEngine;
using SuperSocketNetwork_WindowsForms.Ncs;

namespace SuperSocketNetwork_WindowsForms
{
    public partial class Form1 : Form
    {
        private NcsMain serverMain;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (textBoxIP.Text.Length > 1 && textBoxPort.Text.Length > 1)
            {
                serverMain = new NcsMain(textBoxIP.Text, textBoxPort.Text, this);
            }
            else
            {
                MessageBox.Show("IP 와 Port 를 확인하여 주십시오");
            }
        }

        internal void tcpSession_Connected(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void tcpSession_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void tcpSession_DataReceived(object sender, DataEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void tcpSession_Error(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
