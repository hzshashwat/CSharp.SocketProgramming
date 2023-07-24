using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocketAsync
{
    public class ClientSocket
    {
        IPAddress mServerIPAddress;
        int mServerPort;
        TcpClient mClient;

        public ClientSocket()
        {
            mClient = null;
            mServerIPAddress = null;
            mServerPort = -1;
        }

        public IPAddress ServerIPAddress
        {
            get { return mServerIPAddress; }
        }
        
        public int ServerPort
        {
            get { return mServerPort; }
        }

        public bool SetServerIPAddress(string _IPAddressServer)
        {
            IPAddress ipaddr = null;

            if(!IPAddress.TryParse(_IPAddressServer, out ipaddr))
            {
                Console.WriteLine("Invalid server IP supplied.");
                return false;
            }

            mServerIPAddress = ipaddr;

            return true;
        }

        public bool SetPortNumber(string _ServerPort)
        {
            int portNumber = 0;

            if (!int.TryParse(_ServerPort.Trim(), out portNumber))
            {
                Console.WriteLine("Invalid Port number supplied, return.");
                return false;
            }

            if (portNumber < 0 || portNumber > 65535)
            {
                Console.WriteLine("Port number must be between 0 & 65535");
                return false;
            }
            
            mServerPort = portNumber;
            return true;
        }

        public async Task ConnectToServer()
        {
            if(mClient == null)
            {
                mClient = new TcpClient();
            }

            try
            {
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                Console.WriteLine(string.Format("Connected to server IP/Port: {0} / {1}",
                    mServerIPAddress, mServerPort));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
