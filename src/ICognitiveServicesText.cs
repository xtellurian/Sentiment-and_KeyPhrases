using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICognitiveServicesTextAnalysis
{
    Task<IEnumerable<Article>> SetSentiments(IEnumerable<Article> articles, TextField field);
    Task<IEnumerable<Article>> SetKeyPhrases (IEnumerable<Article> articles, TextField field);
    
}   

public enum TextField {Title, Description}