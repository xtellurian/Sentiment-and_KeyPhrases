using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rian.Cognitive {
        
    public class Article
    {
        public Guid? Id { get;set;}
        
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("urlToImage")]
        public string UrlToImage { get; set; }
        [JsonProperty("publishedAt")]
        public string PublishedAt { get; set; }
        public List<string> TitleKeyPhrases {get; set;}
        public List<string> DescriptionKeyPhrases {get;set;}
        public double TitleSentiment {get; set;}
        public double DescriptionSentiment {get;set;}
        public string Language {get;set;}
    }

    public class ArticleResponse
    {
        public string status { get; set; }
        public string source { get; set; }
        public string sortBy { get; set; }
        public List<Article> articles { get; set; }
    }
}