using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rian.Cognitive;

namespace Sentiment_And_KeyPhrases.Controllers
{
    public class DataController : Controller, ILogger, IOut
    {
        private List<string> _data;
        private Manager _manager;
        private Task task;
        public DataController()
        {
            _data = new List<string>();

            _manager = new Manager(this, this);
            
            task = _manager.Run();

        }
        public IActionResult Index()
        {
            task.Wait();
            
            var content = String.Join("\n", _data.ToArray());
            return Content(content);
        }

      
        public IActionResult Error()
        {
            return View();
        }

        public void WriteOut(string line)
        {
            System.Diagnostics.Debug.WriteLine(line);
             _data.Add(line);
        }

        public void Log(string line)
        {
            System.Diagnostics.Debug.WriteLine(line);
        }
    }
}
