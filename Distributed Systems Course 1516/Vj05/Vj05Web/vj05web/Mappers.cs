using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vj05Web
{
    class Mappers
    {
        public List<string> QuerySearchAndGetNameList(string rezultatHttpPretrage)
        {
            JArray jsonArray = JArray.Parse(rezultatHttpPretrage);

            JToken imenaRezultata = jsonArray.Skip(1).First();

            return imenaRezultata.Select(x => x.ToString()).ToList();
        }

        public WebPage WebPageQuery(string rezultatHttpUpitaZaStranicom)
        {
            JObject jsonObject = JObject.Parse(rezultatHttpUpitaZaStranicom);

            JObject parse = jsonObject["parse"].ToObject<JObject>();

            string title = parse["title"].ToObject<string>();
            int id = parse["pageid"].ToObject<int>();
            JObject textObject = parse["text"].ToObject<JObject>();
            string text = textObject["*"].ToObject<string>();

            return new WebPage(id, title, text);
        }
    }
}
