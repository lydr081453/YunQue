using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class SocialSecurityDataProvider
    {
        public SocialSecurityDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sep_SocialSecurityInfo");
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
		public int Add(SocialSecurityInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sep_SocialSecurityInfo(");
			strSql.Append("EIProportionOfFirms,EIProportionOfIndividuals,UIProportionOfFirms,UIProportionOfIndividuals,BIProportionOfFirms,BIProportionOfIndividuals,CIProportionOfFirms,CIProportionOfIndividuals,MIProportionOfFirms,MIProportionOfIndividuals,MIBigProportionOfIndividuals,PRFProportionOfFirms,PRFProportionOfIndividuals,BeginTime,EndTime,SocialInsuranceCompany,Creator,CreateTime,LastUpdateMan,lastUpdateTime)");
			strSql.Append(" values (");
			strSql.Append("@EIProportionOfFirms,@EIProportionOfIndividuals,@UIProportionOfFirms,@UIProportionOfIndividuals,@BIProportionOfFirms,@BIProportionOfIndividuals,@CIProportionOfFirms,@CIProportionOfIndividuals,@MIProportionOfFirms,@MIProportionOfIndividuals,@MIBigProportionOfIndividuals,@PRFProportionOfFirms,@PRFProportionOfIndividuals,@BeginTime,@EndTime,@SocialInsuranceCompany,@Creator,@CreateTime,@LastUpdateMan,@lastUpdateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@EIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@EIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIBigProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@SocialInsuranceCompany", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateMan", SqlDbType.Int,4),
					new SqlParameter("@lastUpdateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.EIProportionOfFirms;
			parameters[1].Value = model.EIProportionOfIndividuals;
			parameters[2].Value = model.UIProportionOfFirms;
			parameters[3].Value = model.UIProportionOfIndividuals;
			parameters[4].Value = model.BIProportionOfFirms;
			parameters[5].Value = model.BIProportionOfIndividuals;
			parameters[6].Value = model.CIProportionOfFirms;
			parameters[7].Value = model.CIProportionOfIndividuals;
			parameters[8].Value = model.MIProportionOfFirms;
			parameters[9].Value = model.MIProportionOfIndividuals;
			parameters[10].Value = model.MIBigProportionOfIndividuals;
			parameters[11].Value = model.PRFProportionOfFirms;
			parameters[12].Value = model.PRFProportionOfIndividuals;
			parameters[13].Value = model.BeginTime;
			parameters[14].Value = model.EndTime;
			parameters[15].Value = model.SocialInsuranceCompany;
			parameters[16].Value = model.Creator;
			parameters[17].Value = model.CreateTime;
			parameters[18].Value = model.LastUpdateMan;
			parameters[19].Value = model.lastUpdateTime;

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
		/// 更新一条数据
		/// </summary>
		public int Update(SocialSecurityInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sep_SocialSecurityInfo set ");
			strSql.Append("EIProportionOfFirms=@EIProportionOfFirms,");
			strSql.Append("EIProportionOfIndividuals=@EIProportionOfIndividuals,");
			strSql.Append("UIProportionOfFirms=@UIProportionOfFirms,");
			strSql.Append("UIProportionOfIndividuals=@UIProportionOfIndividuals,");
			strSql.Append("BIProportionOfFirms=@BIProportionOfFirms,");
			strSql.Append("BIProportionOfIndividuals=@BIProportionOfIndividuals,");
			strSql.Append("CIProportionOfFirms=@CIProportionOfFirms,");
			strSql.Append("CIProportionOfIndividuals=@CIProportionOfIndividuals,");
			strSql.Append("MIProportionOfFirms=@MIProportionOfFirms,");
			strSql.Append("MIProportionOfIndividuals=@MIProportionOfIndividuals,");
			strSql.Append("MIBigProportionOfIndividuals=@MIBigProportionOfIndividuals,");
			strSql.Append("PRFProportionOfFirms=@PRFProportionOfFirms,");
			strSql.Append("PRFProportionOfIndividuals=@PRFProportionOfIndividuals,");
			strSql.Append("BeginTime=@BeginTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("SocialInsuranceCompany=@SocialInsuranceCompany,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("LastUpdateMan=@LastUpdateMan,");
			strSql.Append("lastUpdateTime=@lastUpdateTime");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@EIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@EIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIBigProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@SocialInsuranceCompany", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateMan", SqlDbType.Int,4),
					new SqlParameter("@lastUpdateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.EIProportionOfFirms;
			parameters[2].Value = model.EIProportionOfIndividuals;
			parameters[3].Value = model.UIProportionOfFirms;
			parameters[4].Value = model.UIProportionOfIndividuals;
			parameters[5].Value = model.BIProportionOfFirms;
			parameters[6].Value = model.BIProportionOfIndividuals;
			parameters[7].Value = model.CIProportionOfFirms;
			parameters[8].Value = model.CIProportionOfIndividuals;
			parameters[9].Value = model.MIProportionOfFirms;
			parameters[10].Value = model.MIProportionOfIndividuals;
			parameters[11].Value = model.MIBigProportionOfIndividuals;
			parameters[12].Value = model.PRFProportionOfFirms;
			parameters[13].Value = model.PRFProportionOfIndividuals;
			parameters[14].Value = model.BeginTime;
			parameters[15].Value = model.EndTime;
			parameters[16].Value = model.SocialInsuranceCompany;
			parameters[17].Value = model.Creator;
			parameters[18].Value = model.CreateTime;
			parameters[19].Value = model.LastUpdateMan;
			parameters[20].Value = model.lastUpdateTime;

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
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete sep_SocialSecurityInfo ");
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
		public SocialSecurityInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from sep_SocialSecurityInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			SocialSecurityInfo model=new SocialSecurityInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.ID=ID;
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString()!="")
				{
					model.EIProportionOfFirms=decimal.Parse(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString()!="")
				{
					model.EIProportionOfIndividuals=decimal.Parse(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString()!="")
				{
					model.UIProportionOfFirms=decimal.Parse(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString()!="")
				{
					model.UIProportionOfIndividuals=decimal.Parse(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString()!="")
				{
					model.BIProportionOfFirms=decimal.Parse(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BIProportionOfIndividuals"].ToString()!="")
				{
					model.BIProportionOfIndividuals=decimal.Parse(ds.Tables[0].Rows[0]["BIProportionOfIndividuals"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString()!="")
				{
					model.CIProportionOfFirms=decimal.Parse(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CIProportionOfIndividuals"].ToString()!="")
				{
					model.CIProportionOfIndividuals=decimal.Parse(ds.Tables[0].Rows[0]["CIProportionOfIndividuals"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString()!="")
				{
					model.MIProportionOfFirms=decimal.Parse(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString()!="")
				{
					model.MIProportionOfIndividuals=decimal.Parse(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString()!="")
				{
					model.MIBigProportionOfIndividuals=decimal.Parse(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString()!="")
				{
					model.PRFProportionOfFirms=decimal.Parse(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PRFProportionOfIndividuals"].ToString()!="")
				{
					model.PRFProportionOfIndividuals=decimal.Parse(ds.Tables[0].Rows[0]["PRFProportionOfIndividuals"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BeginTime"].ToString()!="")
				{
					model.BeginTime=DateTime.Parse(ds.Tables[0].Rows[0]["BeginTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndTime"].ToString()!="")
				{
					model.EndTime=DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SocialInsuranceCompany"].ToString()!="")
				{
					model.SocialInsuranceCompany=int.Parse(ds.Tables[0].Rows[0]["SocialInsuranceCompany"].ToString());
				}
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
				if(ds.Tables[0].Rows[0]["lastUpdateTime"].ToString()!="")
				{
					model.lastUpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["lastUpdateTime"].ToString());
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
			strSql.Append("select [ID],[EIProportionOfFirms],[EIProportionOfIndividuals],[UIProportionOfFirms],[UIProportionOfIndividuals],[BIProportionOfFirms],[BIProportionOfIndividuals],[CIProportionOfFirms],[CIProportionOfIndividuals],[MIProportionOfFirms],[MIProportionOfIndividuals],[MIBigProportionOfIndividuals],[PRFProportionOfFirms],[PRFProportionOfIndividuals],[BeginTime],[EndTime],[SocialInsuranceCompany],[Creator],[CreateTime],[LastUpdateMan],[lastUpdateTime] ");
			strSql.Append(" FROM sep_SocialSecurityInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


		#endregion  成员方法
    }
}
