


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Manager
{
    // forces user to make manager before can get output
    public static void WriteLine(string line)
    {
        Output.Invoke(line);
    }
    public static Action<string> Output {get;set;}

    public async Task Run () 
    {
         Source.SetApiKey(Utility.LoadNewsApiKey());
            var sourceResponse = Source.GetSourcesAsync(Language.en).Result;
           
            Manager.WriteLine($"Found {sourceResponse.sources.Count} Sources");
            var tasks = new List<Task>();
            foreach(var source in sourceResponse.sources){
                tasks.Add(source.LoadArticles());
            }
            
            await Task.WhenAll(tasks);

            

            var key = Utility.LoadCognitiveServicesTextApiKey();
           ICognitiveServicesTextAnalysis textAnalysis = new CognitiveServicesTextAnalysis(key);
           

            var analyser = new ArticleAnalyser(textAnalysis);
            
             analyser.AnalyseAll(sourceResponse.sources).Wait();
    }

}