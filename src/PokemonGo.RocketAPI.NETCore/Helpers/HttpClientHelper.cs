using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PokemonGo.RocketAPI.Helpers
{
    public static class HttpClientHelper
    {
        public static async Task<TResponse> PostFormEncodedAsync<TResponse>(string url,
            params KeyValuePair<string, string>[] keyValuePairs)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
                AllowAutoRedirect = false
            };

            using (var tempHttpClient = new System.Net.Http.HttpClient(handler))
            {
                var response = await tempHttpClient.PostAsync(url, new FormUrlEncodedContent(keyValuePairs));
                var source = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(source);
            }
        }

        public static NameValueCollection ParseQueryString(string query)
        {
            if (query.IndexOf("?") != -1)
                query = query.Substring(query.IndexOf("?") +1);

            query = WebUtility.UrlDecode(query);
            var collection = new NameValueCollection();

            foreach (var pair in query.Split(','))
            {
                if (pair.IndexOf("=") == -1) continue;
                collection.Add(pair.Split('=')[0], pair.Split('=')[1]);
            }

            return collection;
        }
    }
}