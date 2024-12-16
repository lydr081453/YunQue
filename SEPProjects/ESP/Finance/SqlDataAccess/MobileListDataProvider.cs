using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    public class MobileListDataProvider : ESP.Finance.IDataAccess.IMobileListProvider
    {
        #region IMobileListProvider 成员

        public ESP.Finance.Entity.MobileListInfo GetModel(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * FROM f_mobilelist where userid= "+userid.ToString());
          
            return CBO.FillObject<ESP.Finance.Entity.MobileListInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion
    }
}
