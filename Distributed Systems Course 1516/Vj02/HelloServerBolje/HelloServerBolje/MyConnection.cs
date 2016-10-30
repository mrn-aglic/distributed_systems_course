using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HelloServerBolje
{
    class MyConnection
    {
        private readonly NetworkStream stream;
        private readonly MemoryStream memStream = new MemoryStream();
        private readonly StreamReader reader;
        private readonly TcpClient _tcpClient;
        private readonly byte[] inBuffer = new byte[4096];

        public MyConnection(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            stream = _tcpClient.GetStream();

            reader = new StreamReader(memStream);

            Receive();
        }

        private void Receive()
        {
            // procitamo podatake s mreze i kao povratni rezultat dobijemo koliko ih je procitano
            int bytesRead = stream.Read(inBuffer, 0, inBuffer.Length);

            // zapisi podatke u memorystream pocevsi od 0-tog (prvog) do onoliko koliko ih je procitano
            memStream.Write(inBuffer, 0, bytesRead);

            // pozicioniramo se na pocetak MemoryStreama kako bismo mogli citati iz njega
            memStream.Seek(0, SeekOrigin.Begin);

            // uz pomoc StreamReadera procitamo bajtove streama i pretvorimo ih u string
            string msg = reader.ReadLine();

            Console.WriteLine("Primljena poruka: " + msg);

            // odgovorimo klijentu
            Send();
        }

        private void Send()
        {
            // odgovor koji zelimo poslati
            string odgovor = "200 OK";

            // izlazni buffer
            byte[] outBuffer = Encoding.UTF8.GetBytes(odgovor);

            // zapisimo u mrezni stream bajtove
            stream.Write(outBuffer, 0, outBuffer.Length);
            stream.Flush();

            CloseConnection();
        }

        private void CloseConnection()
        {
            Console.WriteLine("Closing server...");

            stream.Dispose();
            memStream.Dispose();
            _tcpClient.Close();
        }
    }
}
