using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace ESP.Finance.DataAccess
{
    class TicketCityProvider:IDataAccess.ITicketCityProvider
    {
        #region ITicketCityProvider 成员

        public ESP.Finance.Entity.TicketCityInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_TicketCity ");
            strSql.Append(" where Id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", System.Data.SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<TicketCityInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public IList<ESP.Finance.Entity.TicketCityInfo> GetList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_TicketCity ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<TicketCityInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion
    }
}
