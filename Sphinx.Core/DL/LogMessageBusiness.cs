using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using SqlSugar;

namespace Sphinx.Core.DL
{
    using Sphinx.Base.Common;
    using Sphinx.Core.Entity;

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
                ConnectionString = AppSettings.MySqlConnection,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            return db;
        }
        #endregion //Function

        #region Methodd
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public string Serialize(LogMessage log)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Serialize<LogMessage>(log, serializeOptions);
        }

        /// <summary>
        /// 保存日志记录
        /// </summary>
        /// <param name="logMessage"></param>
        public void Insert(LogMessage logMessage)
        {
            try
            {
                var db = GetInstance();

                db.Insertable(logMessage).ExecuteCommand();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion //Method
    }
}
