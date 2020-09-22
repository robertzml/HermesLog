using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Sphinx.Model;
using SqlSugar;

namespace Sphinx.DL
{
    /// <summary>
    /// 日志业务类
    /// </summary>
    public class LogMessageBusiness
    {
        #region Function
        private SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Server=192.168.1.121;Database=hermes-log;User=root;Password=123456",
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
           
            return db;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 反序列化日志对象
        /// </summary>
        /// <param name="msg">日志json字符串</param>
        /// <returns></returns>
        public LogMessage Deserialize(string msg)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var log = JsonSerializer.Deserialize<LogMessage>(msg, serializeOptions);
            return log;
        }

        /// <summary>
        /// 设置日志对象ID和时间
        /// </summary>
        /// <param name="log"></param>
        public void SetTime(LogMessage log)
        {
            log.Id = Guid.NewGuid().ToString();
            log.Timestamp = DateTime.Now;
        }

        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="logMessage"></param>
        public void Insert(LogMessage logMessage)
        {
            try
            {
                var db = GetInstance();

                db.Insertable(logMessage).ExecuteCommand();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion //Method
    }
}
