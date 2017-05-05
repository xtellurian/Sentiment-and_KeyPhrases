using System.Collections.Generic;
using Newtonsoft.Json;
using SentimentalNews.CognitiveServices;

namespace SentimentalNews.Model
{
    public class TopicDetectionAggregate : TopicDetectionResponse 
    {
        [JsonProperty("sources")]
        public List<SourceBase> Sources {get;set;}
    }
}