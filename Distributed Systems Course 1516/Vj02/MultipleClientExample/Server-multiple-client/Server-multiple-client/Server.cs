using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server_multiple_client
{
    class Server
    {
        private readonly TcpListener tcpListener;
        private readonly CancellationToken _ct;
        private readonly Task listenTask;

        private int id = 0;
        // zapamtit cemo sve klijente koji se spoje
        private readonly List<Connection> connections = new List<Connection>();

        // saljemo CancellationToken kako bismo mogli prekinuti izvrsavanje servera kada korisnik to pozeli
        public Server(string ip, int port, CancellationToken ct)
        {
            IPAddress address = IPAddress.Parse(ip);
            
            tcpListener = new TcpListener(address, port);

            tcpListener.Start();

            _ct = ct;

            listenTask = Task.Factory.StartNew(() => ListenLoop());
        }

        public async void ListenLoop()
        {
            Console.WriteLine("Server zapocinje sa slusanjem na ulazne konekcije...");
            // zelimo imati mogucnost vise konekcija posluziti
            // petlja ce se izvrsavati dok se ne zatrazi prekid rada servera
            while(!_ct.IsCancellationRequested)
            {
                // asinkrono cekamo na konekciju
                TcpClient client = await tcpListener.AcceptTcpClientAsync();

                Connection conn = new Connection(id, client);

                connections.Add(conn);

                // U novoj niti cemo posluziti klijenta
                Task.Factory.StartNew(() => conn.Do(_ct));

                id++;
            }
        }
    }
}
