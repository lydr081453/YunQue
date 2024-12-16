using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Text;
using System.Configuration;
using ESP.Purchase.Common;


namespace ESP.Purchase.BusinessLogic
{
    public class VoteManager
    {
        public static int GetVoteCount(int userid)
        {
            string sql = @"select count(*) from t_vote where userid=" + userid.ToString();
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }
    }
}
