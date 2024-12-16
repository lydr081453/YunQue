using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    class AdvertisementDetailsDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditBackUpDataProvider"/> class.
        /// </summary>
        public AdvertisementDetailsDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(AdvertisementDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_AdvertisementDetails(");
            strSql.Append(@"AdvertisementID,Discount,DiscountDescription,DistributionMin,DistributionMax,DistributionPercent,ReturnPoint,AccountPeriod,CreatedDate
                ,CreatedUserID,ModifiedDate,ModifiedUserID,DistributionDescription)");
            strSql.Append(" values (");
            strSql.Append(@"@AdvertisementID,@Discount,@DiscountDescription,@DistributionMin,@DistributionMax,@DistributionPercent,@ReturnPoint,@AccountPeriod,@CreatedDate
                ,@CreatedUserID,@ModifiedDate,@ModifiedUserID,@DistributionDescription)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AdvertisementID", SqlDbType.Int,4),
                    
					new SqlParameter("@Discount", SqlDbType.Decimal,8),
					new SqlParameter("@DiscountDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@DistributionMin", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionMax", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionPercent", SqlDbType.Decimal,8),
					new SqlParameter("@ReturnPoint", SqlDbType.Decimal,8),
					new SqlParameter("@AccountPeriod", SqlDbType.NVarChar,50),

					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,6),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6),
					new SqlParameter("@DistributionDescription", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.AdvertisementID;
            parameters[1].Value = model.Discount;
            parameters[2].Value = model.DiscountDescription;
            parameters[3].Value = model.DistributionMin;
            parameters[4].Value = model.DistributionMax;
            parameters[5].Value = model.DistributionPercent;
            parameters[6].Value = model.ReturnPoint;
            parameters[7].Value = model.AccountPeriod;
            parameters[8].Value = DateTime.Now;
            parameters[9].Value = model.CreatedUserID;
            parameters[10].Value = DateTime.Now;
            parameters[11].Value = model.ModifiedUserID;
            parameters[12].Value = model.DistributionDescription;

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
        public int Update(AdvertisementDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_AdvertisementDetails set ");
            strSql.Append(@"AdvertisementID=@AdvertisementID,Discount=@Discount,DiscountDescription=@DiscountDescription,DistributionMin=@DistributionMin,DistributionMax=@DistributionMax
                ,DistributionPercent=@DistributionPercent,ReturnPoint=@ReturnPoint,AccountPeriod=@AccountPeriod,ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID,DistributionDescription=@DistributionDescription");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@AdvertisementID", SqlDbType.Int,4),
                    
					new SqlParameter("@Discount", SqlDbType.Decimal,8),
					new SqlParameter("@DiscountDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@DistributionMin", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionMax", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionPercent", SqlDbType.Decimal,8),
					new SqlParameter("@ReturnPoint", SqlDbType.Decimal,8),
					new SqlParameter("@AccountPeriod", SqlDbType.NVarChar,50),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,6),
					new SqlParameter("@id", SqlDbType.Int,6),
					new SqlParameter("@DistributionDescription", SqlDbType.NVarChar,500)};

            parameters[0].Value = model.AdvertisementID;
            parameters[1].Value = model.Discount;
            parameters[2].Value = model.DiscountDescription;
            parameters[3].Value = model.DistributionMin;
            parameters[4].Value = model.DistributionMax;
            parameters[5].Value = model.DistributionPercent;
            parameters[6].Value = model.ReturnPoint;
            parameters[7].Value = model.AccountPeriod;
            parameters[8].Value = DateTime.Now;
            parameters[9].Value = model.ModifiedUserID;
            parameters[10].Value = model.ID;
            parameters[11].Value = model.DistributionDescription;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_AdvertisementDetails ");
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
        public AdvertisementDetailsInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_AdvertisementDetails ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            AdvertisementDetailsInfo model = new AdvertisementDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdvertisementID"].ToString() != "")
                {
                    model.AdvertisementID = int.Parse(ds.Tables[0].Rows[0]["AdvertisementID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Discount"].ToString() != "")
                {
                    model.Discount = decimal.Parse(ds.Tables[0].Rows[0]["Discount"].ToString());
                }
                model.DiscountDescription = ds.Tables[0].Rows[0]["DiscountDescription"].ToString();
                model.DistributionDescription = ds.Tables[0].Rows[0]["DistributionDescription"].ToString();

                if (ds.Tables[0].Rows[0]["DistributionMin"].ToString() != "")
                {
                    model.DistributionMin = decimal.Parse(ds.Tables[0].Rows[0]["DistributionMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DistributionMax"].ToString() != "")
                {
                    model.DistributionMax = decimal.Parse(ds.Tables[0].Rows[0]["DistributionMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DistributionPercent"].ToString() != "")
                {
                    model.DistributionPercent = decimal.Parse(ds.Tables[0].Rows[0]["DistributionPercent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReturnPoint"].ToString() != "")
                {
                    model.ReturnPoint = decimal.Parse(ds.Tables[0].Rows[0]["ReturnPoint"].ToString());
                }
                model.AccountPeriod = ds.Tables[0].Rows[0]["AccountPeriod"].ToString();

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
            strSql.Append(" FROM T_AdvertisementDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}
