using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Entity;
using System.Data;
using ESP.Purchase.Common;

namespace ESP.Purchase.DataAccess
{
   public  class SupplierShareProvider
    {
       public SupplierShareProvider()
       { }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int UpdateStatus(int supplySupplierId,int receiverId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierShare set ");
            strSql.Append("status=1");
            strSql.Append(" where supplySupplierId=@supplySupplierId and receiverId=@receiverId");
            SqlParameter[] parameters = {
                    new SqlParameter("@supplySupplierId",SqlDbType.Int,4),
					new SqlParameter("@receiverId", SqlDbType.Int,4)
                                        };

            parameters[0].Value = supplySupplierId;
            parameters[1].Value = receiverId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        /// <returns></returns>
        public SupplierShareInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierShare ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.SupplierShareInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public List<SupplierShareInfo> GetList(string sqlwhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierShare ");
            if (!string.IsNullOrEmpty(sqlwhere))
            {
                strSql.Append(" where "+sqlwhere);
            }
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.SupplierShareInfo>(DbHelperSQL.Query(strSql.ToString()));

        }
    }
}
