using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rian.AzureFunctions
{
    public class PollAndStore
    {
        private string _function;
        private HttpClient _client;
        public PollAndStore(string location, string function)
        {
            _function = function;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("x-location", location);
        }

        public async Task Run()
        {
            var response = await _client.GetAsync(_function);
            Debug.WriteLine($"Poll And Store Response: {response.StatusCode}");
        }
    }
}