using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;

namespace ESP.Administrative.DataAccess
{
    public class OverWorkRecordsDataProvicer
    {
        public void Add(ESP.Administrative.Entity.OverWorkRecordsInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_OverWorkRecords(");
            strSql.Append("OverWorkId,TakeOffId,hours,type)");
            strSql.Append(" values (");
            strSql.Append("@OverWorkId,@TakeOffId,@hours,@type)");
            SqlParameter[] parameters = {
					new SqlParameter("@OverWorkId", SqlDbType.Int),
					new SqlParameter("@TakeOffId", SqlDbType.Int),
					new SqlParameter("@hours", SqlDbType.Decimal),
                    new SqlParameter("@type",SqlDbType.Int)
                                        };
            parameters[0].Value = model.OverWorkId;
            parameters[1].Value = model.TakeOffId;
            parameters[2].Value = model.Hours;
            parameters[3].Value = model.Type;

            if(trans != null)
                DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parameters);
            else
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public List<OverWorkRecordsInfo> GetList(int TakeOffId,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_OverWorkRecords where TakeOffId=@TakeOffId ");
            SqlParameter[] parameters = {
					new SqlParameter("@TakeOffId", SqlDbType.Int)};
            parameters[0].Value = TakeOffId;
            if (trans == null)
                return CBO.FillCollection<OverWorkRecordsInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            else
                return CBO.FillCollection<OverWorkRecordsInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));
        }
        public List<OverWorkRecordsInfo> GetList(int TakeOffId)
        {
            return GetList(TakeOffId, null);
        }


        public bool Delete(int TakeOffId,SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_OverWorkRecords ");
            strSql.Append(" where TakeOffId=@TakeOffId");
            SqlParameter[] parameters = {
					new SqlParameter("@TakeOffId", SqlDbType.Int,4)
			};
            parameters[0].Value = TakeOffId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
