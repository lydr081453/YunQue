using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
    public class AdvertisementForOrderDataProvider
    {
        public AdvertisementForOrderDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(AdvertisementForOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_AdvertisementForOrder(");
            strSql.Append(@"OrderID, AdvertisementID, AdvertisementDetailsID, MediaType, MediaName, AdvertisementExemplar, PriceTotal, Discount, Total
                , DistributionPercent, DistributionPrice, ReturnPoint, AccountPeriod, CreatedDate, CreatedUserID, ModifiedDate, ModifiedUserID, IsDel)");
            strSql.Append(" values (");
            strSql.Append("@OrderID, @AdvertisementID, @AdvertisementDetailsID, @MediaType, @MediaName, @AdvertisementExemplar, @PriceTotal, @Discount, @Total, @DistributionPercent, @DistributionPrice, @ReturnPoint, @AccountPeriod, @CreatedDate, @CreatedUserID, @ModifiedDate, @ModifiedUserID, @IsDel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,8),
					new SqlParameter("@AdvertisementID", SqlDbType.Int,8),
					new SqlParameter("@AdvertisementDetailsID", SqlDbType.Int,8),                    
					new SqlParameter("@MediaType", SqlDbType.NVarChar,500),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@AdvertisementExemplar", SqlDbType.NVarChar,500),
					new SqlParameter("@PriceTotal", SqlDbType.Decimal,8),
					new SqlParameter("@Discount", SqlDbType.Decimal,8),
					new SqlParameter("@Total", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionPercent", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionPrice", SqlDbType.Decimal,8),
					new SqlParameter("@ReturnPoint", SqlDbType.Decimal,8),
					new SqlParameter("@AccountPeriod", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,8),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,8),
					new SqlParameter("@IsDel", SqlDbType.Bit)
                                        };
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.AdvertisementID;
            parameters[2].Value = model.AdvertisementDetailsID;
            parameters[3].Value = model.MediaType;
            parameters[4].Value = model.MediaName;
            parameters[5].Value = model.AdvertisementExemplar;
            parameters[6].Value = model.PriceTotal;
            parameters[7].Value = model.Discount;
            parameters[8].Value = model.Total;
            parameters[9].Value = model.DistributionPercent;
            parameters[10].Value = model.DistributionPrice;
            parameters[11].Value = model.ReturnPoint;
            parameters[12].Value = model.AccountPeriod;
            parameters[13].Value = DateTime.Now;
            parameters[14].Value = model.CreatedUserID;
            parameters[15].Value = DateTime.Now;
            parameters[16].Value = model.ModifiedUserID;
            parameters[17].Value = false;
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
        public int Update(AdvertisementForOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_AdvertisementForOrder set ");
            strSql.Append(@"OrderID=@OrderID, AdvertisementID=@AdvertisementID, AdvertisementDetailsID=@AdvertisementDetailsID, MediaType=@MediaType, MediaName=@MediaName
                , AdvertisementExemplar=@AdvertisementExemplar, PriceTotal=@PriceTotal, Discount=@Discount, Total=@Total, DistributionPercent=@DistributionPercent
                , DistributionPrice=@DistributionPrice, ReturnPoint=@ReturnPoint, AccountPeriod=@AccountPeriod, ModifiedDate=@ModifiedDate, ModifiedUserID=@ModifiedUserID, IsDel=@IsDel");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,8),
					new SqlParameter("@AdvertisementID", SqlDbType.Int,8),
					new SqlParameter("@AdvertisementDetailsID", SqlDbType.Int,8),                    
					new SqlParameter("@MediaType", SqlDbType.NVarChar,500),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@AdvertisementExemplar", SqlDbType.NVarChar,500),
					new SqlParameter("@PriceTotal", SqlDbType.Decimal,8),
					new SqlParameter("@Discount", SqlDbType.Decimal,8),
					new SqlParameter("@Total", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionPercent", SqlDbType.Decimal,8),
					new SqlParameter("@DistributionPrice", SqlDbType.Decimal,8),
					new SqlParameter("@ReturnPoint", SqlDbType.Decimal,8),
					new SqlParameter("@AccountPeriod", SqlDbType.NVarChar,500),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,8),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.AdvertisementID;
            parameters[2].Value = model.AdvertisementDetailsID;
            parameters[3].Value = model.MediaType;
            parameters[4].Value = model.MediaName;
            parameters[5].Value = model.AdvertisementExemplar;
            parameters[6].Value = model.PriceTotal;
            parameters[7].Value = model.Discount;
            parameters[8].Value = model.Total;
            parameters[9].Value = model.DistributionPercent;
            parameters[10].Value = model.DistributionPrice;
            parameters[11].Value = model.ReturnPoint;
            parameters[12].Value = model.AccountPeriod;
            parameters[13].Value = DateTime.Now;
            parameters[14].Value = model.ModifiedUserID;
            parameters[15].Value = false;
            parameters[16].Value = model.ID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_AdvertisementForOrder ");
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
        public AdvertisementForOrderInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_AdvertisementForOrder ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            AdvertisementForOrderInfo model = new AdvertisementForOrderInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdvertisementID"].ToString() != "")
                {
                    model.AdvertisementID = int.Parse(ds.Tables[0].Rows[0]["AdvertisementID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdvertisementDetailsID"].ToString() != "")
                {
                    model.AdvertisementDetailsID = int.Parse(ds.Tables[0].Rows[0]["AdvertisementDetailsID"].ToString());
                }
                model.MediaName = ds.Tables[0].Rows[0]["MediaName"].ToString();
                model.MediaType = ds.Tables[0].Rows[0]["MediaType"].ToString();
                model.AdvertisementExemplar = ds.Tables[0].Rows[0]["AdvertisementExemplar"].ToString();
                model.AccountPeriod = ds.Tables[0].Rows[0]["AccountPeriod"].ToString();

                if (ds.Tables[0].Rows[0]["PriceTotal"].ToString() != "")
                {
                    model.PriceTotal = decimal.Parse(ds.Tables[0].Rows[0]["PriceTotal"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Discount"].ToString() != "")
                {
                    model.Discount = decimal.Parse(ds.Tables[0].Rows[0]["Discount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Total"].ToString() != "")
                {
                    model.Total = decimal.Parse(ds.Tables[0].Rows[0]["Total"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DistributionPercent"].ToString() != "")
                {
                    model.DistributionPercent = decimal.Parse(ds.Tables[0].Rows[0]["DistributionPercent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DistributionPrice"].ToString() != "")
                {
                    model.DistributionPrice = decimal.Parse(ds.Tables[0].Rows[0]["DistributionPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReturnPoint"].ToString() != "")
                {
                    model.ReturnPoint = decimal.Parse(ds.Tables[0].Rows[0]["ReturnPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDel"].ToString() != "")
                {
                    model.IsDel = bool.Parse(ds.Tables[0].Rows[0]["IsDel"].ToString());
                }


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


        private AdvertisementForOrderInfo SetModel(DataRow dr)
        {
            AdvertisementForOrderInfo model = new AdvertisementForOrderInfo();


            if (dr["ID"].ToString() != "")
            {
                model.ID = int.Parse(dr["ID"].ToString());
            }
            if (dr["OrderID"].ToString() != "")
            {
                model.OrderID = int.Parse(dr["OrderID"].ToString());
            }
            if (dr["AdvertisementID"].ToString() != "")
            {
                model.AdvertisementID = int.Parse(dr["AdvertisementID"].ToString());
            }
            if (dr["AdvertisementDetailsID"].ToString() != "")
            {
                model.AdvertisementDetailsID = int.Parse(dr["AdvertisementDetailsID"].ToString());
            }
            model.MediaName = dr["MediaName"].ToString();
            model.MediaType = dr["MediaType"].ToString();
            model.AdvertisementExemplar = dr["AdvertisementExemplar"].ToString();
            model.AccountPeriod = dr["AccountPeriod"].ToString();

            if (dr["PriceTotal"].ToString() != "")
            {
                model.PriceTotal = decimal.Parse(dr["PriceTotal"].ToString());
            }
            if (dr["Discount"].ToString() != "")
            {
                model.Discount = decimal.Parse(dr["Discount"].ToString());
            }
            if (dr["Total"].ToString() != "")
            {
                model.Total = decimal.Parse(dr["Total"].ToString());
            }
            if (dr["DistributionPercent"].ToString() != "")
            {
                model.DistributionPercent = decimal.Parse(dr["DistributionPercent"].ToString());
            }
            if (dr["DistributionPrice"].ToString() != "")
            {
                model.DistributionPrice = decimal.Parse(dr["DistributionPrice"].ToString());
            }
            if (dr["ReturnPoint"].ToString() != "")
            {
                model.ReturnPoint = decimal.Parse(dr["ReturnPoint"].ToString());
            }
            if (dr["IsDel"].ToString() != "")
            {
                model.IsDel = bool.Parse(dr["IsDel"].ToString());
            }


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

        public IList<AdvertisementForOrderInfo> GetInfoList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            IList<AdvertisementForOrderInfo> list = new List<AdvertisementForOrderInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(SetModel(dr));
                }
            }
            return list;
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
            strSql.Append(" FROM T_AdvertisementForOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}