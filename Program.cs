using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Sentiment_And_KeyPhrases
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any(s => string.Equals(s, "--environment"))){
                Console.WriteLine(@"Usage: dotnet run --environment 'Development'");
                Console.WriteLine("Defaulting to Production Environment");
                var argList = new List<string>(args);
                argList.Add("--environment");
                argList.Add("Production");
                args = argList.ToArray();
            }
            
            var config = new ConfigurationBuilder()   // enabled command line environment spec
            .AddCommandLine(args)
            .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .CaptureStartupErrors(true) // for dev
                .UseSetting("detailedErrors","true") // for dev
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
