using NUnit.Framework;

namespace HermesLog.Test
{
    using Sphinx.Base.Common;
    using Sphinx.Core.Builder;
    using Sphinx.Core.DL;
    using Sphinx.Core.Entity;
    using System;

    /// <summary>
    /// 日志测试类
    /// </summary>
    public class LogMessageTest
    {
        private LogMessageBusiness logMessageBusiness;

        [SetUp]
        public void Setup()
        {
            AppSettings.MySqlConnection = "Server=47.111.23.211;Database=sphinx-log;User=zml;Password=zml*321;";

            logMessageBusiness = new LogMessageBusiness();
        }

        /// <summary>
        /// 测试添加日志
        /// </summary>
        [Test]
        public void TestInsertMessage()
        {
            LogMessage logMessage = new LogMessage
            {
                Level = 1,
                System = "sphinx-log",
                Module = "test",
                Action = "test insert",
                Message = "insert message"
            };

            var builder = new LogMessageBuilder(logMessage).SetId().SetTime();

            var message = builder.Build();
            Console.WriteLine(message.Timestamp);

            logMessageBusiness.Insert(message);

            Assert.Pass();
        }

        [Test]
        public void TestSerialize()
        {
            LogMessage message = new LogMessage
            {
                Level = 1,
                System = "sphinx-log",
                Module = "test",
                Action = "test insert",
                Message = "insert message",
                Timestamp = DateTime.Now
            };

            Console.WriteLine(logMessageBusiness.Serialize(message));
        }

        [Test]
        public void TestDeserializeLarge()
        {
            string json = "{ \"Level\":4,\"System\":\"Glaucus\",\"Module\":\"main\",\"Action\":\"state\",\"Message\":\"time is 2020-09-26 14:15:24.9015384 +0800 CST m=+60.022818201.\"}";

            ILogMessageBuilder logMessageBuilder = new LogMessageBuilder().SetJson(json).SetId().SetTime();

            var message = logMessageBuilder.Build();
            Console.WriteLine(message.ToString());

            Assert.AreEqual(4, message.Level);
        }

        [Test]
        public void TestDeserializeSmall()
        {
            string json = "{\"level\":5, \"system\":\"sphinx\", \"module\":\"hermes-account\",\"action\":\"find user\",\"message\":\"find user by 152222222\"}";

            ILogMessageBuilder logMessageBuilder = new LogMessageBuilder().SetJson(json).SetId().SetTime();

            var message = logMessageBuilder.Build();
            Console.WriteLine(message.ToString());

            Assert.AreEqual(5, message.Level);
        }
    }
}