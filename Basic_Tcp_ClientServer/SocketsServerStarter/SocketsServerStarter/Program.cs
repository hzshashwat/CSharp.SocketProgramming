using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

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

            listenerSocket.Bind(ipep);

            listenerSocket.Listen(5);
            //This defines how many clients can wait for connection at time when system is busy handling other client.
            listenerSocket.Accept();
            // Accept is Blocking Operation. That means our program will not move until this operation is finished.

        }
    }
}
