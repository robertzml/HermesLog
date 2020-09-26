﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Sphinx.App
{
    using Sphinx.Base.Common;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Start.");

            LoadSettings();

            QueueProcess queueProcess = new QueueProcess();
            queueProcess.Run();

            Console.WriteLine("Application exit.");
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        static void LoadSettings()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            AppSettings.MySqlConnection = config["mysql:connection"];
            AppSettings.RabbitMQHostName = config["rabbitmq:hostName"];
            AppSettings.RabbitMQPort = Convert.ToInt32(config["rabbitmq:port"]);
            AppSettings.RabbitMQUserName = config["rabbitmq:userName"];
            AppSettings.RabbitMQPassword = config["rabbitmq:password"];
        }
    }
}