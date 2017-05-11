using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SentimentalNews;
using SentimentalNews.AzureFunctions;
using SentimentalNews.Web;

namespace Sentiment_And_KeyPhrases.Controllers
{
    public class SourcesController : Controller
    {
        private Manager _manager;
        public SourcesController()
        {
            _manager = Manager.GetInstance();
            
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Detail (string id) // id = articleId
        {

            var data = await _manager.GetLatest();


            ViewData["title"] = data.Sources.FirstOrDefault(s=>s.id == id).name;
            ViewData["SourceId"] = id;
            return View();
        }

      
        public IActionResult Error()
        {
            return View();
        }

    }
}
