using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.BusinessLogic
{
    /// <summary>
    /// 采购运维业务逻辑
    /// PO部分
    /// </summary>
    public partial class Purchase
    {
        /// <summary>
        /// 检查数据完整性
        /// </summary>
        public void CheckData()
        {
            ESP.ITIL.BusinessLogic.Rules.Purchase.已经提交的PR单项目ID必须为有效值.Check();
        }
    }
}
