using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using Portal.Common;
namespace Portal.Data.Provider
{
    public interface IDatabaseProvider
    {
        #region Misc

        #region 人力地图搜索人员信息
        /// <summary>
        /// 根据搜索关键字，查询当前人员所在部门的人员信息（总监查看所有的）
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="userid">用户编号</param>
        /// <returns>返回一个人员信息列表</returns>
        IList<ESP.Framework.Entity.EmployeeInfo> Misc_HumanMap_SearchUserInfo(string keyword, int userid);
        #endregion

        #endregion
    }
}
