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

                Console.WriteLine("Connected to the server. Type the message and press enter to send it to the server. Type <EXIT> to close.");

                string inputCommand = string.Empty;

                while(true)
                {
                    inputCommand = Console.ReadLine();

                    if(inputCommand.Equals("<EXIT>"))
                    {
                        break;
                    }

                    byte[] buffSend = Encoding.ASCII.GetBytes(inputCommand);
                    client.Send(buffSend);

                    byte[] buffRecieved = new byte[1024];
                    int noRecvBytes = client.Receive(buffRecieved);

                    Console.WriteLine("Data recieved: {0}", Encoding.ASCII.GetString(buffRecieved, 0, noRecvBytes));
                }

            }
            catch(Exception excp) 
            {
                Console.WriteLine(excp.ToString());
            }

            finally
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                    client.Dispose();
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
