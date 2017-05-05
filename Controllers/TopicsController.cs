using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SentimentalNews;

namespace Sentiment_And_KeyPhrases.Controllers
{
    public class TopicsController : Controller
    {
        private Manager _manager;
        public TopicsController()
        {
            _manager = Manager.GetInstance();
            
        }
        public async Task<IActionResult> Index()
        {
            var data = await _manager.GetLatest();   
            return View(data);
        }


        public async Task<IActionResult> Detail (string id) // id = topicId
        {
            var data = await _manager.GetLatest();
            // get document ids
            var articles = data.Articles.Where(a=>a.TopicAssignments.Any(b => b.TopicId == id));
            // get documents and display
            
            // get keyphrase
            var topic = data.Topics.FirstOrDefault(d => d.Id == id);

            ViewData["Title"] = topic.KeyPhrase;
            return View(articles);
        }

      
        public IActionResult Error()
        {
            return View();
        }

    }
}
