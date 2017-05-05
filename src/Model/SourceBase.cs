using System.Collections.Generic;
using Newtonsoft.Json;

namespace SentimentalNews.Model {
        
    public class UrlsToLogos
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }

    public class SourceBase
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string country { get; set; }
        public UrlsToLogos urlsToLogos { get; set; }
        public List<string> sortBysAvailable { get; set; }
        
        [JsonProperty("articles")]
        public List<Article> Articles {get; protected set;}
    }

    public class SourceResponse
    {
        public string status { get; set; }
        public List<Source> sources { get; set; }
    }
}