using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_GroupAndSupplierRelationDataProvider
    {
        public SC_GroupAndSupplierRelationDataProvider()
        { }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_GroupAndSupplierRelation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_GroupAndSupplierRelation(");
            strSql.Append("SupplierID,GroupID,CreatedDate,ModifiedDate)");
            strSql.Append(" values (");
            strSql.Append("@SupplierID,@GroupID,@CreatedDate,@ModifiedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupplierID", SqlDbType.Int,8),
                    new SqlParameter("@GroupID", SqlDbType.Int,8),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@ModifiedDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.SupplierID;
            parameters[1].Value = model.GroupID;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.ModifiedDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_GroupAndSupplierRelation ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
            parameters[0].Value = Id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_GroupAndSupplierRelation GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_GroupAndSupplierRelation ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return ESP.ConfigCommon.CBO.FillObject<SC_GroupAndSupplierRelation>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM SC_GroupAndSupplierRelation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_GroupAndSupplierRelation> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_GroupAndSupplierRelation where 1=1 ");
            strSql.Append(strWhere);
            return ESP.ConfigCommon.CBO.FillCollection<SC_GroupAndSupplierRelation>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_GroupAndSupplierRelation> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_GroupAndSupplierRelation>(GetList(""));
        }
    }
}
