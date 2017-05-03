using Newtonsoft.Json;

namespace Rian.Cognitive
{
    public class Topic 
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("score")]
        public double Score { get; set; }
        [JsonProperty("keyPhrase")]
        public string KeyPhrase { get; set; }
        public double AverageSentiment {get;set;}
    }
}