using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double AverageSentiment {get;set;}
    }
}