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
    public class DataController : Controller
    {
        private List<string> _data;
        private Manager _manager;
        public DataController()
        {
            _data = new List<string>();

            _manager = new Manager();

        }
        public async Task<IActionResult> Index()
        {
            var data = await _manager.GetLatest();
            
            return Ok(JsonConvert.SerializeObject(data));
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
