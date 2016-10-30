using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HelloClientBolje
{
    class Client
    {
        private NetworkStream stream;
        private StreamReader reader;

        private readonly MemoryStream memStream = new MemoryStream();
        private readonly TcpClient tcpClient;
        private readonly IPAddress _ip;
        private readonly byte[] buffer = new byte[4096];
 
        // Konstruktor za Client-a
        public Client(string ip, int port)
        {
            _ip = IPAddress.Parse(ip);

            // stvorimo TcpClient
            tcpClient = new TcpClient();

            // inicijaliziramo konekciju
            tcpClient.Connect(_ip, port);

            Connection();
        }

        private void Connection()
        {

            // ako je konekcija uspjesna
            if (tcpClient.Connected)
            {
                // dobavi NetworkStream konekcije
                stream = tcpClient.GetStream();
                reader = new StreamReader(memStream);

                Send();
            }
        }

        private void Send()
        {
            Console.WriteLine("Vasa poruka:> ");
            string msg = Console.ReadLine();

            byte[] outBuffer = Encoding.UTF8.GetBytes(msg);

            // zapisemo podatke u stream
            stream.Write(outBuffer, 0, outBuffer.Length);
            stream.Flush();

            Receive();
        }

        private void Receive()
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            memStream.Write(buffer, 0, bytesRead);
            memStream.Seek(0, SeekOrigin.Begin);

            string msg = reader.ReadLine();

            Console.WriteLine(msg);
            
            CloseConnection();
        }
        
        private void CloseConnection()
        {
            Console.WriteLine("Closing server...");

            stream.Dispose();
            memStream.Dispose();
        }
    }
}
