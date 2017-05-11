using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<IActionResult> Source (string id)
        {
            var uri = ConfigurationWrapper.Config["ArticlesFromSourceUri"];
            var function = new GetArticlesFromSource(uri, id);
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

    }
}
