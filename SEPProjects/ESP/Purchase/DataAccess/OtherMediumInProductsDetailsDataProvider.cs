using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    class OtherMediumInProductsDetailsDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditBackUpDataProvider"/> class.
        /// </summary>
        public OtherMediumInProductsDetailsDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(OtherMediumInProductDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OtherMediumInProductDetails(");
            strSql.Append("Area,ManuscriptType,NewsPrice,Description,Layout,HopePrice,ShunYaDescription,CreatedDate,CreatedUserID,ModifiedDate,ModifiedUserID,MediaProductID,Unit,TitlePrice,Discount,IsHavePic)");
            strSql.Append(" values (");
            strSql.Append("@Area,@ManuscriptType,@NewsPrice,@Description,@Layout,@HopePrice,@ShunYaDescription,@CreatedDate,@CreatedUserID,@ModifiedDate,@ModifiedUserID,@MediaProductID,@Unit,@TitlePrice,@Discount,@IsHavePic)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Area", SqlDbType.NVarChar,500),
					new SqlParameter("@ManuscriptType", SqlDbType.Int,4),
					new SqlParameter("@NewsPrice", SqlDbType.NVarChar,500),
					new SqlParameter("@Description", SqlDbType.NVarChar,2000),
					new SqlParameter("@Layout", SqlDbType.NVarChar,500),
					new SqlParameter("@HopePrice", SqlDbType.NVarChar,8),
					new SqlParameter("@ShunYaDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,6),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6),
					new SqlParameter("@MediaProductID", SqlDbType.Int,6),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@TitlePrice", SqlDbType.NVarChar,500),
					new SqlParameter("@Discount", SqlDbType.NVarChar,50),
					new SqlParameter("@IsHavePic", SqlDbType.Bit)};
            parameters[0].Value = model.Area;
            parameters[1].Value = model.ManuscriptType;
            parameters[2].Value = model.NewsPrice;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Layout;
            parameters[5].Value = model.HopePrice;
            parameters[6].Value = model.ShunYaDescription;
            parameters[7].Value = DateTime.Now;
            parameters[8].Value = model.CreatedUserID;
            parameters[9].Value = DateTime.Now;
            parameters[10].Value = model.ModifiedUserID;
            parameters[11].Value = model.MediaProductID;
            parameters[12].Value = model.Unit;
            parameters[13].Value = model.TitlePrice;
            parameters[14].Value = model.Discount;
            parameters[15].Value = model.IsHavePic;

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
        public int Update(OtherMediumInProductDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OtherMediumInProductDetails set ");
            strSql.Append(@"Area = @Area,ManuscriptType = @ManuscriptType,NewsPrice = @NewsPrice,Description = @Description
                ,Layout = @Layout,HopePrice = @HopePrice,ShunYaDescription = @ShunYaDescription,CreatedDate = @CreatedDate,CreatedUserID = @CreatedUserID
                ,ModifiedDate = @ModifiedDate,ModifiedUserID = @ModifiedUserID,MediaProductID=@MediaProductID,Unit=@Unit,TitlePrice=@TitlePrice,Discount=@Discount,IsHavePic=@IsHavePic");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@Area", SqlDbType.NVarChar,500),
					new SqlParameter("@ManuscriptType", SqlDbType.Int,4),
					new SqlParameter("@NewsPrice", SqlDbType.NVarChar,500),
					new SqlParameter("@Description", SqlDbType.NVarChar,2000),
					new SqlParameter("@Layout", SqlDbType.NVarChar,500),
					new SqlParameter("@HopePrice", SqlDbType.NVarChar,8),
					new SqlParameter("@ShunYaDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,6),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6),
					new SqlParameter("@id", SqlDbType.Int,6),
					new SqlParameter("@MediaProductID", SqlDbType.Int,6),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@TitlePrice", SqlDbType.NVarChar,500),
					new SqlParameter("@Discount", SqlDbType.NVarChar,50),
					new SqlParameter("@IsHavePic", SqlDbType.Bit)};
            parameters[0].Value = model.Area;
            parameters[1].Value = model.ManuscriptType;
            parameters[2].Value = model.NewsPrice;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Layout;
            parameters[5].Value = model.HopePrice;
            parameters[6].Value = model.ShunYaDescription;
            parameters[7].Value = model.CreatedDate;
            parameters[8].Value = model.CreatedUserID;
            parameters[9].Value = DateTime.Now;
            parameters[10].Value = model.ModifiedUserID;
            parameters[11].Value = model.ID;
            parameters[12].Value = model.MediaProductID;
            parameters[13].Value = model.Unit;
            parameters[14].Value = model.TitlePrice;
            parameters[15].Value = model.Discount;
            parameters[16].Value = model.IsHavePic;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OtherMediumInProductDetails ");
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
        public OtherMediumInProductDetailsInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_OtherMediumInProductDetails ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            OtherMediumInProductDetailsInfo model = new OtherMediumInProductDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MediaProductID"].ToString() != "")
                {
                    model.MediaProductID = int.Parse(ds.Tables[0].Rows[0]["MediaProductID"].ToString());
                }
                model.Area = ds.Tables[0].Rows[0]["Area"].ToString();
                if (ds.Tables[0].Rows[0]["ManuscriptType"].ToString() != "")
                {
                    model.ManuscriptType = int.Parse(ds.Tables[0].Rows[0]["ManuscriptType"].ToString());
                }
                model.NewsPrice = ds.Tables[0].Rows[0]["NewsPrice"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                model.Layout = ds.Tables[0].Rows[0]["Layout"].ToString();
                model.HopePrice = ds.Tables[0].Rows[0]["HopePrice"].ToString();
                model.ShunYaDescription = ds.Tables[0].Rows[0]["ShunYaDescription"].ToString();
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                model.TitlePrice = ds.Tables[0].Rows[0]["TitlePrice"].ToString();
                model.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                model.IsHavePic = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsHavePic"]);
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
            strSql.Append(" FROM T_OtherMediumInProductDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}
