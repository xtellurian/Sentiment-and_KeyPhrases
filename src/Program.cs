﻿using System;

namespace Rian.Cognitive 
{
    class Program
    {
        static void Main(string[] args)
        {   

           var manager = new Manager();

           var task = manager.AnalyseArticles2();;
           Console.Read();
          
        }
    }
}
