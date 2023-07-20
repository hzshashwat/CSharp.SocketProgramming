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

        List<TcpClient> mClients;

        public bool KeepRunning { get; set; }

        public ServerSocket()
        {
                mClients = new List<TcpClient>();
        }

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
                    mClients.Add(returnedByAccept);

                    System.Diagnostics.Debug.WriteLine(
                        string.Format("Client Connected successfully. Count: {0} - {1}", mClients.Count, returnedByAccept.Client.RemoteEndPoint)
                        );

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
                        RemoveClient(paramClient);

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
                RemoveClient(paramClient);
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
        }

        private void RemoveClient(TcpClient paramClient)
        {
            if(mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
                Debug.WriteLine(String.Format("Client removed, count: {0}", mClients.Count));
            }
        }

        public async void SendToAll(string leMessage)
        {
            if (string.IsNullOrEmpty(leMessage))
            {
                return;
            }

            try
            {
                byte[] buffMessage = Encoding.ASCII.GetBytes(leMessage);

                foreach(TcpClient c in mClients)
                {
                    c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                }
            }
            catch (Exception excp)  
            {
                Debug.WriteLine(excp.ToString());
            }
        }

        public void StopServer()
        {
            try
            {
                if (mTcpListener != null)
                {
                    mTcpListener.Stop();
                }

            foreach (TcpClient c in mClients)
                {
                    c.Close();
                }

                mClients.Clear();

            }

            catch(Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }
    }
}
