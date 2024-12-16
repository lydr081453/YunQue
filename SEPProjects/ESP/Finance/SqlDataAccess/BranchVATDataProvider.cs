using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
   internal class BranchVATDataProvider: ESP.Finance.IDataAccess.IBranchVATProvider
    {
       public IList<BranchVATInfo> GetList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM F_BranchVAT ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<BranchVATInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
