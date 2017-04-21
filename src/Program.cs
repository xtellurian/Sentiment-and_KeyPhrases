using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET_Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {   
            Source.SetApiKey(Utility.LoadNewsApiKey());
            var sourceResponse = Source.GetSourcesAsync(Language.en).Result;
           
            Console.WriteLine($"Found {sourceResponse.sources.Count} Sources");
            var tasks = new List<Task>();
            foreach(var source in sourceResponse.sources){
                tasks.Add(source.LoadArticles());
            }
            
            Task.WhenAll(tasks).Wait();

            

            var key = Utility.LoadCognitiveServicesTextApiKey();
           ICognitiveServicesTextAnalysis textAnalysis = new CognitiveServicesTextAnalysis(key);
           

            var analyser = new ArticleAnalyser(textAnalysis);
            
             analyser.AnalyseAll(sourceResponse.sources).Wait();
          
        }
    }
}
