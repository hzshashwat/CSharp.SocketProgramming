using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketsServerStarter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Any;
            // Here Any means that localhost[127.0.0.1] or particular ip address assigned which can be found out by ipconfig.

            // IPAddress ipaddr = IPAddress.Parse("127.0.0.1");

            IPEndPoint ipep = new IPEndPoint(ipaddr, 5555);

            try
            {

                listenerSocket.Bind(ipep);

                listenerSocket.Listen(5);
                //This defines how many clients can wait for connection at time when system is busy handling other client.

                Socket client = listenerSocket.Accept();
                // Accept is Blocking Operation. That means our program will not move until this operation is finished.

                Console.WriteLine("Client connected. " + client.ToString() + " - IP End Point: " + client.RemoteEndPoint.ToString());

                byte[] buffer = new byte[1024];
                while (true)
                {

                    int numberofRecievedBytes = client.Receive(buffer);

                    string recievedText = Encoding.ASCII.GetString(buffer, 0, numberofRecievedBytes);

                    Console.WriteLine("Number of recieved bytes: " + numberofRecievedBytes);

                    Console.WriteLine("Data sent by client is: " + buffer);

                    Console.WriteLine("Data Sent by client is : {0}", recievedText);

                    if (recievedText == "x")
                    {
                        break;
                    }

                    string response = Console.ReadLine();
                    var responseBytes = Encoding.ASCII.GetBytes(response);
                    client.Send(responseBytes);

                    Array.Clear(buffer, 0, buffer.Length);
                    numberofRecievedBytes = 0;

                }
            }
            catch(Exception excp)
            {
                Console.WriteLine(excp.Message);
            }
        }
    }
}
