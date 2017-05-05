using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SentimentalNews.Model;

namespace SentimentalNews.CognitiveServices
{

    public class TopicAssignment
    {
        [JsonProperty("topicId")]
        public string TopicId { get; set; }
        [JsonProperty("documentId")]
        public string DocumentId { get; set; }
        [JsonProperty("distance")]
        public float Distance { get; set; }
        [JsonProperty("topicKeyPhrase")]
        public string TopicKeyPhrase {get;set;}
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
        [JsonProperty("createdDateTime")]
        public DateTime? CreatedDateTime {get;set;}
        [JsonProperty("dataLocation")]
        public string DataLocation {get;set;}
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("operationProcessingResult")]
        public OperationResult Result { get; set; }
    }

}