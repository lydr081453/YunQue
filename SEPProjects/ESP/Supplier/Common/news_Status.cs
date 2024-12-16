using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Common
{
    public class Status
    {
        public enum NewsStatus
        {
            /// <summary>
            /// 删除
            /// </summary>
            Deleted = 0,
            /// <summary>
            /// 正常
            /// </summary>
            Common = 1
        }

        /// <summary>
        /// 前台用户状态
        /// </summary>
        public enum siteUsersStatus : int
        {
            /// <summary>
            /// 正常使用
            /// </summary>
            Common=0
        }

        /// <summary>
        /// 答疑解惑状态
        /// </summary>
        public enum resoStatus : int
        {
            /// <summary>
            /// 待审核
            /// </summary>
            auditing = 0,
            /// <summary>
            /// 审核通过
            /// </summary>
            audited = 1,
            /// <summary>
            /// 删除
            /// </summary>
            del = 2
        }
        public static string[] resoStatusNames = {"待审核","审核通过","已删除" };
    }
}