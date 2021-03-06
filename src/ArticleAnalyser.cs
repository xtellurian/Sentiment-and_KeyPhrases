using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SentimentalNews.Services;

namespace SentimentalNews.Model {


    public class ArticleAnalyser 
    {
        private ICognitiveServicesTextAnalysis _congitiveService;

        public ArticleAnalyser(ICognitiveServicesTextAnalysis cognitiveService)
        {
            _congitiveService = cognitiveService;
        }

        public async Task<IEnumerable<Article>> AnalyseArticles(List<Article> articles)
        {

            for (int i=0; i * 100 < articles.Count(); i++){
                var count = Math.Min(100, articles.Count() - i * 100);
                await AnalyseArticlesBy100(articles.GetRange(i*100,count));
            }

            return articles;
        }

        public async Task AnalyseArticlesBy100(IEnumerable<Article> articles)
        {
            // needs max 100 articles
            if(articles.Count() > 100 ) throw new ArgumentException("More than 100 articles");

            
            var art1 = await _congitiveService.SetSentiments(articles);
        
        }

        // public async Task AnalyseAll (IEnumerable<Source> sources)
        // {
        //     var allArticles = new List<Article>();
        //     foreach(var source in sources){
        //         allArticles.AddRange(source.Articles);
        //     }
        //     for (int i=0; i * 100 < allArticles.Count; i++){
        //         var count = Math.Min(100, allArticles.Count - i * 100);
        //         var articles = await AnalyseArticlesBy100(allArticles.GetRange(i*100,count));
        //     }

        //     var Aggregates = new List<KeyPhraseSentimentAggregate>();
            
        //     foreach(var article in allArticles)
        //     {
        //         if(article.KeyPhrases==null) continue;
        //         foreach(var phrase in article.DescriptionKeyPhrases)
        //         {
        //             if(string.IsNullOrWhiteSpace(phrase) || WordCount(phrase) <= 1) continue;
        //             if(Aggregates.Any(c=> string.Equals(c.Phrase, phrase))){
        //                 var item = Aggregates.FirstOrDefault(c=> string.Equals(c.Phrase, phrase));
        //                 item.TotalSentiment+= article.Sentiment;
        //                 item.Count++;
        //             }
        //             else{
        //                 Aggregates.Add(new KeyPhraseSentimentAggregate(phrase, article.Sentiment));
        //             }
                    
        //         }
        //     }

        //     Aggregates.Sort((pair1,pair2) => pair2.Phrase.Length.CompareTo(pair1.Phrase.Length));
        //     Aggregates.Sort((pair1,pair2) => pair2.Count.CompareTo(pair1.Count));


        // }

        private int WordCount(string input){
            var text = input.Trim();
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {
            // check if current char is part of a word
            while (index < text.Length && !char.IsWhiteSpace(text[index]))
                index++;

            wordCount++;

            // skip whitespace until next word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;
            }
            return wordCount;
        }
    }

    public class KeyPhraseSentimentAggregate 
    {
        public KeyPhraseSentimentAggregate(string phrase, double sentiment, int count = 1)
        {
            Phrase = phrase;
            TotalSentiment = sentiment;
            Count = count;
        }
        public string Phrase;
        public int Count;
        public double TotalSentiment;

        public override string ToString()
        {
            return $"{Count} x {Phrase} -- {(TotalSentiment*100/Count).ToString("##")}%";
        }
    }

}