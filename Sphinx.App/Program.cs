using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Sphinx.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Console.WriteLine(config["mysql:connection"]);

            //QueueProcess queueProcess = new QueueProcess();
            //queueProcess.Run();

            Console.WriteLine("Application exit.");
        }
    }
}
