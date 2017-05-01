using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Rian.AzureFunctions;

namespace Rian.Cognitive {
        
    public class Manager: ILogger, IOut
    {
        private ILogger _logger;
        private IOut _output;

        public Manager()
        {
            _logger =  this;
            _output =  this;
        }
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


        public async Task RunTopicDetectionAsync()
        {
             var sourceResponse = await LoadSources();

             await LoadArticles(sourceResponse);
             var articles = new List<Article>();
             foreach(var source in sourceResponse.sources){
                articles.AddRange(source.Articles);
             }

             
             var key = ConfigurationWrapper.Config["CognitiveServicesTextApiKey"];
             var service = new TopicDetectionService(key);
             var request = TopicDetectionRequest.CreateRequest(articles);
             var location = await service.Post(request);
             _logger.Log(location);
             // v1
            //  var azureFunction = Utility.GetPollAndStoreAzureFunction();
             // var upload = new PollAndStore(location, azureFunction);
             // await upload.Run();

             // v2
             var functionLocation = ConfigurationWrapper.Config["PollAndStoreV2FunctionUri"];
             var upload = new PollAndStoreV2(location, functionLocation, sourceResponse);
             await upload.Run();

        }

        public void RunTopicDetectionSynchronous()
        {
            this.RunTopicDetectionAsync().Wait();
        }
        

        public async Task<ArticleDataAggregate> DownloadLastTopicDetection()
        {
            var functionLocation = ConfigurationWrapper.Config["LatestDataUri"];
            var latest = new GetLatestData(functionLocation);
            var response = await latest.Run();
            var convertedResponse = latest.Convert(response);
            return convertedResponse;;
        }



        private async Task AnalyseArticles(SourceResponse sourceResponse)
        {
            var key = ConfigurationWrapper.Config["CognitiveServicesTextApiKey"];
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
            var key = ConfigurationWrapper.Config["NewsApiKey"];
            Source.SetApiKey(key);

            var sourceResponse = await Source.GetSourcesAsync(_logger, Language.en);
            
            _logger.Log($"Found {sourceResponse.sources.Count} Sources");

            return sourceResponse;
        }
    }
}