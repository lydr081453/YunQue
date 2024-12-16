using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Collections.Generic;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class TemporaryMeritPayDataProvider
    {
        public TemporaryMeritPayDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ESP_TemporaryMeritPay");
			strSql.Append(" where ID= @ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TemporaryMeritPayInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ESP_TemporaryMeritPay(");
			strSql.Append("UserID,Code,MeritPay,ImplementYear,ImplementMonth,Creator,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@Code,@MeritPay,@ImplementYear,@ImplementMonth,@Creator,@CreateDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar),
					new SqlParameter("@MeritPay", SqlDbType.NVarChar),
					new SqlParameter("@ImplementYear", SqlDbType.Int,4),
					new SqlParameter("@ImplementMonth", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.Code;
			parameters[2].Value = model.MeritPay;
			parameters[3].Value = model.ImplementYear;
			parameters[4].Value = model.ImplementMonth;
			parameters[5].Value = model.Creator;
			parameters[6].Value = model.CreateDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
        /// 增加一条数据
        /// </summary>
        public int Add(TemporaryMeritPayInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ESP_TemporaryMeritPay(");
            strSql.Append("UserID,Code,MeritPay,ImplementYear,ImplementMonth,Creator,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Code,@MeritPay,@ImplementYear,@ImplementMonth,@Creator,@CreateDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar),
					new SqlParameter("@MeritPay", SqlDbType.NVarChar),
					new SqlParameter("@ImplementYear", SqlDbType.Int,4),
					new SqlParameter("@ImplementMonth", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.MeritPay;
            parameters[3].Value = model.ImplementYear;
            parameters[4].Value = model.ImplementMonth;
            parameters[5].Value = model.Creator;
            parameters[6].Value = model.CreateDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
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
		public void Update(TemporaryMeritPayInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ESP_TemporaryMeritPay set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("Code=@Code,");
			strSql.Append("MeritPay=@MeritPay,");
			strSql.Append("ImplementYear=@ImplementYear,");
			strSql.Append("ImplementMonth=@ImplementMonth,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar),
					new SqlParameter("@MeritPay", SqlDbType.NVarChar),
					new SqlParameter("@ImplementYear", SqlDbType.Int,4),
					new SqlParameter("@ImplementMonth", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.Code;
			parameters[3].Value = model.MeritPay;
			parameters[4].Value = model.ImplementYear;
			parameters[5].Value = model.ImplementMonth;
			parameters[6].Value = model.Creator;
			parameters[7].Value = model.CreateDate;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TemporaryMeritPayInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ESP_TemporaryMeritPay set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Code=@Code,");
            strSql.Append("MeritPay=@MeritPay,");
            strSql.Append("ImplementYear=@ImplementYear,");
            strSql.Append("ImplementMonth=@ImplementMonth,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar),
					new SqlParameter("@MeritPay", SqlDbType.NVarChar),
					new SqlParameter("@ImplementYear", SqlDbType.Int,4),
					new SqlParameter("@ImplementMonth", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Code;
            parameters[3].Value = model.MeritPay;
            parameters[4].Value = model.ImplementYear;
            parameters[5].Value = model.ImplementMonth;
            parameters[6].Value = model.Creator;
            parameters[7].Value = model.CreateDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
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
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete ESP_TemporaryMeritPay ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public TemporaryMeritPayInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from ESP_TemporaryMeritPay ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			TemporaryMeritPayInfo model=new TemporaryMeritPayInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);			
            return GetData(ds,model);
			
		}

        /// <summary>
        /// 得到绩效变动
        /// </summary>
        public string GetMeritPay(int UserID, int Year, int Month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MeritPay from ESP_TemporaryMeritPay ");
            strSql.Append(" where UserID=@UserID");            
            strSql.Append(" and ImplementYear=@ImplementYear");
            strSql.Append(" and ImplementMonth=@ImplementMonth");            
            SqlParameter[] parameters = {					
					new SqlParameter("@UserID", SqlDbType.Int,4),					
					new SqlParameter("@ImplementYear", SqlDbType.Int,4),
					new SqlParameter("@ImplementMonth", SqlDbType.Int,4)
					};            
            parameters[0].Value = UserID;           
            parameters[1].Value = Year;
            parameters[2].Value = Month;

            TemporaryMeritPayInfo model = new TemporaryMeritPayInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["MeritPay"].ToString();
            }
            else
            {
                return ESP.Salary.Utility.DESEncrypt.Encode("0");
            }

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TemporaryMeritPayInfo GetModel(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ESP_TemporaryMeritPay ");
            strSql.Append(" where Code=@Code");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,128)};
            parameters[0].Value = Code;
            TemporaryMeritPayInfo model = new TemporaryMeritPayInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);            
            return GetData(ds, model);

        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select [ID],[UserID],[Code],[MeritPay],[ImplementYear],[ImplementMonth],[Creator],[CreateDate] ");
			strSql.Append(" FROM ESP_TemporaryMeritPay ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        private TemporaryMeritPayInfo GetData(DataSet ds,TemporaryMeritPayInfo model)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                model.MeritPay = ds.Tables[0].Rows[0]["MeritPay"].ToString();
                if (ds.Tables[0].Rows[0]["ImplementYear"].ToString() != "")
                {
                    model.ImplementYear = int.Parse(ds.Tables[0].Rows[0]["ImplementYear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ImplementMonth"].ToString() != "")
                {
                    model.ImplementMonth = int.Parse(ds.Tables[0].Rows[0]["ImplementMonth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            } 
        }



		#endregion  成员方法
    }
}
