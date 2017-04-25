using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rian.Cognitive {
    


    public class TopicDetectionRequest
    {
    public static TopicDetectionRequest CreateRequest (IEnumerable<Article> articles)
    {
        var request = new TopicDetectionRequest(){
            Documents = new List<Document>(),
            StopWords = new List<string>(),
            StopPhrases = new List<string>()
        };

        foreach(var article in articles){
            request.Documents.Add(new Document(){
                Id = article.Id.ToString(),
                Text = article.title + "\n" + article.description
            });
        }
        return request;
    }


        [JsonProperty("documents")]
        public List<Document> Documents { get; set; }
        [JsonProperty("stopWords")]
        public List<string> StopWords { get; set; }
        [JsonProperty("stopPhrases")]
        public List<string> StopPhrases { get; set; }

         public class Document
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}