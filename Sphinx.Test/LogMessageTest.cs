using NUnit.Framework;

namespace HermesLog.Test
{
    using Sphinx.Base.Common;
    using Sphinx.Core.DL;
    using Sphinx.Core.Entity;
    using System;

    /// <summary>
    /// ��־������
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
        /// ���������־
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

            logMessageBusiness.SetTime(logMessage);

            Console.WriteLine(logMessage.Timestamp);

            logMessageBusiness.Insert(logMessage);

            Assert.Pass();
        }

        /// <summary>
        /// ���Է����л�
        /// </summary>
        [Test]
        public void TestDeserialize()
        {
            string json = "{\"level\":5, \"system\":\"sphinx\", \"module\":\"hermes-account\",\"action\":\"find user\",\"message\":\"find user by 152222222\"}";

            var logMessage = logMessageBusiness.Deserialize(json);
            logMessageBusiness.SetTime(logMessage);
            Console.WriteLine(logMessage.ToString());

            Assert.AreEqual(5, logMessage.Level);
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
                Message = "insert message"
            };

            logMessageBusiness.SetTime(message);

            Console.WriteLine(logMessageBusiness.Serialize(message));
        }
    }
}