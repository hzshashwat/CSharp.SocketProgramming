using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerSocketAsync;

namespace SocketsServerStarter
{
    public partial class Form1 : Form
    {
        ServerSocket mServer;
        public Form1()
        {
            InitializeComponent();
            mServer = new ServerSocket();
        }

        private void btnAcceptIncomingConnections_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }
    }
}
