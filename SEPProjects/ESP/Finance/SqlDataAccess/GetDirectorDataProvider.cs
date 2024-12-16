using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class GetDirectorDataProvider : ESP.Finance.IDataAccess.IDirectorDataViewProvider
    {

        #region IV_GetDirectorProvider 成员

        public int GetDirector(int depId)
        {
            string sql = "select top 1 DirectorId from V_GetDirector where depId = @depId";
            SqlParameter param = new SqlParameter("@depId",SqlDbType.Int,4);
            param.Value = depId;
            object obj = DbHelperSQL.GetSingle(sql, param);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public Entity.DirectorViewInfo GetModelByDepId(int DepId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 Id,FAName,DepId,DirectorId,DirectorName,
                                            ManagerId,ManagerName,CEOId,CEOName,FAId 
                            from V_GetDirector ");
            strSql.Append(" where DepId=@DepId");
            SqlParameter[] parameters = {
					new SqlParameter("@DepId", SqlDbType.Int,4)};
            parameters[0].Value = DepId;

            return CBO.FillObject<Entity.DirectorViewInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public string GetDirectors()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct DirectorId from V_GetDirector ");
            SqlDataReader sdr = DbHelperSQL.ExecuteReader( strSql.ToString());
            string Ids = string.Empty;
            while (sdr.Read())
            {
                Ids += sdr["DirectorId"] == DBNull.Value ? string.Empty : (sdr["DirectorId"].ToString() + ",");
            }
            sdr.Close();
            return Ids.TrimEnd(',');
        }

        public string GetManagers()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct ManagerId from V_GetDirector ");
            SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString());
            string Ids = string.Empty;
            while (sdr.Read())
            {
                Ids += sdr["ManagerId"] == DBNull.Value ? string.Empty : (sdr["ManagerId"].ToString() + ",");
            }
            sdr.Close();
            return Ids.TrimEnd(',');
        }

        #endregion

    }
}
