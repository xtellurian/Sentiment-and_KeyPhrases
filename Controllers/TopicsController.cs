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
        private Manager _manager;
        private static ArticleDataAggregate _data;
        private static DateTime _dataBirth;
        private const int DataMaxAgeMinutes = 5;
        public TopicsController()
        {
            _manager = new Manager();
            
        }
        public async Task<IActionResult> Index()
        {
            if(IsRefreshData()){
                await RefreshData();
            }
            
            return View(_data);
        }
        private bool IsRefreshData()
        {
            return _data == null || _dataBirth.AddMinutes(DataMaxAgeMinutes) < DateTime.Now;
        }

        private async Task RefreshData ()
        {
            Debug.WriteLine("Refreshing Data");
            _dataBirth = DateTime.Now;
            _data = await _manager.DownloadLastTopicDetection();
        }

        public async Task<IActionResult> Detail (string id) // id = topicId
        {
            if(IsRefreshData()) await RefreshData();

            // get document ids
            var articles = _data.Articles.Where(a=>a.TopicAssignments.Any(b => b.TopicId == id));
            // get documents and display
            
            // get keyphrase
            var keyPhrase = _data.Topics.FirstOrDefault(d => d.Id == id);

            ViewData["KeyPhrase"] = keyPhrase;
            return View(articles);
        }

      
        public IActionResult Error()
        {
            return View();
        }

    }
}
