using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SentimentalNews.Model;

namespace SentimentalNews.AzureFunctions
{
    public class GetKeyphraseTrend
    {
        private string _function;
        private HttpClient _client;

        public GetKeyphraseTrend(string functionUri, string keyphrase)
        {
            _client = new HttpClient();
            _function = functionUri.Replace("{phrase}",keyphrase);
            
        }

        public async Task<JObject> Run()
        {
            var response = await _client.GetAsync(_function);
            Debug.WriteLine($"GetKeyPhraseTrend response code:: {response.StatusCode}");
            var payload = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(payload);
            return result;
        }
    }
}