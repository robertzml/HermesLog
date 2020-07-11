using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using HermesLog.Model;
using HermesLog.DL;

namespace HermesLog
{
    /// <summary>
    /// 队列处理类
    /// </summary>
    public class QueueProcess
    {
        /// <summary>
        /// 启动队列监听
        /// </summary>
        public void Run()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "192.168.1.121",
                Port = 5672,
                UserName = "admin",
                Password = "admin"
            };

            LogMessageBusiness logMessageBusiness = new LogMessageBusiness();

            using (var connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "LogQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        // get text
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        // insert log entity
                        var log = logMessageBusiness.Deserialize(message);
                        logMessageBusiness.SetTime(log);
                        logMessageBusiness.Insert(log);

                        Console.WriteLine(log.ToString());
                    };
                    
                    channel.BasicConsume(queue: "LogQueue", autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
