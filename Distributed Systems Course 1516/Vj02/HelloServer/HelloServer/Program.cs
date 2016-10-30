using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HelloServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipString = "127.0.0.1";
            int port = 8080;

            // Stvaramo instancu ip adrese
            IPAddress ipAddress = IPAddress.Parse(ipString);
            
            Console.WriteLine("Starting the listener....");

            // Prilikom instanciranja listenera u konstruktor šaljemo ip adresu i port na kojem će slušati 
            // za nadolazeće konekcije. 
            TcpListener listener = new TcpListener(ipAddress, port);

            listener.Start();

            TcpClient socket = listener.AcceptTcpClient();

            NetworkStream stream = socket.GetStream();
            // Stream koji zapisuje u memoriju racunala
            MemoryStream memoryStream = new MemoryStream();
            StreamReader reader = new StreamReader(memoryStream);

            // buffer - spremnik za podatke koje čitamo
            byte[] inBuffer = new byte[4096];

            // pročitani podatci se spremaju u buffer
            int bytesRead = stream.Read(inBuffer, 0, inBuffer.Length);
            
            memoryStream.Write(inBuffer, 0, bytesRead);

            // Uz pomoc Seek metode se postavimo na neku poziciju unutar memory streama
            // Pokušajte promijeniti SeekOrigin na End.
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Pročitajmo poruku. 
            string line = reader.ReadLine();

            Console.WriteLine("Got msg: " + line);

            string response = "200 OK";

            byte[] buffer = Encoding.UTF8.GetBytes(response);

            // zapisimo podatke u network stream
            // parametri objasnjeni redom: 
            // - buffer iz kojeg citamo podatke - u ovom slucaju niz byteova "data"
            // - offset - od koje pozicije pocinjemo s citanjem podataka - 0
            // - size - koliko podataka zelimo zapisati - data.length
            stream.Write(buffer, 0, buffer.Length);
            // Obicno kada radimo sa streamovima, potrebno je pozvati Flush() metodu kako bi se podatci zapisali.
            // To nije slucaj sa NetworkStreamom kako pise na stranici microsofta:
            // "The Flush method implements the Stream.Flush method; however, because NetworkStream is not buffered, 
            // it has no affect on network streams. Calling the Flush method does not throw an exception."
            // Pozivamo ovu metodu cisto zbog konvencije
            stream.Flush();
            // ocisti stream-ove
            stream.Dispose();
            memoryStream.Dispose();

            Console.ReadLine();

            Console.WriteLine("Stopping server...");
        }
    }
}
