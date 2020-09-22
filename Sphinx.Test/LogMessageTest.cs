using NUnit.Framework;

namespace HermesLog.Test
{
    using HermesLog.DL;
    using HermesLog.Model;
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
                Module = "hermes-log",
                Action = "test",
                Message = "test insert 2"
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
            string json = "{\"level\":5,\"module\":\"hermes-account\",\"action\":\"find user\",\"message\":\"find user by 15995202790\"}";

            var logMessage = logMessageBusiness.Deserialize(json);
            logMessageBusiness.SetTime(logMessage);
            Console.WriteLine(logMessage.ToString());

            Assert.AreEqual(5, logMessage.Level);
        }
    }
}