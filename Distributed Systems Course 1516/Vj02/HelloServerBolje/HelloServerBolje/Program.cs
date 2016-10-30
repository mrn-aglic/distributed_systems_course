using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloServerBolje
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 8080;

            Server server = new Server(ip, port);

            Console.ReadLine();
        }
    }
}
