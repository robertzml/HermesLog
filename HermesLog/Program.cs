using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HermesLog
{
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

                        Console.WriteLine(" [x] Received {0}", message);
                    };

                    channel.BasicConsume(queue: "LogQueue", autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }     
    }
}
