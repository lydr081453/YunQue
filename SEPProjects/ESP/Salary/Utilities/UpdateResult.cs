using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Salary.Utility
{
    public enum UpdateResult : int
    {
        /// <summary>
        /// 失败的
        /// </summary>
        Failed = 0,

        /// <summary>
        /// 没有执行
        /// </summary>
        UnExecute = 1,

        /// <summary>
        /// 时间戳过期
        /// </summary>
        TimeStampOverdue = 2,

        /// <summary>
        /// 有重复
        /// </summary>
        Iterative = 3,


        /// <summary>
        /// 不能提交
        /// </summary>
        CannotSubmit = 4,

        /// <summary>
        /// 总金额超出
        /// </summary>
        AmountOverflow = 5,

        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 5000

    }

}