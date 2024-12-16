using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimingService
{
    public class ProjectClose
    {
        public void ClosePrj()
        {
            string Ids = GetProjectIds();
            if (!string.IsNullOrEmpty(Ids))
            {
                string updateTerm = " update f_project set status=" + (int)ESP.Finance.Utility.Status.ProjectPreClose + " where projectid in({0})";
                updateTerm += " ; insert f_timingLog values(0,'" + DateTime.Now + "','{1}')";
                updateTerm = string.Format(updateTerm, Ids, Ids);
                ESP.Finance.DataAccess.DbHelperSQL.ExecuteSql(updateTerm);
            }
        }

        public string GetProjectIds()
        {
            string terms = " and enddate<='" + DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + "' and projectcode <> '' and status=32";
            IList<ESP.Finance.Entity.ProjectInfo> list =  new ESP.Finance.DataAccess.ProjectDataProvider().GetList(terms, null);
            string Ids = "";
            foreach (ESP.Finance.Entity.ProjectInfo model in list)
            {
                Ids += model.ProjectId + ",";
            }
            return Ids.TrimEnd(',');
        }
    }
}
