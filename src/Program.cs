﻿using System;

namespace NET_Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {   

           var manager = new Manager();
           Manager.Output = line => Console.WriteLine(line);
           var task = manager.Run();
           Console.Read();
          
        }
    }
}
