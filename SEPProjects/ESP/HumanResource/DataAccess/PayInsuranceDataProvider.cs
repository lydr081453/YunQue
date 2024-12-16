using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;

namespace ESP.HumanResource.DataAccess
{
    public class PayInsuranceDataProvider
    {
        public PayInsuranceDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sep_PayInsurance");
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
		public int Add(PayInsuranceInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sep_PayInsurance(");
			strSql.Append("UserID,EndowmentInsurance,UnemploymentInsurance,MedicalInsurance,PublicReserveFunds,PayYear,PayMonth,Remark,Creator,CreateTime,LastUpdateMan,LastUpdateTime)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@EndowmentInsurance,@UnemploymentInsurance,@MedicalInsurance,@PublicReserveFunds,@PayYear,@PayMonth,@Remark,@Creator,@CreateTime,@LastUpdateMan,@LastUpdateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@EndowmentInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@UnemploymentInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@MedicalInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@PublicReserveFunds", SqlDbType.NVarChar,200),
					new SqlParameter("@PayYear", SqlDbType.Int,4),
					new SqlParameter("@PayMonth", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateMan", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.EndowmentInsurance;
			parameters[2].Value = model.UnemploymentInsurance;
			parameters[3].Value = model.MedicalInsurance;
			parameters[4].Value = model.PublicReserveFunds;
			parameters[5].Value = model.PayYear;
			parameters[6].Value = model.PayMonth;
			parameters[7].Value = model.Remark;
			parameters[8].Value = model.Creator;
			parameters[9].Value = model.CreateTime;
			parameters[10].Value = model.LastUpdateMan;
			parameters[11].Value = model.LastUpdateTime;

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
        public int Add(PayInsuranceInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_PayInsurance(");
            strSql.Append("UserID,EndowmentInsurance,UnemploymentInsurance,MedicalInsurance,PublicReserveFunds,PayYear,PayMonth,Remark,Creator,CreateTime,LastUpdateMan,LastUpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@EndowmentInsurance,@UnemploymentInsurance,@MedicalInsurance,@PublicReserveFunds,@PayYear,@PayMonth,@Remark,@Creator,@CreateTime,@LastUpdateMan,@LastUpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@EndowmentInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@UnemploymentInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@MedicalInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@PublicReserveFunds", SqlDbType.NVarChar,200),
					new SqlParameter("@PayYear", SqlDbType.Int,4),
					new SqlParameter("@PayMonth", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateMan", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.EndowmentInsurance;
            parameters[2].Value = model.UnemploymentInsurance;
            parameters[3].Value = model.MedicalInsurance;
            parameters[4].Value = model.PublicReserveFunds;
            parameters[5].Value = model.PayYear;
            parameters[6].Value = model.PayMonth;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.LastUpdateMan;
            parameters[11].Value = model.LastUpdateTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
		public void Update(PayInsuranceInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sep_PayInsurance set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("EndowmentInsurance=@EndowmentInsurance,");
			strSql.Append("UnemploymentInsurance=@UnemploymentInsurance,");
			strSql.Append("MedicalInsurance=@MedicalInsurance,");
			strSql.Append("PublicReserveFunds=@PublicReserveFunds,");
			strSql.Append("PayYear=@PayYear,");
			strSql.Append("PayMonth=@PayMonth,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("LastUpdateMan=@LastUpdateMan,");
			strSql.Append("LastUpdateTime=@LastUpdateTime");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@EndowmentInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@UnemploymentInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@MedicalInsurance", SqlDbType.NVarChar,200),
					new SqlParameter("@PublicReserveFunds", SqlDbType.NVarChar,200),
					new SqlParameter("@PayYear", SqlDbType.Int,4),
					new SqlParameter("@PayMonth", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateMan", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.EndowmentInsurance;
			parameters[3].Value = model.UnemploymentInsurance;
			parameters[4].Value = model.MedicalInsurance;
			parameters[5].Value = model.PublicReserveFunds;
			parameters[6].Value = model.PayYear;
			parameters[7].Value = model.PayMonth;
			parameters[8].Value = model.Remark;
			parameters[9].Value = model.Creator;
			parameters[10].Value = model.CreateTime;
			parameters[11].Value = model.LastUpdateMan;
			parameters[12].Value = model.LastUpdateTime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete sep_PayInsurance ");
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
		public PayInsuranceInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select u.lastnamecn+u.firstnamecn as FullNameCn,p.* from sep_PayInsurance p join sep_users u on p.userid=u.userid  ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			PayInsuranceInfo model=new PayInsuranceInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.ID=ID;
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
                model.FullNameCn = ds.Tables[0].Rows[0]["FullNameCn"].ToString();
				model.EndowmentInsurance=(ds.Tables[0].Rows[0]["EndowmentInsurance"].ToString());
				
				if(ds.Tables[0].Rows[0]["UnemploymentInsurance"].ToString()!="")
				{
					model.UnemploymentInsurance=(ds.Tables[0].Rows[0]["UnemploymentInsurance"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MedicalInsurance"].ToString()!="")
				{
					model.MedicalInsurance=(ds.Tables[0].Rows[0]["MedicalInsurance"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PublicReserveFunds"].ToString()!="")
				{
					model.PublicReserveFunds=(ds.Tables[0].Rows[0]["PublicReserveFunds"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PayYear"].ToString()!="")
				{
					model.PayYear=int.Parse(ds.Tables[0].Rows[0]["PayYear"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PayMonth"].ToString()!="")
				{
					model.PayMonth=int.Parse(ds.Tables[0].Rows[0]["PayMonth"].ToString());
				}
				model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				if(ds.Tables[0].Rows[0]["Creator"].ToString()!="")
				{
					model.Creator=int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastUpdateMan"].ToString()!="")
				{
					model.LastUpdateMan=int.Parse(ds.Tables[0].Rows[0]["LastUpdateMan"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString()!="")
				{
					model.LastUpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
				}
				return model;
			}
			else
			{
			return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PayInsuranceInfo GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u.lastnamecn+u.firstnamecn as FullNameCn,p.* from sep_users u join sep_PayInsurance p on u.userid=p.userid  ");            
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            PayInsuranceInfo model = new PayInsuranceInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());            
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
                model.FullNameCn = ds.Tables[0].Rows[0]["FullNameCn"].ToString();
                if (ds.Tables[0].Rows[0]["EndowmentInsurance"].ToString() != "")
                {
                    model.EndowmentInsurance = (ds.Tables[0].Rows[0]["EndowmentInsurance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnemploymentInsurance"].ToString() != "")
                {
                    model.UnemploymentInsurance = (ds.Tables[0].Rows[0]["UnemploymentInsurance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MedicalInsurance"].ToString() != "")
                {
                    model.MedicalInsurance = (ds.Tables[0].Rows[0]["MedicalInsurance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PublicReserveFunds"].ToString() != "")
                {
                    model.PublicReserveFunds = (ds.Tables[0].Rows[0]["PublicReserveFunds"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayYear"].ToString() != "")
                {
                    model.PayYear = int.Parse(ds.Tables[0].Rows[0]["PayYear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayMonth"].ToString() != "")
                {
                    model.PayMonth = int.Parse(ds.Tables[0].Rows[0]["PayMonth"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateMan"].ToString() != "")
                {
                    model.LastUpdateMan = int.Parse(ds.Tables[0].Rows[0]["LastUpdateMan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
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
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select u.lastnamecn+u.firstnamecn as FullNameCn,p.* from sep_users u join sep_PayInsurance p on u.userid=p.userid ");
			
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
	
    }
}
