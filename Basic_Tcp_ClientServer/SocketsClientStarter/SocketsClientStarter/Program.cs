using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketsClientStarter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = null;

            try
            {
                Console.WriteLine("Welcome to hzshashwat's Client Starter...");
                Console.WriteLine("Please type a valid server IP Address and press enter.");

                string strIPAddress = Console.ReadLine();

                Console.WriteLine("Please supply a valid port number 0 - 65535 and press Enter");
                string strPortInput = Console.ReadLine();
                int nPortInput = 0;

                if(!IPAddress.TryParse(strIPAddress, out ipaddr))
                {
                    Console.WriteLine("Invalid server IP supplied.");
                    return;
                }
                if(!int.TryParse(strPortInput, out nPortInput))
                {
                    Console.WriteLine("Invalid port number supplied, return.");
                    return;
                }

                System.Console.WriteLine(string.Format("IPAdress: {0} - Port: {1}", ipaddr.ToString(), nPortInput));

                client.Connect(ipaddr, nPortInput);

                Console.ReadKey();

            }
            catch(Exception excp) 
            {

            }

        }
    }
}
