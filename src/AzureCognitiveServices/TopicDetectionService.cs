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
        
         public async Task<TopicDetectionResponse> DetectTopics(TopicDetectionRequest request)
         {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"{serviceEndpoint}topics", content).ConfigureAwait(false);

            var resultLocation = result.Headers.GetValues("Location").First();
            
            var results = await GetResponse(resultLocation);
            while(results.Result == null || string.Equals(results.Status, "Running"))
            {
                await Task.Delay(10000); // waits 10 seconds
                results = await GetResponse(resultLocation);
            }
            return results;
         }

         private async Task<TopicDetectionResponse> GetResponse(string location) {

            var topics = await _httpClient.GetAsync(location);
            var content2 = await topics.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<TopicDetectionResponse>(content2);
            return results;
        }
    }

    

}