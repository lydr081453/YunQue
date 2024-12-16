using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
    public class OtherMediumForOrderDataProvider
    {
        public OtherMediumForOrderDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(OtherMediumForOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OtherMediumForOrder(");
            strSql.Append("OrderID,MediaID,MediaName,CustomerName,Title,OfSpace,WordsCount,PicSize,LayoutSize,Color,Price,Unit,Amount,Discount,CreatedDate,CreatedUserID,ModifiedDate,ModifiedUserID,OtherFees,IsAccessories,Description,MediaArea,MediaDescription,MediaShunYaDescription)");
            strSql.Append(" values (");
            strSql.Append("@OrderID,@MediaID,@MediaName,@CustomerName,@Title,@OfSpace,@WordsCount,@PicSize,@LayoutSize,@Color,@Price,@Unit,@Amount,@Discount,@CreatedDate,@CreatedUserID,@ModifiedDate,@ModifiedUserID,@OtherFees,@IsAccessories,@Description,@MediaArea,@MediaDescription,@MediaShunYaDescription)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,8),
					new SqlParameter("@MediaID", SqlDbType.Int,8),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,500),
					new SqlParameter("@Title", SqlDbType.NVarChar,500),
					new SqlParameter("@OfSpace", SqlDbType.NVarChar,500),
					new SqlParameter("@WordsCount", SqlDbType.NVarChar,500),
					new SqlParameter("@PicSize", SqlDbType.NVarChar,500),
					new SqlParameter("@LayoutSize", SqlDbType.NVarChar,500),
					new SqlParameter("@Color", SqlDbType.NVarChar,500),
					new SqlParameter("@Price", SqlDbType.NVarChar,500),
					new SqlParameter("@Unit", SqlDbType.NVarChar,500),
					new SqlParameter("@Amount", SqlDbType.NVarChar,500),
					new SqlParameter("@Discount", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,8),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,8),
					new SqlParameter("@OtherFees", SqlDbType.NVarChar,500),
					new SqlParameter("@IsAccessories", SqlDbType.Bit),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@MediaArea", SqlDbType.NVarChar,500),
					new SqlParameter("@MediaDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@MediaShunYaDescription", SqlDbType.NVarChar,500)
                   // ,new SqlParameter("@StartDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.MediaID;
            parameters[2].Value = model.MediaName;
            parameters[3].Value = model.CustomerName;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.OfSpace;
            parameters[6].Value = model.WordsCount;
            parameters[7].Value = model.PicSize;
            parameters[8].Value = model.LayoutSize;
            parameters[9].Value = model.Color;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.Unit;
            parameters[12].Value = model.Amount;
            parameters[13].Value = model.Discount;
            parameters[14].Value = DateTime.Now;
            parameters[15].Value = model.CreatedUserID;
            parameters[16].Value = DateTime.Now;
            parameters[17].Value = model.ModifiedUserID;
            parameters[18].Value = model.OtherFees;
            parameters[19].Value = model.IsAccessories;
            parameters[20].Value = model.Description;
            //parameters[21].Value = model.StartDate;

            parameters[21].Value = model.MediaArea;
            parameters[22].Value = model.MediaDescription;
            parameters[23].Value = model.MediaShunYaDescription;
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
        public int Update(OtherMediumForOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OtherMediumForOrder set ");
            strSql.Append(@"OrderID = @OrderID,MediaID = @MediaID,[MediaName] = @MediaName,CustomerName = @CustomerName,Title = @Title,OfSpace = @OfSpace,StartDate = @StartDate,WordsCount = @WordsCount
                ,PicSize = @PicSize,LayoutSize = @LayoutSize,Color = @Color,Price = @Price,Unit = @Unit,Amount = @Amount,Discount = @Discount,CreatedDate = @CreatedDate,CreatedUserID = @CreatedUserID
                ,ModifiedDate = @ModifiedDate,ModifiedUserID = @ModifiedUserID,OtherFees = @OtherFees,IsAccessories = @IsAccessories,[Description] = @Description
                ,MediaArea = @MediaArea,MediaDescription = @MediaDescription,MediaShunYaDescription = @MediaShunYaDescription");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,8),
					new SqlParameter("@MediaID", SqlDbType.Int,8),
					new SqlParameter("@MediaName", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerName", SqlDbType.NVarChar,500),
					new SqlParameter("@Title", SqlDbType.NVarChar,500),
					new SqlParameter("@OfSpace", SqlDbType.NVarChar,500),
					new SqlParameter("@WordsCount", SqlDbType.NVarChar,500),
					new SqlParameter("@PicSize", SqlDbType.NVarChar,500),
					new SqlParameter("@LayoutSize", SqlDbType.NVarChar,500),
					new SqlParameter("@Color", SqlDbType.NVarChar,500),
					new SqlParameter("@Price", SqlDbType.NVarChar,500),
					new SqlParameter("@Unit", SqlDbType.NVarChar,500),
					new SqlParameter("@Amount", SqlDbType.NVarChar,500),
					new SqlParameter("@Discount", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,8),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int,8),
					new SqlParameter("@OtherFees", SqlDbType.NVarChar,500),
					new SqlParameter("@IsAccessories", SqlDbType.Bit),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,6),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@MediaArea", SqlDbType.NVarChar,500),
					new SqlParameter("@MediaDescription", SqlDbType.NVarChar,500),
					new SqlParameter("@MediaShunYaDescription", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.MediaID;
            parameters[2].Value = model.MediaName;
            parameters[3].Value = model.CustomerName;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.OfSpace;
            parameters[6].Value = model.WordsCount;
            parameters[7].Value = model.PicSize;
            parameters[8].Value = model.LayoutSize;
            parameters[9].Value = model.Color;
            parameters[10].Value = model.Price;
            parameters[11].Value = model.Unit;
            parameters[12].Value = model.Amount;
            parameters[13].Value = model.Discount;
            parameters[14].Value = model.CreatedDate;
            parameters[15].Value = model.CreatedUserID;
            parameters[16].Value = DateTime.Now;
            parameters[17].Value = model.ModifiedUserID;
            parameters[18].Value = model.OtherFees;
            parameters[19].Value = model.IsAccessories;
            parameters[20].Value = model.Description;
            parameters[21].Value = model.ID;
            parameters[22].Value = model.StartDate;

            parameters[23].Value = model.MediaArea;
            parameters[24].Value = model.MediaDescription;
            parameters[25].Value = model.MediaShunYaDescription;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OtherMediumForOrder ");
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
        public OtherMediumForOrderInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_OtherMediumForOrder ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            OtherMediumForOrderInfo model = new OtherMediumForOrderInfo();
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
                if (ds.Tables[0].Rows[0]["MediaID"].ToString() != "")
                {
                    model.MediaID = int.Parse(ds.Tables[0].Rows[0]["MediaID"].ToString());
                }
                model.MediaName = ds.Tables[0].Rows[0]["MediaName"].ToString();
                model.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();

                model.OfSpace = ds.Tables[0].Rows[0]["OfSpace"].ToString();
                if (ds.Tables[0].Rows[0]["WordsCount"].ToString() != "")
                {
                    model.WordsCount = Convert.ToDecimal(ds.Tables[0].Rows[0]["WordsCount"]);
                }
                model.PicSize = ds.Tables[0].Rows[0]["PicSize"].ToString();
                model.LayoutSize = ds.Tables[0].Rows[0]["LayoutSize"].ToString();
                model.Color = ds.Tables[0].Rows[0]["Color"].ToString();
                model.Price = ds.Tables[0].Rows[0]["Price"].ToString();
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                model.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                model.OtherFees = ds.Tables[0].Rows[0]["OtherFees"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                model.IsAccessories = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAccessories"]);
                if (ds.Tables[0].Rows[0]["StartDate"].ToString() != "")
                {
                    model.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]);
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
                model.MediaArea = ds.Tables[0].Rows[0]["MediaArea"].ToString();
                model.MediaDescription = ds.Tables[0].Rows[0]["MediaDescription"].ToString();
                model.MediaShunYaDescription = ds.Tables[0].Rows[0]["MediaShunYaDescription"].ToString();
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
            strSql.Append(" FROM T_OtherMediumForOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        private OtherMediumForOrderInfo SetModel(DataRow dr)
        {
            OtherMediumForOrderInfo model = new OtherMediumForOrderInfo();

            if (dr["ID"].ToString() != "")
            {
                model.ID = int.Parse(dr["ID"].ToString());
            }
            if (dr["OrderID"].ToString() != "")
            {
                model.OrderID = int.Parse(dr["OrderID"].ToString());
            }
            if (dr["MediaID"].ToString() != "")
            {
                model.MediaID = int.Parse(dr["MediaID"].ToString());
            }
            model.MediaName = dr["MediaName"].ToString();
            model.CustomerName = dr["CustomerName"].ToString();
            model.Title = dr["Title"].ToString();

            model.OfSpace = dr["OfSpace"].ToString();
            if (dr["WordsCount"].ToString() != "")
            {
                model.WordsCount = Convert.ToDecimal(dr["WordsCount"]);
            }
            model.PicSize = dr["PicSize"].ToString();
            model.LayoutSize = dr["LayoutSize"].ToString();
            model.Color = dr["Color"].ToString();
            model.Price = dr["Price"].ToString();
            model.Unit = dr["Unit"].ToString();
            model.Amount = dr["Amount"].ToString();
            model.Discount = dr["Discount"].ToString();
            model.OtherFees = dr["OtherFees"].ToString();
            model.Description = dr["Description"].ToString();
            model.IsAccessories = Convert.ToBoolean(dr["IsAccessories"]);
            if (dr["StartDate"].ToString() != "")
            {
                model.StartDate = Convert.ToDateTime(dr["StartDate"]);
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
            model.MediaArea = dr["MediaArea"].ToString();
            model.MediaDescription = dr["MediaDescription"].ToString();
            model.MediaShunYaDescription = dr["MediaShunYaDescription"].ToString();
            return model;
        }

        public IList<OtherMediumForOrderInfo> GetInfoList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            IList<OtherMediumForOrderInfo> list = new List<OtherMediumForOrderInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(SetModel(dr));
                }
            }
            return list;
        }
        #endregion  成员方法
    }
}