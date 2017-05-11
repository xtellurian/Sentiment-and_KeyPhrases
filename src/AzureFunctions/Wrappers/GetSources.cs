using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SentimentalNews.Model;

namespace SentimentalNews.AzureFunctions
{
    public class GetSources
    {
        private string _function;
        private HttpClient _client;

        public GetSources(string functionUri)
        {

            _client = new HttpClient();
            _function = functionUri;
        }

        public async Task<JObject> Run()
        {
            var response = await _client.GetAsync(_function);
            Debug.WriteLine($"GetSources response code:: {response.StatusCode}");
            var payload = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(payload);
            return result;
        }
    }
}