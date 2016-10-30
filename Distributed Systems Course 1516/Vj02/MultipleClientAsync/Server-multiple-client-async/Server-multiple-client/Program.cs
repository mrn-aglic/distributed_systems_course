using System;
using System.Threading;

namespace Server_multiple_client
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 8080;

            // izvor za cancellation token
            CancellationTokenSource cts = new CancellationTokenSource();

            // Token od cts-a
            CancellationToken ct = cts.Token;

            Server server = new Server(ip, port, ct);

            Console.WriteLine("Za prekid pritisnite <ENTER>");
            Console.ReadLine();

            cts.Cancel();

            Console.ReadLine();
        }
    }
}
