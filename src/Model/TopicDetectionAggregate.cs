using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rian.Cognitive
{
    public class TopicDetectionAggregate : TopicDetectionResponse 
    {
        [JsonProperty("sources")]
        public List<SourceBase> Sources {get;set;}
    }
}