using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Entity;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Common;

namespace ESP.Purchase.DataAccess
{
    public class RiskConsultationDataProvider
    {
        public int Add(RiskConsultationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_RiskConsultation(");
            strSql.Append("UserId,PrId,CommitDate,total)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@PrId,@CommitDate,@total)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@PrId", SqlDbType.Int,4),
					new SqlParameter("@CommitDate", SqlDbType.DateTime),
                    new SqlParameter("@total", SqlDbType.Int,4)
            };
            parameters[0].Value = model.Id;
             parameters[1].Value = model.UserId;
            parameters[2].Value = model.Prid;
            parameters[3].Value = model.CommitDate;
            parameters[4].Value = model.Total;
            object obj = null;
            using (SqlConnection newConn = new SqlConnection(DbHelperSQL.connectionString))
            {
                newConn.Open();
                try
                {
                    obj = DbHelperSQL.GetSingle(strSql.ToString(), newConn,null, parameters);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public IList<RiskConsultationInfo> GetList(int prid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM T_RiskConsultation  where Prid=" + prid.ToString()+" order by CommitDate Desc");
            return ESP.Finance.Utility.CBO.FillCollection<RiskConsultationInfo>(DbHelperSQL.Query(strSql.ToString(), null));
        }
    }
}
