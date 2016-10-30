using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vj05Web
{
    class RemoteQueries
    {
        static Mappers mappers = new Mappers();

        static List<string> AllSearchResults = new List<string>();
        static List<WebPage> AllPages = new List<WebPage>();

        const string searchUrl = "https://en.wikipedia.org/w/api.php?action=opensearch&format=json&limit=15&search=";
        const string pageUrl = "https://en.wikipedia.org/w/api.php?action=parse&format=json&prop=text&page=";

        public async Task<List<string>> GetWikipediaSearchResults(string izraz)
        {
            using (var httpClient = new HttpClient())
            {
                string httpResponse = await httpClient.GetStringAsync(searchUrl + izraz);

                List<string> results = mappers.QuerySearchAndGetNameList(httpResponse);

                return results;
            }
        }

        public async Task<WebPage> GetWebPage(string expression)
        {
            using (var httpClient = new HttpClient())
            {
                string httpResponse = await httpClient.GetStringAsync(pageUrl + expression);

                WebPage page = mappers.WebPageQuery(httpResponse);

                return page;
            }
        }
    }
}
