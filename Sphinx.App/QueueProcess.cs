using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Sphinx.App
{
    using Sphinx.Base.Common;
    using Sphinx.Core.Builder;
    using Sphinx.Core.DL;
    using Sphinx.Core.Entity;

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
                HostName = AppSettings.RabbitMQHostName,
                Port = AppSettings.RabbitMQPort,
                UserName = AppSettings.RabbitMQUserName,
                Password = AppSettings.RabbitMQPassword
            };

            using (var connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "LogQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 5, false);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += ReceiveHandler;

                    channel.BasicConsume(queue: "LogQueue", autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// 队列接收服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveHandler(object sender, BasicDeliverEventArgs e)
        {
            // get text
            var body = e.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);

            // insert log entity
            var builder = new LogMessageBuilder(json).SetId().SetTime();
            var log = builder.Build();

            Console.WriteLine("message received, tag: {0}, msg:{1}", e.DeliveryTag, log.ToString());

            ((EventingBasicConsumer)sender).Model.BasicAck(e.DeliveryTag, false);
        }
    }
}
