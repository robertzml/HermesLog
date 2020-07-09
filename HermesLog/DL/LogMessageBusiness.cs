using System;
using System.Collections.Generic;
using System.Text;
using HermesLog.Model;
using SqlSugar;

namespace HermesLog.DL
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
        /// 添加日志记录
        /// </summary>
        /// <param name="logMessage"></param>
        public void Insert(LogMessage logMessage)
        {
            var db = GetInstance();

            db.Insertable(logMessage).ExecuteCommand();
        }
        #endregion //Method
    }
}
