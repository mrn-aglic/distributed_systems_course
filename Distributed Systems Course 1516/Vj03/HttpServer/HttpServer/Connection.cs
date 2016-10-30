using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpServer
{
    class Connection
    {
        public int Id { get; private set; }

        private TcpClient _tcpClient;
        private StreamReader _reader;
        private NetworkStream _stream;
        private IHTTPParser _httpParser;

        public Connection(int id, TcpClient tcpClient, IHTTPParser httpParser)
        {
            Id = id;

            _httpParser = httpParser;
            _tcpClient = tcpClient;
        }

        public async void Do(CancellationToken ct)
        {
            // ulazni buffer
            byte[] inBuffer = new byte[4096];

            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    StreamReader reader = new StreamReader(memStream);

                    _stream = _tcpClient.GetStream();

                    int readBytes = await _stream.ReadAsync(inBuffer, 0, inBuffer.Length);

                    await memStream.WriteAsync(inBuffer, 0, readBytes);
                    await memStream.FlushAsync();

                    memStream.Seek(0, SeekOrigin.Begin);

                    string received = "";
                    string receivedBuffer = "";

                    do
                    {
                        received = await reader.ReadLineAsync();

                        receivedBuffer = receivedBuffer + received + "\n";
                    } while (received != "");

                    RequestInfo requestInfo = _httpParser.GetRequestInfo(receivedBuffer);
                    
                    string header = "HTTP/1.1 {0}\r\n"
                                    + "Server: PMFST\r\n"
                                    + "Content-Length: {1}\r\n"
                                    + "Content-Type: {2}\r\n"
                                    + "Keep-Alive: Close\r\n"
                                    + "\r\n";

                    string folder = "Files/";
                    string fileName = requestInfo.RequestLine.Item2.TrimStart('/');

                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        fileName = "index.html";
                    }

                    fileName = folder + fileName;

                    string responseCode = "200 OK";
                    string mimeType = "";

                    byte[] outBuffer;

                    try
                    {
                        if (File.Exists(fileName))
                        {
                            outBuffer = File.ReadAllBytes(fileName);
                            string extension = Path.GetExtension(fileName);
                            mimeType = GetMimeType(extension);
                        }
                        else
                        {
                            outBuffer = File.ReadAllBytes(folder + "NotFound.html");
                            string extension = ".html";
                            mimeType = GetMimeType(extension);
                            responseCode = "404 Not found";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        string extension = ".html";
                        mimeType = GetMimeType(extension);

                        outBuffer = File.ReadAllBytes(folder + "InternalServerError.html");
                        responseCode = "500 Internal server error";
                    }

                    string formatedHeader = string.Format(header, responseCode, outBuffer.Length, mimeType);

                    byte[] headerBytes = Encoding.UTF8.GetBytes(formatedHeader);

                    await _stream.WriteAsync(headerBytes, 0, headerBytes.Length);
                    await _stream.WriteAsync(outBuffer, 0, outBuffer.Length);
                    await _stream.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured");
                Console.WriteLine(ex.Message);
            }

            _stream.Close();
            _stream.Dispose();
            _tcpClient.Close();
        }

        private string GetMimeType(string extension)
        {
            if (extension == ".html")
            {
                return "text/html";
            }
            else if (extension == ".jpeg" || extension == ".jpg" || extension == ".png")
            {
                return "image/jpeg";
            }
            else
            {
                return "application/octet-stream";
            }
        }
    }
}
