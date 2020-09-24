using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Sphinx.Core.Builder
{
    using Sphinx.Core.Entity;

    /// <summary>
    /// 日志消息构建
    /// </summary>
    public class LogMessageBuilder : ILogMessageBuilder
    {
        #region Field
        /// <summary>
        /// 日志消息对象
        /// </summary>
        private LogMessage logMessage;
        #endregion //Field

        #region Constructor
        public LogMessageBuilder()
        {
            this.logMessage = new LogMessage();
        }

        public LogMessageBuilder(LogMessage logMessage)
        {
            this.logMessage = logMessage;
        }

        /// <summary>
        /// 日志消息构建
        /// </summary>
        /// <param name="msg">序列化后字符串</param>
        public LogMessageBuilder(string msg)
        {
            this.logMessage = Deserialize(msg);
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 反序列化日志对象
        /// </summary>
        /// <param name="msg">日志json字符串</param>
        /// <returns></returns>
        private LogMessage Deserialize(string msg)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var log = JsonSerializer.Deserialize<LogMessage>(msg, serializeOptions);
            return log;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 创建日志消息
        /// </summary>
        /// <returns></returns>
        public LogMessage Build()
        {
            return this.logMessage;
        }

        /// <summary>
        /// 设置json字符串
        /// </summary>
        /// <param name="json">序列化后内容</param>
        /// <returns></returns>
        public ILogMessageBuilder SetJson(string json)
        {
            this.logMessage = Deserialize(json);
            return this;
        }

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <returns></returns>
        public ILogMessageBuilder SetId()
        {
            logMessage.Id = Guid.NewGuid().ToString();
            return this;
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <returns></returns>
        public ILogMessageBuilder SetTime()
        {
            logMessage.Timestamp = DateTime.Now;
            return this;
        }
        #endregion //Method
    }
}
