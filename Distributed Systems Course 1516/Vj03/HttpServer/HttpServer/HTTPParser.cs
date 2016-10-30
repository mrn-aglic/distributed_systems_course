using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class HTTPParser : IHTTPParser
    {
        public RequestInfo GetRequestInfo(string request)
        {
            string[] lines = SplitRequest(request);

            Tuple<Method, string, Version> requestLineParsed = ParseRequestLine(lines[0]);

            string[] headerLines = lines.Skip(1).TakeWhile(x => x.Trim() != "").ToArray();
            List<Tuple<string, string>> headers = ParseHeaders(headerLines);

            // lines -> linije koje smo dobili na pocetku
            // lines.SkipWhile -> preskaci elemente dok je uvjet zadovoljen - dok linija nije prazna
            // lines.SkipWhile(...).Skip(1) -> preskoci praznu liniju
            // lines.SkipWhile(...).Skip(1).Aggregate((x, y) => x + " " + y) -> spoji elemente koji su ostali u jedan element
            // string messageBody = lines.SkipWhile(x => x.Trim() != "").Skip(1).Aggregate((x, y) => x + " " + y);

            string[] messageLines = lines.SkipWhile(x => x.Trim() != "").Skip(1).ToArray();
            string messageBody = ParseMessageBody(messageLines);

            return new RequestInfo(requestLineParsed, headers, messageBody);
        }

        private string ParseMessageBody(string[] messageBody)
        {
            return messageBody.Aggregate((x, y) => x + " " + y);
        }

        private List<Tuple<string, string>> ParseHeaders(string[] lines)
        {
            List<Tuple<string, string>> headers = lines.Select(x => ToTuple(x.Split(':'))).ToList();

            return headers;
        }

        private Tuple<string, string> ToTuple(string[] header)
        {
            // Pretpostavimo da nece biti pogresaka i da cemo primiti uvijek header sa 2 elementa
            return Tuple.Create(header.First(), header.Last());
        }

        private string[] SplitRequest(string request)
        {
            string[] lines = request.Split('\n').Select(x => x.Trim()).ToArray();
            
            return lines;
        }

        private Tuple<Method, string, Version> ParseRequestLine(string requestLine)
        {
            string[] tokens = requestLine.Split(' ');

            Method requestMethod = tokens[0].ToUpper() == "GET" ? Method.GET : Method.Unsupported;

            string url = tokens[1];
            Version version = tokens[2] == "HTTP/1.1" ? Version.v11 : tokens[2] == "HTTP/1.0" ? Version.v10 : Version.Unsupported;

            return Tuple.Create(requestMethod, url, version);
        }
    }
}
