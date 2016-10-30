using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server_multiple_messages
{
    class Connection
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        public Connection(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;

            _networkStream = _tcpClient.GetStream();
        }

        public void Do(CancellationToken ct)
        {
            // ulazni buffer
            byte[] inBuffer = new byte[4096];
            bool error = false;

            // zelimo imati mogucnost razmjene vise poruka.
            // ova petlja ce se izvrsavati sve dok se ne zatrazi prekid ili se ne dogodi pogreska
            while(!ct.IsCancellationRequested && !error)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    StreamReader reader = new StreamReader(memStream);

                    try
                    {
                        int bytesRead = _networkStream.Read(inBuffer, 0, inBuffer.Length);

                        memStream.Write(inBuffer, 0, bytesRead);
                        memStream.Seek(0, SeekOrigin.Begin);

                        string msg = reader.ReadLine();

                        Console.WriteLine("Got from client: " + msg);

                        string response = "200 OK";

                        byte[] outBuffer = Encoding.UTF8.GetBytes(response);

                        _networkStream.Write(outBuffer, 0, outBuffer.Length);
                        _networkStream.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Dogodila se pogreska...");
                        Console.WriteLine(ex.Message);

                        error = true;
                    }
                }
            }

            _networkStream.Dispose();
            _tcpClient.Close();
        }
    }
}
