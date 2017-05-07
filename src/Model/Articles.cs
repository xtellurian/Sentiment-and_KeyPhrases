using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SentimentalNews.CognitiveServices;

namespace SentimentalNews.Model 
{
        
    public class Article
    {
        public string Id { get;set;}
        
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
        public DateTime? PublishedAt { get; set; }
        public List<string> KeyPhrases {get;set;} // currently not using this

        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double Sentiment {get;set;}
        public string Language {get;set;}
        [Display(Name = "Associated Topics")]
        public List<TopicAssignment> TopicAssignments {get;set;}
        public AnalyseImageResponse ImageAnalysis {get;private set;} // this is set by a different call, not in initial req

        public void SetImageAnalysis(AnalyseImageResponse response)
        {
            ImageAnalysis = response;
        }
    }

    public class ArticleResponse
    {
        public string status { get; set; }
        public string source { get; set; }
        public string sortBy { get; set; }
        public List<Article> articles { get; set; }
    }
}