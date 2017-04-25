using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rian.Cognitive 
{
    public class Topic
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("score")]
        public string Score { get; set; }
        [JsonProperty("keyPhrase")]
        public string KeyPhrase { get; set; }
    }

    public class TopicAssignment
    {
        [JsonProperty("topicId")]
        public string TopicId { get; set; }
        [JsonProperty("documentId")]
        public string DocumentId { get; set; }
        [JsonProperty("distance")]
        public string Distance { get; set; }
    }

    public class OperationResult
    {
        [JsonProperty("topics")]
        public List<Topic> Topics { get; set; }

        [JsonProperty("topicAssignments")]
        public List<TopicAssignment> TopicAssignments { get; set; }
    }

    public class TopicDetectionResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("operationProcessingResult")]
        public OperationResult Result { get; set; }
    }

}