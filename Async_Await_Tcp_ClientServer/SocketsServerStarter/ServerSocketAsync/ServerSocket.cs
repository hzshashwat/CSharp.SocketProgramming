using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocketAsync
{
    public class ServerSocket
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTcpListener;

        public async void StartListeningForIncomingConnection(IPAddress iPAddr = null, int iPort = 23000)
        {
            if (iPAddr == null)
            {
                iPAddr = IPAddress.Any;
            }

            if (iPort <= 0)
            {
                iPort = 23000;
            }

            mIP = iPAddr;
            mPort = iPort;

            System.Diagnostics.Debug.WriteLine(string.Format("IP Address: {0} - Port: {1}", mIP.ToString(), mPort));

            mTcpListener = new TcpListener(mIP, mPort);
            mTcpListener.Start();

            var returnedByAccept = await mTcpListener.AcceptTcpClientAsync();

            System.Diagnostics.Debug.WriteLine("Client Connected successfully: " + returnedByAccept.ToString());
        }

    }
}
