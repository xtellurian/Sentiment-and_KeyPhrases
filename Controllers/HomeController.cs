using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rian.Cognitive;

namespace Sentiment_And_KeyPhrases.Controllers
{
    public class HomeController : Controller
    {
        private Task _startDownloading;
        private Manager _manager;
        public HomeController()
        {
             _manager = new Manager();
             _startDownloading = _manager.GetLatest();
             
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
