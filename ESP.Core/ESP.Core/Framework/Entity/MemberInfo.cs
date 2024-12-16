using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 成员信息
    /// 用户（目前就是员工）登录后就应该建立这个对象
    /// </summary>
    [Serializable]
    public class MemberInfo
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public UserInfo UserInfomation { get; set; }
        /// <summary>
        /// 员工信息
        /// </summary>
        public EmployeeInfo EmployeeInformation { get; set; }
        /// <summary>
        /// 员工部门职务信息
        /// </summary>
        public IList<EmployeePositionInfo> EmployeePositionInfomationList { get; set; }
    }
}
