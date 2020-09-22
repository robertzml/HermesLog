using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Sphinx.Model
{
    /// <summary>
    /// 日志消息类
    /// </summary>
    [SugarTable(tableName: "log")]
    public class LogMessage
    {
        #region Field
        /// <summary>
        /// 日志级别常量
        /// </summary>
        private readonly string[] levels = { "Exception", "Error", "Warning", "Info", "Debug", "Verbose" };
        #endregion //Field

        #region Property
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [SugarColumn(ColumnName = "level")]
        public int Level { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        [SugarColumn(ColumnName = "module")]
        public string Module { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        [SugarColumn(ColumnName = "action")]
        public string Action { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        [SugarColumn(ColumnName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        [SugarColumn(ColumnName = "timestamp")]
        public DateTime Timestamp { get; set; }
        #endregion //Property

        #region Override
        public override string ToString()
        {
            return string.Format("[{0}][{1}]-[{2}][{3}]-[{4}]", this.Timestamp, this.levels[this.Level], this.Module, this.Action, this.Message);
        }
        #endregion //Override
    }
}
