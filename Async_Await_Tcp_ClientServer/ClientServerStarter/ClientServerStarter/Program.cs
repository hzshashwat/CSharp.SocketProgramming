using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSocketAsync;

namespace ClientServerStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientSocket client = new ClientSocket();

            Console.WriteLine("*** Welcome to Socket Client Starter by hzshashwat");
            Console.WriteLine("Please Type a valid Server IP Address and Press Enter: ");

            string strIPAddress = Console.ReadLine();

            Console.WriteLine("Please Supply a Valid Port Number 0 - 65535 and Press Enter: ");
            string strPortInput = Console.ReadLine();


            if(!client.SetServerIPAddress(strIPAddress) || !client.SetPortNumber(strPortInput))
            {
                Console.WriteLine(
                    string.Format("Wrong IP Address or Port Number supplied - {0} - {1} - Press any key to exit",
                    strIPAddress,strPortInput));
                Console.ReadLine();
                return;
            }

            client.ConnectToServer();

            string strInputUser = null;

            do
            {
                strInputUser = Console.ReadLine();
            } while (strInputUser != "<EXIT>");
        }
    }
}
