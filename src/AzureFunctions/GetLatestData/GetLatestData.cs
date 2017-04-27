using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rian.Cognitive;

namespace Rian.AzureFunctions
{
    public class GetLatestData
    {
        private HttpClient _client;
        private string _function;
        public GetLatestData(string function)
        {
            _client = new HttpClient();
            _function = function;
        }

        public async Task<TopicDetectionResponse> Run()
        {
            var res = await _client.GetAsync(_function);
            var text = await res.Content.ReadAsStringAsync();
            
            try
            {
                var other1 = JsonConvert.DeserializeObject<string>(text);
                var other = JsonConvert.DeserializeObject<TopicDetectionResponse>(other1);
                // weird - its getting serialised twice somehow...
                return other;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(text);
            }
            return null;
            
        }
    }
}