using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SentimentalNews.Model;

namespace SentimentalNews.AzureFunctions
{
    public class GetImageAnalysis
    {
        private string _function;
        private HttpClient _client;

        public GetImageAnalysis(string functionUri, string articleId)
        {

            _client = new HttpClient();
            _function = functionUri.Replace("{articleId}",articleId);
        }

        public async Task<AnalyseImageResponse> Run()
        {
            var response = await _client.GetAsync(_function);
            Debug.WriteLine($"GetImageAnalysis response code:: {response.StatusCode}");
            var payload = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AnalyseImageResponse>(payload);
            return result;
        }
    }
}