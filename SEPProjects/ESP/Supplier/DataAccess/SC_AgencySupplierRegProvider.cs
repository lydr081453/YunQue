using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using System.Data;
using ESP.Supplier.Entity;

namespace ESP.Supplier.DataAccess
{
    public class SC_AgencySupplierRegProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_AgencySupplierReg model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_AgencySupplierReg(");
            strSql.Append("[supplierId],[suplierName],[createUserId],[createUserName],[createDate],[createIp],[createDesc])");
            strSql.Append(" values (");
            strSql.Append("@supplierId,@suplierName,@createUserId,@createUserName,@createDate,@createIp,@createDesc)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@suplierName", SqlDbType.NVarChar),
					new SqlParameter("@createUserId", SqlDbType.Int,4),
					new SqlParameter("@createUserName", SqlDbType.NVarChar),
                    new SqlParameter("@createDate",SqlDbType.DateTime),
                    new SqlParameter("@createIp",SqlDbType.NVarChar),
                    new SqlParameter("@createDesc",SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.SuplierName;
            parameters[2].Value = model.CreateUserId;
            parameters[3].Value = model.CreateUserName;
            parameters[4].Value = model.CreateDate;
            parameters[5].Value = model.CreateIp;
            parameters[6].Value = model.CreateDesc;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection,trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_AgencySupplierReg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_AgencySupplierReg> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_AgencySupplierReg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_AgencySupplierReg>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public List<SC_AgencySupplierReg> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_AgencySupplierReg>(GetList(""));
        }
    }
}
