﻿using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_multiple_msgs
{
    class Client
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private IPAddress _ip;

        public Client(string ip, int port, CancellationToken ct)
        {
            _ip = IPAddress.Parse(ip);

            _tcpClient = new TcpClient();

            _tcpClient.Connect(_ip, port);

            _networkStream = _tcpClient.GetStream();
        }

        public async Task<string> ReadAsync()
        {
            byte[] buffer = new byte[4096];

            int bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length);

            using (MemoryStream memStream = new MemoryStream())
            {
                await memStream.WriteAsync(buffer, 0, bytesRead);
                memStream.Seek(0, SeekOrigin.Begin);

                StreamReader reader = new StreamReader(memStream);

                string msg = await reader.ReadLineAsync();

                return msg;
            }
        }

        public string Read()
        {
            byte[] buffer = new byte[4096];

            int bytesRead = _networkStream.Read(buffer, 0, buffer.Length);

            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(buffer, 0, bytesRead);
                memStream.Seek(0, SeekOrigin.Begin);

                StreamReader reader = new StreamReader(memStream);

                string msg = reader.ReadLine();

                return msg;
            }
        }

        public async void WriteAsync(string msg)
        {
            byte[] outBuffer = Encoding.UTF8.GetBytes(msg);

            await _networkStream.WriteAsync(outBuffer, 0, outBuffer.Length);
        }

        public void Write(string msg)
        {
            byte[] outBuffer = Encoding.UTF8.GetBytes(msg);

            _networkStream.Write(outBuffer, 0, outBuffer.Length);
        }

        public void Close()
        {
            _networkStream.Close();
            _networkStream.Dispose();
            _tcpClient.Close();
        }
    }
}
