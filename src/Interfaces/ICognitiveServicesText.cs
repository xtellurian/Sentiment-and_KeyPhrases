using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rian.Cognitive {
        

    public interface ICognitiveServicesTextAnalysis
    {
        Task<IEnumerable<Article>> SetSentiments(IEnumerable<Article> articles);
        Task<IEnumerable<Article>> SetKeyPhrases (IEnumerable<Article> articles);
        
    }   

}