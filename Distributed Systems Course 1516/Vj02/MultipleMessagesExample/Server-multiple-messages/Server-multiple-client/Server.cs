using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server_multiple_messages
{
    class Server
    {
        private readonly TcpListener tcpListener;
        private readonly CancellationToken _ct;
        private readonly Task listenTask;

        // saljemo CancellationToken kako bismo mogli prekinuti izvrsavanje servera kada korisnik to pozeli
        public Server(string ip, int port, CancellationToken ct)
        {
            IPAddress address = IPAddress.Parse(ip);
            
            tcpListener = new TcpListener(address, port);
            tcpListener.Start();

            _ct = ct;

            listenTask = Task.Factory.StartNew(() => ListenLoop());
        }

        private void ListenLoop()
        {
            Console.WriteLine("Server zapocinje sa slusanjem na ulazne konekcije...");

            TcpClient client = tcpListener.AcceptTcpClient();

            Connection conn = new Connection(client);

            conn.Do(_ct);
        }
    }
}
