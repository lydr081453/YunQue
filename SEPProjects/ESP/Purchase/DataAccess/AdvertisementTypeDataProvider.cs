using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    public class AdvertisementTypeDataProvider
    {
        public AdvertisementTypeDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(AdvertisementTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_AdvertisementType(");
            strSql.Append("TypeName,CreatedDate,CreatedUserID,ModifiedDate,ModifiedUserID,IsDel)");
            strSql.Append(" values (");
            strSql.Append("@TypeName,@CreatedDate,@CreatedUserID,@ModifiedDate,@ModifiedUserID,@IsDel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,8),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,8),
					new SqlParameter("@IsDel", SqlDbType.Bit) };
            parameters[0].Value = model.TypeName;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = model.CreatedUserID;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.ModifiedUserID;
            parameters[5].Value = false;
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
        public int Update(AdvertisementTypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_AdvertisementType set ");
            strSql.Append(@"[TypeName] = @TypeName,CreatedDate = @CreatedDate,CreatedUserID = @CreatedUserID
                ,ModifiedDate = @ModifiedDate,ModifiedUserID = @ModifiedUserID, IsDel=@IsDel");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,8),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,8),
					new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@IsDel", SqlDbType.Bit) };
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.CreatedUserID;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.ModifiedUserID;
            parameters[5].Value = model.ID;
            parameters[6].Value = model.IsDel;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_AdvertisementType ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,6)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public AdvertisementTypeInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_AdvertisementType ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            AdvertisementTypeInfo model = new AdvertisementTypeInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.TypeName = ds.Tables[0].Rows[0]["TypeName"].ToString();
                model.IsDel = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDel"].ToString());
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

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM T_AdvertisementType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}