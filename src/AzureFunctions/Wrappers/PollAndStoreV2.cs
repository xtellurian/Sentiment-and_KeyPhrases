using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SentimentalNews.Model;

namespace SentimentalNews.AzureFunctions
{
    public class PollAndStoreV2
    {
        private string _function;
        private HttpClient _client;
        private SourceResponse _sources;
        public PollAndStoreV2(string location, string function, SourceResponse sources)
        {
            _function = function;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("x-location", location);
            _sources = sources;
        }

        public async Task Run()
        {
            var serialisedData = JsonConvert.SerializeObject(_sources);
            var stringContent = new StringContent(serialisedData);
            var response = await _client.PostAsync(_function, stringContent);
            Debug.WriteLine($"Poll And Store Response: {response.StatusCode}");
        }
    }
}