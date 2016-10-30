using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    enum Version
    {
        v10,
        v11,
        Unsupported
    }

    enum Method
    {
        GET,
        Unsupported
    }

    interface IHTTPParser
    {
        RequestInfo GetRequestInfo(string request);
    }
}
