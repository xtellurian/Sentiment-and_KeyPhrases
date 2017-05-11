using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SentimentalNews;
using SentimentalNews.AzureFunctions;
using SentimentalNews.Web;

namespace Sentiment_And_KeyPhrases.Controllers
{
    public class DataController : Controller
    {
        private List<string> _data;
        private Manager _manager;
        public DataController()
        {
            _data = new List<string>();

            _manager = Manager.GetInstance();

        }

        public async Task<IActionResult> TrendingTopics()
        {
            var data = await _manager.GetLatest();
            var topics = data.Topics;
            return Ok(new ResultClass(){Data = topics});
        }

        public async Task<IActionResult> Source (string id)
        {
            var uri = ConfigurationWrapper.Config["ArticlesFromSourceUri"];
            var days = Request.Query["days"];
            int numDays = 1;
            int.TryParse(days, NumberStyles.Integer , null, out numDays);
            var function = new GetArticlesFromSource(uri, id, numDays);
            var result = await function.Run();
            return Ok(result);
        }

        public async Task<IActionResult> AllSources ()
        {
            var uri = ConfigurationWrapper.Config["GetSourcesUri"];
            var function = new GetSources(uri);
            var data = await function.Run();
            return Ok(data);
        }



        public async Task<IActionResult> Refresh()
        {
            var data = await _manager.GetLatest(false);
            return Ok(data);
        }
        public async Task<IActionResult> Index()
        {
            var data = await _manager.GetLatest();
            
            return Ok(data);
        }

        public async Task<IActionResult> Renew() 
        {
            await _manager.RunTopicDetectionAsync();
            Debug.WriteLine("Renewing Data");
            return Ok();
        }

      
        public IActionResult Error()
        {
            return View();
        }

        public class ResultClass 
        {
            [JsonPropertyAttribute("data")]
            public dynamic Data {get;set;}
        }

    }
}
