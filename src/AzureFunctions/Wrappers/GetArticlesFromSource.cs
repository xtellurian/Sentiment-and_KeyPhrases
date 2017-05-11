using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SentimentalNews.Model;

namespace SentimentalNews.AzureFunctions
{
    public class GetArticlesFromSource
    {
        private string _function;
        private HttpClient _client;

        public GetArticlesFromSource(string functionUri, string sourceId, int numDays = 1)
        {
            if(numDays < 1) numDays = 1;
            _client = new HttpClient();
            sourceId = sourceId.Replace("-",""); // strip - chars for table names, maybe not neccessary
            _function = functionUri.Replace("{sourceId}",sourceId).Replace("{days}",numDays.ToString());
            
        }

        public async Task<JObject> Run()
        {
            var response = await _client.GetAsync(_function);
            Debug.WriteLine($"GetArticlesFromSource response code:: {response.StatusCode}");
            var payload = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(payload);
            return result;
        }
    }
}