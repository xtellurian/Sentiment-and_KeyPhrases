using System.Collections.Generic;
using System.Threading.Tasks;
using SentimentalNews.Model;

namespace SentimentalNews.Services {
        

    public interface ICognitiveServicesTextAnalysis
    {
        Task<IEnumerable<Article>> SetSentiments(IEnumerable<Article> articles);
        Task<IEnumerable<Article>> SetKeyPhrases (IEnumerable<Article> articles);
        
    }   

}