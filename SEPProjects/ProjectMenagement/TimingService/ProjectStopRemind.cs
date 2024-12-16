using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimingService
{
    public class ProjectStopRemind
    {
        public ProjectStopRemind()
        {

        }
        /// <summary>
        /// 获取七天后关闭的项目列表
        /// </summary>
        /// <returns></returns>
        public IList<ESP.Finance.Entity.ProjectInfo> GetRemindProjectList()
        {
            //在预计结束日期+2月-7天
            string terms = " and enddate='" + DateTime.Now.AddMonths(-1).AddDays(7).ToString("yyyy-MM-dd") + "' and projectcode <> '' and status not in(33,34,99)";
            return new ESP.Finance.DataAccess.ProjectDataProvider().GetList(terms, null);
        }

        public IList<ESP.Finance.Entity.ProjectInfo> GetTeamLeaderList()
        {
            //在预计结束日前前7天
            string terms = " and enddate='" + DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + "' and projectcode <> '' and status not in(33,34,99)";
            return new ESP.Finance.DataAccess.ProjectDataProvider().GetList(terms, null);
        }
       
    }
}
