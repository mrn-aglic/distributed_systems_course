using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class RequestInfo
    {
        public Tuple<Method, string, Version> RequestLine { get; private set; }
        public List<Tuple<string, string>> Headers { get; private set; }
        public string MessageBody { get; private set; }

        public RequestInfo(Tuple<Method, string, Version> requestLine, List<Tuple<string, string>> headers, string mbody)
        {
            RequestLine = requestLine;
            Headers = headers;
            MessageBody = mbody;
        }

        public override string ToString()
        {
            string requestLine = "( " + RequestLine.Item1 + ", " + RequestLine.Item2 + ", " + RequestLine.Item3 + " )\n";
            string headers = Headers.Select(x => x.Item1 + " : " + x.Item2 + "\n").Aggregate((x, y) => x + y);

            return requestLine + headers + "\n" + MessageBody + "\n";
        }
    }
}
