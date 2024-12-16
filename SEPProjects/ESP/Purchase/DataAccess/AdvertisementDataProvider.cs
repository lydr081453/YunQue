using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
    public class AdvertisementDataProvider
    {
        public AdvertisementDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(AdvertisementInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Advertisement(");
            strSql.Append("MediaTypeID,MediaName,IsDel,CreatedDate,CreatedUserID,ModifiedDate,ModifiedUserID,Description,ProductTypeID)");
            strSql.Append(" values (");
            strSql.Append("@MediaTypeID,@MediaName,@IsDel,@CreatedDate,@CreatedUserID,@ModifiedDate,@ModifiedUserID,@Description,@ProductTypeID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MediaTypeID", SqlDbType.Int,8),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,6),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@ProductTypeID", SqlDbType.Int,6)};
            parameters[0].Value = model.MediaTypeID;
            parameters[1].Value = model.MediaName;
            parameters[2].Value = false;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.CreatedUserID;
            parameters[5].Value = DateTime.Now;
            parameters[6].Value = model.ModifiedUserID;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.ProductTypeID;

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
        public int Update(AdvertisementInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Advertisement set ");
            strSql.Append(@"MediaTypeID = @MediaTypeID,[MediaName] = @MediaName,IsDel = @IsDel,CreatedDate = @CreatedDate,CreatedUserID = @CreatedUserID
                ,ModifiedDate = @ModifiedDate,ModifiedUserID = @ModifiedUserID,Description=@Description,ProductTypeID=@ProductTypeID");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@MediaTypeID", SqlDbType.Int,8),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,6),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@id", SqlDbType.Int,6),
					new SqlParameter("@ProductTypeID", SqlDbType.Int,6)};
            parameters[0].Value = model.MediaTypeID;
            parameters[1].Value = model.MediaName;
            parameters[2].Value = false;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.CreatedUserID;
            parameters[5].Value = DateTime.Now;
            parameters[6].Value = model.ModifiedUserID;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.ID;
            parameters[9].Value = model.ProductTypeID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Advertisement ");
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
        public AdvertisementInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Advertisement ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            AdvertisementInfo model = new AdvertisementInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductTypeID"].ToString() != "")
                {
                    model.ProductTypeID = int.Parse(ds.Tables[0].Rows[0]["ProductTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MediaTypeID"].ToString() != "")
                {
                    model.MediaTypeID = int.Parse(ds.Tables[0].Rows[0]["MediaTypeID"].ToString());
                }
                model.MediaName = ds.Tables[0].Rows[0]["MediaName"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
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

        private AdvertisementInfo SetModel(DataRow dr)
        {
            AdvertisementInfo model = new AdvertisementInfo();

            if (dr["ID"].ToString() != "")
            {
                model.ID = int.Parse(dr["ID"].ToString());
            }
            if (dr["ProductTypeID"].ToString() != "")
            {
                model.ProductTypeID = int.Parse(dr["ProductTypeID"].ToString());
            }
            if (dr["MediaTypeID"].ToString() != "")
            {
                model.MediaTypeID = int.Parse(dr["MediaTypeID"].ToString());
            }
            model.MediaName = dr["MediaName"].ToString();
            model.Description = dr["Description"].ToString();
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
        public DataSet GetList(string strWhere, string strOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM T_Advertisement ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(strOrder))
            {
                strSql.Append(" Order By " + strOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public IList<AdvertisementInfo> GetInfoList(string strWhere,string strOrder)
        {
            DataSet ds = GetList(strWhere, strOrder);
            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();
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
