using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
   public  class PositionViewProvider
    {
       public List<ESP.HumanResource.Entity.PositionView> GetModelList(int deptid)
       {
           string strSql = "select * from V_Position where departmentid= "+deptid.ToString();
           List<ESP.HumanResource.Entity.PositionView> list = new List<ESP.HumanResource.Entity.PositionView>();
           using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
           {
               while (r.Read())
               {
                   ESP.HumanResource.Entity.PositionView model = new ESP.HumanResource.Entity.PositionView();
                   model.PopupData(r);
                   list.Add(model);
               }
               r.Close();
           }
           return list;
       }
    }
}
