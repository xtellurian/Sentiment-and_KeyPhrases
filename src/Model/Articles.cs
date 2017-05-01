using System;
using System.Collections.Generic;

namespace Rian.Cognitive {
        
    public class Article
    {
        public Guid? Id { get;set;}
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public string publishedAt { get; set; }
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