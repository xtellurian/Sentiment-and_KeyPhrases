


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Rian.AzureFunctions;
using Rian.AzureStorage;

namespace Rian.Cognitive {
        
    public class Manager: ILogger, IOut
    {
        private ILogger _logger;
        private IOut _output;

        public Manager(ILogger logger = null, IOut output = null)
        {
            // set the logger and the output here
            // you can set 'this for both if you want console out

            _logger = logger ?? this;
            _output = output ?? this;
        }

        public void Log(string line)
        {
            Console.WriteLine(line);
        }

        public async Task RunIndependents ()
        {
            if (_logger == null)
            {
                throw new NullReferenceException("Logger not set");
            }
            if (_output == null)
            {
                throw new NullReferenceException("output is null");
            }

            var sourceResponse = await LoadSources();

            await LoadArticles(sourceResponse);

            await AnalyseArticles(sourceResponse);
        }

        private static string TopicDetectionContainer = "outcontainer";
        private static string TopicDetectionBlob = "topic-detection";
        public async Task RunTopicDetection()
        {
             var sourceResponse = await LoadSources();

             await LoadArticles(sourceResponse);
             var articles = new List<Article>();
             foreach(var source in sourceResponse.sources){
                articles.AddRange(source.Articles);
             }

             var service = new TopicDetectionService(Utility.LoadCognitiveServicesTextApiKey());
             var request = TopicDetectionRequest.CreateRequest(articles);
             var location = await service.Post(request);
             _logger.Log(location);

             var azureFunction = Utility.GetPollAndStoreAzureFunction();
             var upload = new PollAndStoreData(location, azureFunction);
             await upload.Run();
            var returned = await DownloadLastTopicDetection();
            Console.WriteLine("Returned: ");
            PrintInfo(returned, 10);
        }

        public async Task<TopicDetectionResponse> DownloadLastTopicDetection()
        {
            var storage = new DownloadFromBlobStorage(Utility.LoadStorageConnectionString());
            var response = await storage.DownloadFileAs<TopicDetectionResponse>(TopicDetectionBlob, TopicDetectionContainer);
            return response;
        }

        private void PrintInfo(TopicDetectionResponse response, int count){
            response.Result.Topics.Sort( (r1, r2) => r2.Score.CompareTo(r1.Score));
            
            for(int i = 0 ; i < count; i++){
                _output.WriteOut($"{response.Result.Topics[i].KeyPhrase} has score {response.Result.Topics[i].Score}");
            }
        }

        private async Task AnalyseArticles(SourceResponse sourceResponse)
        {
            var key = Utility.LoadCognitiveServicesTextApiKey();
            ICognitiveServicesTextAnalysis textAnalysis =
            new CognitiveServicesTextAnalysis(key);

            var analyser = new ArticleAnalyser(textAnalysis, _logger, _output);

            await analyser.AnalyseAll(sourceResponse.sources);
        }

        private async Task LoadArticles(SourceResponse sourceResponse)
        {
            var tasks = new List<Task>();
            foreach (var source in sourceResponse.sources)
            {
                tasks.Add(source.LoadArticles());
            }

            await Task.WhenAll(tasks);
        }

        public void WriteOut(string line)
        {
            Console.WriteLine(line);
        }

        private async Task<SourceResponse> LoadSources() 
        {
            Source.SetApiKey(Utility.LoadNewsApiKey());

            var sourceResponse = await Source.GetSourcesAsync(_logger, Language.en);
            
            _logger.Log($"Found {sourceResponse.sources.Count} Sources");

            return sourceResponse;
        }
    }
}