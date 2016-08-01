﻿using System.Collections.Generic;
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
            return new NameValueCollection();
        }
    }
}