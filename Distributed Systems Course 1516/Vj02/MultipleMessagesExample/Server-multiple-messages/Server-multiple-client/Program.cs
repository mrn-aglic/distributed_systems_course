using System;
using System.Threading;

namespace Server_multiple_messages
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

            string read = "";
            Console.WriteLine("Za prekid unesite q i pritisnite <ENTER>");

            do
            {
                read = Console.ReadLine();
            } while (read != "q");

            cts.Cancel();

            Console.ReadLine();
        }
    }
}
