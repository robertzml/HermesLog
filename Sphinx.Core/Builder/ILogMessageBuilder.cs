using System;
using System.Collections.Generic;
using System.Text;

namespace Sphinx.Core.Builder
{
    using Sphinx.Core.Entity;

    /// <summary>
    /// 日志消息构建
    /// </summary>
    public interface ILogMessageBuilder
    {
        /// <summary>
        /// 创建日志消息
        /// </summary>
        /// <returns></returns>
        LogMessage Build();

        /// <summary>
        /// 设置json字符串
        /// </summary>
        /// <param name="json">序列化后内容</param>
        /// <returns></returns>
        ILogMessageBuilder SetJson(string json);

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <returns></returns>
        ILogMessageBuilder SetId();

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <returns></returns>
        ILogMessageBuilder SetTime();
    }
}
