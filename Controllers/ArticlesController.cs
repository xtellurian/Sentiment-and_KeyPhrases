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
    public class ArticlesController : Controller
    {
        private Manager _manager;
        private static ArticleDataAggregate _data;
        private static DateTime _dataBirth;
        private const int DataMaxAgeMinutes = 20;
        public ArticlesController()
        {
            _manager = new Manager();
            
        }
        public async Task<IActionResult> Index()
        {
            if(IsRefreshData()){
                await RefreshData();
            }
            
            return View();
        }
        private bool IsRefreshData()
        {
            return _data == null || _dataBirth.AddMinutes(DataMaxAgeMinutes) < DateTime.Now;
        }

        private async Task RefreshData ()
        {
            Debug.WriteLine("Refreshing Data");
            _dataBirth = DateTime.Now;
            // _data = await _manager.DownloadLastTopicDetection();
            _data = await _manager.GetLatest();
        }

        public async Task<IActionResult> Detail (string id) // id = articleId
        {
            if(IsRefreshData()) await RefreshData();

            // get document ids
            var article = _data.Articles.FirstOrDefault(a=>a.Id == id);
            // get documents and display
            if(article==null){
                throw new Exception($"{id} not found");
            }

            ViewData["title"] = article.Title;
            return View(article);
        }

      
        public IActionResult Error()
        {
            return View();
        }

    }
}
