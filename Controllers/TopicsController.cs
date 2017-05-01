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
        private TopicDetectionAggregate _data;
        private DateTime _dataBirth;
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

        public async Task<IActionResult> Detail (string id) 
        {
            if(IsRefreshData()) await RefreshData();

            // get document ids
            var docIds = new List<string>();
            foreach(var ass in _data.Result.TopicAssignments){
                if(string.Equals(ass.TopicId, id )){
                    docIds.Add(ass.DocumentId);
                }
            }

            // get documents and display
            var articles = new List<Article>();
            foreach(var source in _data.Sources){
                foreach(var article in source.Articles){
                    Debug.WriteLine(article.Id.ToString());
                    if (docIds.Any(d => string.Equals(d, article.Id.ToString())))
                    {
                        articles.Add(article);
                    }
                }
            }
            // get keyphrase
            var keyPhrase = _data.Result.Topics.FirstOrDefault(d => d.Id == id);

            ViewData["KeyPhrase"] = keyPhrase;
            return View(articles);
        }

      
        public IActionResult Error()
        {
            return View();
        }

    }
}
