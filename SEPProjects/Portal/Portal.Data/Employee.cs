using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Portal.Data
{
    public class Employee
    {
        #region 人力地图搜索人员信息
        /// <summary>
        /// 根据搜索关键字，查询当前人员所在部门的人员信息（总监查看所有的）
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="userid">用户编号</param>
        /// <returns>返回一个人员信息列表</returns>
        public static IList<ESP.Framework.Entity.EmployeeInfo> Misc_HumanMap_SearchUserInfo(string keyword, int userid)
        {
            string sql = @"select distinct s.* from sep_EmployeesInPositions ep1 
                           join sep_EmployeesInPositions ep2 on ep1.DepartmentID=ep2.DepartmentID
                           join V_EmpSearch s on ep1.UserID=s.UserID
                           where ep2.UserID=@UserID
                           AND EmpInfo LIKE '%' + @KeyWord + '%'";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserID", System.Data.DbType.Int32, userid);
            db.AddInParameter(cmd, "KeyWord", System.Data.DbType.String, keyword);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadList<ESP.Framework.Entity.EmployeeInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 判断用户是否是总监以上级别
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>如果是总监以上就返回true,否则返回false</returns>
        public static bool Misc_HumanMap_CheckUserIsMajordomo(int UserID, int majordomoPosLevel)
        {
            bool b = false;
            IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.Framework.Entity.EmployeePositionInfo empPosInfo in list)
                {
                    if (empPosInfo.PositionLevel >= majordomoPosLevel)
                    {
                        b = true;
                        break;
                    }
                }
            }
            return b;
        }
        #endregion
    }
}
