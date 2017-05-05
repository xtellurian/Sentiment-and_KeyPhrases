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
        public ArticlesController()
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
            // get document ids
            var article = data.Articles.FirstOrDefault(a=>a.Id == id);
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
