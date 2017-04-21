using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET_Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {   

           var manager = new Manager();
           var task = manager.Run();
           Console.Read();
          
        }
    }
}
