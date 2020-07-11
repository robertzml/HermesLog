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
    }
}