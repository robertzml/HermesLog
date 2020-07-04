using System;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HermesLog
{
    using HermesLog.Model;
    using System.Text.Encodings.Web;

    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "192.168.1.121",
                Port = 5672,
                UserName = "admin",
                Password = "admin"
            };
            
            using(var connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "LogQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var serializeOptions = new JsonSerializerOptions
                        {
                            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        var msg = JsonSerializer.Deserialize<LogMessage>(message, serializeOptions);

                        Console.WriteLine(" [x] Received {0}, Level is: {1}", message, msg.Level);
                    };

                    channel.BasicConsume(queue: "LogQueue", autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }     
    }
}
