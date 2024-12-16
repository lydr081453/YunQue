using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.Common
{
    public class ErrorMessage
    {
        /// <summary>
        /// 是否有异常
        /// </summary>
        public bool IsException { get; set; }
        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// 如果有异常，这个字段就是引发错误的异常信息
        /// </summary>
        public ITILException InterException { get; set; }
        /// <summary>
        /// 对于错误的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 错误的代码，目前还没有开始编码，此字段暂时没有用处
        /// </summary>
        public bool ErrorCode { get; set; }
        /// <summary>
        /// 引发错误的Sql，如果有的话
        /// </summary>
        public string ErrorSql { get; set; }
        /// <summary>
        /// 引发错误的SqlParameters集合
        /// </summary>
        public List<System.Data.SqlClient.SqlParameter> ErrorSqlParameterList { get; set; }
    }
}
