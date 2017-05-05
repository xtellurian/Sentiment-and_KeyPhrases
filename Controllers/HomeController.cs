﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rian.Cognitive;

namespace Sentiment_And_KeyPhrases.Controllers
{
    public class HomeController : Controller
    {
       private Manager _manager;
        public HomeController()
        {
             _manager = Manager.GetInstance();
             
        }
        public IActionResult Index()
        {
            var data = _manager.GetLatest();
            return View(data);
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
