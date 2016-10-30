using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HelloClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8080;
            string stringIp = "127.0.0.1";

            IPAddress ip = IPAddress.Parse(stringIp);

            // instanciramo klijenta
            TcpClient client = new TcpClient();

            // inicijaliziramo konekciju
            client.Connect(ip, port);

            // dohvati stream
            NetworkStream stream = client.GetStream();
            // Stream koji zapisuje u memoriju racunala
            MemoryStream memoryStream = new MemoryStream();
            StreamReader reader = new StreamReader(memoryStream);

            // buffer za odlazne poruke
            byte[] inBuffer = new byte[4096];

            // P2
            string msg = Console.ReadLine();

            // Pretvorimo string u niz byte-ova
            byte[] data = Encoding.UTF8.GetBytes(msg);

            // parametri objasnjeni redom: 
            // - buffer iz kojeg citamo podatke - u ovom slucaju niz byteova "data"
            // - offset - od koje pozicije pocinjemo s citanjem podataka - 0
            // - size - koliko podataka zelimo zapisati - data.length
            stream.Write(data, 0, data.Length);
            // Obicno kada radimo sa streamovima, potrebno je pozvati Flush() metodu kako bi se podatci zapisali.
            // To nije slucaj sa NetworkStreamom kako pise na stranici microsofta:
            // "The Flush method implements the Stream.Flush method; however, because NetworkStream is not buffered, 
            // it has no affect on network streams. Calling the Flush method does not throw an exception."
            // Pozivamo ovu metodu cisto zbog konvencije
            stream.Flush();

            int bytesRead = stream.Read(inBuffer, 0, inBuffer.Length);

            memoryStream.Write(inBuffer, 0, bytesRead);

            memoryStream.Seek(0, SeekOrigin.Begin);
            
            string recv = reader.ReadLine();

            Console.WriteLine(recv);

            // zatvorimo streamove
            memoryStream.Dispose();
            stream.Dispose();

            Console.ReadLine();
        }
    }
}
