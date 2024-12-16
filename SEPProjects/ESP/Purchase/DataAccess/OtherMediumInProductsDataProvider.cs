using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
    public class OtherMediumInProductsDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditBackUpDataProvider"/> class.
        /// </summary>
        public OtherMediumInProductsDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(OtherMediumInProductInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OtherMediumInProduct(");
            strSql.Append("ProductID,MediaName,IsDel,CreatedDate,CreatedUserID,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@ProductID,@MediaName,@IsDel,@CreatedDate,@CreatedUserID,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.Int,8),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,6),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6)};
            parameters[0].Value = model.ProductID;
            parameters[1].Value = model.MediaName;
            parameters[2].Value = false;
            parameters[3].Value = model.CreatedDate;
            parameters[4].Value = model.CreatedUserID;
            parameters[5].Value = model.ModifiedDate;
            parameters[6].Value = model.ModifiedUserID;

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
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Update(OtherMediumInProductInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OtherMediumInProduct set ");
            strSql.Append(@"ProductID = @ProductID,[MediaName] = @MediaName,IsDel = @IsDel,CreatedDate = @CreatedDate,CreatedUserID = @CreatedUserID
                ,ModifiedDate = @ModifiedDate,ModifiedUserID = @ModifiedUserID");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.Int,8),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,6),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6),
					new SqlParameter("@id", SqlDbType.Int,6)};
            parameters[0].Value = model.ProductID;
            parameters[1].Value = model.MediaName;
            parameters[2].Value = model.IsDel;
            parameters[3].Value = model.CreatedDate;
            parameters[4].Value = model.CreatedUserID;
            parameters[5].Value = model.ModifiedDate;
            parameters[6].Value = model.ModifiedUserID;
            parameters[7].Value = model.ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OtherMediumInProduct ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public OtherMediumInProductInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_OtherMediumInProduct ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            OtherMediumInProductInfo model = new OtherMediumInProductInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductID"].ToString() != "")
                {
                    model.ProductID = int.Parse(ds.Tables[0].Rows[0]["ProductID"].ToString());
                }
                model.MediaName = ds.Tables[0].Rows[0]["MediaName"].ToString();
                model.IsDel = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDel"]);
                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedDate"]);
                }
                if (ds.Tables[0].Rows[0]["ModifiedDate"].ToString() != "")
                {
                    model.ModifiedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ModifiedDate"]);
                }
                if (ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != "")
                {
                    model.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedUserID"].ToString() != "")
                {
                    model.ModifiedUserID = int.Parse(ds.Tables[0].Rows[0]["ModifiedUserID"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        private OtherMediumInProductInfo SetModel(DataRow dr)
        {
            OtherMediumInProductInfo model = new OtherMediumInProductInfo();

            if (dr["ID"].ToString() != "")
            {
                model.ID = int.Parse(dr["ID"].ToString());
            }
            if (dr["ProductID"].ToString() != "")
            {
                model.ProductID = int.Parse(dr["ProductID"].ToString());
            }
            model.MediaName = dr["MediaName"].ToString();
            model.IsDel = Convert.ToBoolean(dr["IsDel"]);
            if (dr["CreatedDate"].ToString() != "")
            {
                model.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
            }
            if (dr["ModifiedDate"].ToString() != "")
            {
                model.ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
            }
            if (dr["CreatedUserID"].ToString() != "")
            {
                model.CreatedUserID = int.Parse(dr["CreatedUserID"].ToString());
            }
            if (dr["ModifiedUserID"].ToString() != "")
            {
                model.ModifiedUserID = int.Parse(dr["ModifiedUserID"].ToString());
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM T_OtherMediumInProduct ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public IList<OtherMediumInProductInfo> GetInfoList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            IList<OtherMediumInProductInfo> list = new List<OtherMediumInProductInfo>();
            if(ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(SetModel(dr));
                }
            }
            return list ;
        }
        #endregion  成员方法
    }
}
