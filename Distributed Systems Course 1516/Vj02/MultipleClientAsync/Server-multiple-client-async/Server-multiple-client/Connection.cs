using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_multiple_client
{
    class Connection
    {
        public int Id { get; private set; }

        private TcpClient _tcpClient;
        private StreamReader _reader;
        private NetworkStream _stream;

        public Connection(int id, TcpClient tcpClient)
        {
            Id = id;

            _tcpClient = tcpClient;

            _stream = _tcpClient.GetStream();
        }

        public async void Do(CancellationToken ct)
        {
            bool error = false;
            
            // ulazni buffer
            byte[] inBuffer = new byte[4096];

            while (!ct.IsCancellationRequested && !error)
            {
                try
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        StreamReader reader = new StreamReader(memStream);

                        int readBytes = await _stream.ReadAsync(inBuffer, 0, inBuffer.Length);

                        await memStream.WriteAsync(inBuffer, 0, readBytes);
                        await memStream.FlushAsync();

                        memStream.Seek(0, SeekOrigin.Begin);

                        string received = await reader.ReadLineAsync();

                        Console.WriteLine(Id + " received: " + received);

                        string response = "200 OK";

                        byte[] outBuffer = Encoding.UTF8.GetBytes(response);

                        await _stream.WriteAsync(outBuffer, 0, outBuffer.Length);
                        await _stream.FlushAsync();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("An exception occured");
                    Console.WriteLine(ex.Message);
                    error = true;
                }
            }

            _stream.Close();
            _stream.Dispose();
            _tcpClient.Close();
        }
    }
}
