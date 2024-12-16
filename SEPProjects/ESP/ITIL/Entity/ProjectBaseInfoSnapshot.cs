using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.Entity
{
    /// <summary>
    /// 项目基础信息快照
    /// </summary>
    [Serializable]
    public class ProjectBaseInfoSnapshot:MarshalByRefObject
    {
        /// <summary>
        /// 项目主申请方信息
        /// </summary>
        public ESP.Finance.Entity.ProjectInfo Info { get; set; }
        /// <summary>
        /// 项目主申请方成员列表
        /// </summary>
        public IEnumerable<ESP.Finance.Entity.ProjectMemberInfo> ProjectMemberList { get; set; }
        /// <summary>
        /// 支持方信息列表
        /// </summary>
        public IList<SupportInfoSnapshot> SupportInfoList { get; set; }
    }
}
