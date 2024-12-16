using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.Entity
{
    /// <summary>
    /// PR单基础信息快照
    /// </summary>
    [Serializable]
    public class PrSnapshot : MarshalByRefObject
    {
        /// <summary>
        /// PR单业务类型
        /// </summary>
        public Common.Enum.PrBusinessType BusinessType;
        /// <summary>
        /// PR单基础信息
        /// </summary>
        public ESP.Purchase.Entity.GeneralInfo BaseInfo { get; set; }

    }
}
