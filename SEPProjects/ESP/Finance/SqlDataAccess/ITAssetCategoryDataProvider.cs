using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class ITAssetCategoryDataProviderr : ESP.Finance.IDataAccess.IITAssetCategoryProvider
    {
        public IList<ESP.Finance.Entity.ITAssetCategoryInfo> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_ITAssetCategory ");
            return CBO.FillCollection<ITAssetCategoryInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public ESP.Finance.Entity.ITAssetCategoryInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_ITAssetCategory ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<ITAssetCategoryInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }
    }
}
