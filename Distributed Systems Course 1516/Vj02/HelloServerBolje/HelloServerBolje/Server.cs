using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HelloServerBolje
{
    class Server
    {
        private readonly int _port;
        private readonly IPAddress _ip;
        private readonly TcpListener tcpListener;
        private MyConnection connection;

        public Server( string ip, int port)
        {
            _port = port;
            _ip = IPAddress.Parse(ip);


            Console.WriteLine("Starting server...");

            // Stvori TcpListener-a 
            tcpListener = new TcpListener(_ip, port);
            // Zapocmi sa slusanjem na dolazece konekcije
            tcpListener.Start();

            // Pozovemo metodu koja ce slusati na dolazece konekcije
            Listen();
        }

        private void Listen()
        {
            TcpClient client = tcpListener.AcceptTcpClient();

            connection = new MyConnection(client);
        }
    }
}
