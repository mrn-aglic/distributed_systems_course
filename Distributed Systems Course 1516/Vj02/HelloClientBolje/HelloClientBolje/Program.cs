using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloClientBolje
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 8080;

            Client client = new Client(ip, port);

            Console.ReadLine();
        }
    }
}
