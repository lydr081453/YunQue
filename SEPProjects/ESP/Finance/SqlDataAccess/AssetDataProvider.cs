using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class AssetDataProvider:ESP.Finance.IDataAccess.IAssetProvider
    {

        #region IAssetProvider 成员

        public int Add(ESP.Finance.Entity.AssetInfo model)
        {
            throw new NotImplementedException();
        }

        public int Update(ESP.Finance.Entity.AssetInfo model)
        {
            throw new NotImplementedException();
        }

        public int Delete(int assetid)
        {
            throw new NotImplementedException();
        }

        public ESP.Finance.Entity.AssetInfo GetModel(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from F_Assets ");
            strSql.Append(" where UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)};
            parameters[0].Value = UserId;

            return CBO.FillObject<AssetInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public IList<ESP.Finance.Entity.AssetInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_Assets ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<AssetInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
        
        #endregion
    }
}
