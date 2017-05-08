using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SentimentalNews.AzureFunctions;
using SentimentalNews.CognitiveServices;
using SentimentalNews.Model;
using SentimentalNews.Web;

namespace SentimentalNews {
        
    public class Manager
    {
        private static Manager _instance;
        public static Manager GetInstance ()
        {
            if(_instance==null) _instance = new Manager();
            return _instance;
        }
        protected Manager()
        {
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

             var functionLocation = ConfigurationWrapper.Config["PollAndStoreV2FunctionUri"];
             var upload = new PollAndStoreV2(location, functionLocation, sourceResponse);
             await upload.Run();

        }
        
        
        private ArticleDataAggregate _cachedData;
        public async Task<ArticleDataAggregate> GetLatest(bool fromCache = true)
        {
            if(fromCache == false || _cachedData == null)
            {
                _cachedData = await GetLatestFromRemote();
            }
            return _cachedData;
        }

        public async Task<AnalyseImageResponse> GetImageAnalysis(string articleId)
        {
            var uri = ConfigurationWrapper.Config["ImageAnalysisUri"];
            var request = new GetImageAnalysis(uri, articleId);
            return await request.Run();
        }

        private async Task<ArticleDataAggregate> GetLatestFromRemote()
        {
            var functionLocation = ConfigurationWrapper.Config["LatestDataV2Uri"];
            var latest = new GetLatestDataV2(functionLocation);
            var response = await latest.Run();
            var averagedResponse = latest.AverageSentimentsOverTopcs(response);
            return averagedResponse;
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

        private async Task<SourceResponse> LoadSources() 
        {
            var key = ConfigurationWrapper.Config["NewsApiKey"];
            Source.SetApiKey(key);

            var sourceResponse = await Source.GetSourcesAsync( "en");
            

            return sourceResponse;
        }
    }
}