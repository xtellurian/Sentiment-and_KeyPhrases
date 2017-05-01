using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rian.Cognitive {

    public class TopicDetectionService
    {
        private const string serviceEndpoint = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/";
        private HttpClient _httpClient;
        public TopicDetectionService(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
        }
        
        // this is usually handled by an azure function
         public async Task<string> Post(TopicDetectionRequest request)
         {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"{serviceEndpoint}topics", content).ConfigureAwait(false);

            var resultLocation = result.Headers.GetValues("Location").First();
            return resultLocation;
         }

         public async Task<TopicDetectionResponse> GetProcessedDocuments(string location, 
            int retries = 20, int interval = 20) {
                
                var results = await GetResults(location);
                var tries = 0;

                 while( results.Result == null || string.Equals(results.Status, "Running")){
                     if(tries++ > retries) break;
                     await Task.Delay(interval * 1000);
                     results = await GetResults(location);

                 }
            
            return results;
        }

        private async Task<TopicDetectionResponse> GetResults(string location) {
                var topics = await _httpClient.GetAsync(location);
                var content = await topics.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TopicDetectionResponse>(content);
        }
    }

    

}