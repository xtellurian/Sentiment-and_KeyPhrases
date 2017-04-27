using System;

namespace Rian.Cognitive 
{
    class Program
    {
        static void Main(string[] args)
        {   

           var manager = new Manager();

           var task = manager.RunTopicDetection();;
           Console.Read();
          
        }
    }
}
