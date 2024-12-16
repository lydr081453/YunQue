using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.Common
{
    public class Enum
    {
        /// <summary>
        /// PR单业务类型
        /// 普通对公（不包含押金）
        /// 对私费用
        /// 媒体稿件费用
        /// 对公包含押金
        /// 对私包含借款
        /// </summary>
        public enum PrBusinessType
        {
            Public = 0,
            Private = 1,
            MediaFee = 2,
            PublicWithDeposit = 3,
            PrivateWithBorrowing = 4
        }
    }
}
