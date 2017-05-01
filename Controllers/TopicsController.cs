using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rian.Cognitive;

namespace Sentiment_And_KeyPhrases.Controllers
{
    public class TopicsController : Controller
    {
        private List<string> _data;
        private Manager _manager;

        public TopicsController()
        {
            _data = new List<string>();

            _manager = new Manager();

        }
        public async Task<IActionResult> Index()
        {
            var data = await _manager.DownloadLastTopicDetection();
            
            return View(data);
        }

        public async Task<IActionResult> Detail () 
        {
            return Ok("hello, im detail");
        }

      
        public IActionResult Error()
        {
            return View();
        }

    }
}
