using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Common
{
    public class common
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum logType : int
        {
            Login = 0
        }

        /// <summary>
        /// 人员类型
        /// </summary>
        public enum userType : int
        {
            /// <summary>
            /// 管理员
            /// </summary>
            Manager = 0,
            /// <summary>
            /// 普通用户
            /// </summary>
            User = 1
        }
    }
}
