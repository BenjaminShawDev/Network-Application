using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private const string serverIP = "127.0.0.1";
        private const int serverPort = 4444;

        static void Main(string[] args)
        {
            Client client = new Client();
            //client.Connect("127.0.0.1", 4444);

            if (client.Connect(serverIP, serverPort))
            {
                Console.WriteLine("Connected...");
                client.Run();
            }
        }
    }
}
