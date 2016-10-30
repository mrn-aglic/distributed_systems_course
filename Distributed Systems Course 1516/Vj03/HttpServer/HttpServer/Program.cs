using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string example = @"GET /hello.htm HTTP/1.1
                                User-Agent: Mozilla/4.0 (compatible; MSIE5.01; Windows NT)
                                Host: www.tutorialspoint.com
                                Accept-Language: en-us
                                Accept-Encoding: gzip, deflate
                                Connection: Keep-Alive
                                
                                Hello";

            string ip = "127.0.0.1";
            int port = 12000;

            CancellationTokenSource cts = new CancellationTokenSource();

            Server server = new Server(ip, port, cts.Token);

            Console.WriteLine("Enter q + <enter> to terminate");

            do
            {

            } while (Console.ReadLine().ToLower() != "q");

            Console.WriteLine("Server shutting down...");

            cts.Cancel();

            Console.ReadLine();
        }
    }
}
