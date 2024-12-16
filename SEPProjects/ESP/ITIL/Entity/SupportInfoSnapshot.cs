using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.Entity
{
    /// <summary>
    /// 支持方基础信息快照
    /// </summary>
    [Serializable]
    public class SupportInfoSnapshot : MarshalByRefObject
    {
        /// <summary>
        /// 支持方信息
        /// </summary>
        public ESP.Finance.Entity.SupporterInfo Info { get; set; }
        /// <summary>
        /// 支持方成员信息列表
        /// </summary>
        public IEnumerable<ESP.Finance.Entity.SupportMemberInfo> MemberList { get; set; }
    }
}
