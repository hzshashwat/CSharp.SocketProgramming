using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ServerSocketAsync
{
    public class ServerSocket
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTcpListener;

        public bool KeepRunning { get; set; }

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

            try
            {
                mTcpListener.Start();


                KeepRunning = true; 
                while(KeepRunning)
                {
                    var returnedByAccept = await mTcpListener.AcceptTcpClientAsync();

                    System.Diagnostics.Debug.WriteLine("Client Connected successfully: " + returnedByAccept.ToString());

                    TakeCareOfTCPClient(returnedByAccept);
                }
            }

            catch(Exception excp)
            {
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
            
        }

        private async void TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[64];

                while (KeepRunning)
                {
                    Debug.WriteLine("*** Ready to read");

                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);
                    // If ReaderAsync returns 0, means the socket is closed & this reads data in char format instead of bytes.

                    if (nRet == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Socket Disconnected");
                        break;
                    }

                    string recievedText = new string(buff);

                    System.Diagnostics.Debug.WriteLine("*** RECIEVED: " + recievedText);

                    Array.Clear(buff, 0, buff.Length);
                }
            }

            catch (Exception excp)
            {
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
        }
    }
}
