using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using ESP.Media.Common;
using System.Data;
using System.Text;
using ESP.Media.Entity;

/*
 * 
 * 机构
 * 
 */
namespace ESP.Media.DataAccess
{
    public class AgencyDataProvider
    {
        public AgencyDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AgencyID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Media_Agency");
			strSql.Append(" where AgencyID= @AgencyID");
			SqlParameter[] parameters = {
					new SqlParameter("@AgencyID", SqlDbType.Int,4)
				};
			parameters[0].Value = AgencyID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AgencyInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Media_Agency(");
            strSql.Append("AgencyCName,AgencyEName,CShortName,EShortName,AgencyType,Status,CreatedByUserID,CreatedDate,LastModifiedByUserID,LastModifiedDate,MediumSort,ReaderSort,GoverningBody,FrontFor,ResponsiblePerson,Contacter,TelephoneExchange,Fax,AddressOne,AddressTwo,WebAddress,Cooperate,PhoneOne,PhoneTwo,AgencyLogo,AgencyIntro,EngIntro,Remarks,TopicName,TopicProperty,OverrideRange,ChannelWebAddress,countryid,provinceid,cityid,del,MediaType,addr1_provinceid,Agency,addr1_countryid,addr2_provinceid,addr2_cityid,addr2_countryid,PostCode,MediaID,RegionAttribute)");
			strSql.Append(" values (");
            strSql.Append("@AgencyCName,@AgencyEName,@CShortName,@EShortName,@AgencyType,@Status,@CreatedByUserID,@CreatedDate,@LastModifiedByUserID,@LastModifiedDate,@MediumSort,@ReaderSort,@GoverningBody,@FrontFor,@ResponsiblePerson,@Contacter,@TelephoneExchange,@Fax,@AddressOne,@AddressTwo,@WebAddress,@Cooperate,@PhoneOne,@PhoneTwo,@AgencyLogo,@AgencyIntro,@EngIntro,@Remarks,@TopicName,@TopicProperty,@OverrideRange,@ChannelWebAddress,@countryid,@provinceid,@cityid,@del,@MediaType,@addr1_provinceid,@Agency,@addr1_countryid,@addr2_provinceid,@addr2_cityid,@addr2_countryid,@PostCode,@MediaID,@RegionAttribute)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@AgencyCName", SqlDbType.NVarChar),
					new SqlParameter("@AgencyEName", SqlDbType.NVarChar),
					new SqlParameter("@CShortName", SqlDbType.NVarChar),
					new SqlParameter("@EShortName", SqlDbType.NVarChar),
					new SqlParameter("@AgencyType", SqlDbType.SmallInt,2),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedByUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastModifiedByUserID", SqlDbType.Int,4),
					new SqlParameter("@LastModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@MediumSort", SqlDbType.NVarChar),
					new SqlParameter("@ReaderSort", SqlDbType.NVarChar),
					new SqlParameter("@GoverningBody", SqlDbType.NVarChar),
					new SqlParameter("@FrontFor", SqlDbType.NVarChar),
					new SqlParameter("@ResponsiblePerson", SqlDbType.NVarChar),
					new SqlParameter("@Contacter", SqlDbType.NVarChar),
					new SqlParameter("@TelephoneExchange", SqlDbType.NVarChar),
					new SqlParameter("@Fax", SqlDbType.NVarChar),
					new SqlParameter("@AddressOne", SqlDbType.NVarChar),
					new SqlParameter("@AddressTwo", SqlDbType.NVarChar),
					new SqlParameter("@WebAddress", SqlDbType.NVarChar),
					new SqlParameter("@Cooperate", SqlDbType.NVarChar),
					new SqlParameter("@PhoneOne", SqlDbType.NVarChar),
					new SqlParameter("@PhoneTwo", SqlDbType.NVarChar),
					new SqlParameter("@AgencyLogo", SqlDbType.NVarChar),
					new SqlParameter("@AgencyIntro", SqlDbType.NVarChar),
					new SqlParameter("@EngIntro", SqlDbType.NVarChar),
					new SqlParameter("@Remarks", SqlDbType.NVarChar),
					new SqlParameter("@TopicName", SqlDbType.NVarChar),
					new SqlParameter("@TopicProperty", SqlDbType.Int,4),
					new SqlParameter("@OverrideRange", SqlDbType.NVarChar),
					new SqlParameter("@ChannelWebAddress", SqlDbType.NVarChar),
					new SqlParameter("@countryid", SqlDbType.Int,4),
					new SqlParameter("@provinceid", SqlDbType.Int,4),
					new SqlParameter("@cityid", SqlDbType.Int,4),
					new SqlParameter("@del", SqlDbType.SmallInt,2),
					new SqlParameter("@MediaType", SqlDbType.NVarChar),
					new SqlParameter("@addr1_provinceid", SqlDbType.Int,4),
					new SqlParameter("@Agency", SqlDbType.Int,4),
					new SqlParameter("@addr1_countryid", SqlDbType.Int,4),
					new SqlParameter("@addr2_provinceid", SqlDbType.Int,4),
					new SqlParameter("@addr2_cityid", SqlDbType.Int,4),
					new SqlParameter("@addr2_countryid", SqlDbType.Int,4),
					new SqlParameter("@PostCode", SqlDbType.NVarChar),
					new SqlParameter("@MediaID", SqlDbType.Int,4),
                    new SqlParameter("@RegionAttribute",SqlDbType.Int,4)};
			parameters[0].Value = model.AgencyCName;
			parameters[1].Value = model.AgencyEName;
			parameters[2].Value = model.CShortName;
			parameters[3].Value = model.EShortName;
			parameters[4].Value = model.AgencyType;
			parameters[5].Value = model.Status;
			parameters[6].Value = model.CreatedByUserID;
			parameters[7].Value = model.CreatedDate;
			parameters[8].Value = model.LastModifiedByUserID;
			parameters[9].Value = model.LastModifiedDate;
			parameters[10].Value = model.MediumSort;
			parameters[11].Value = model.ReaderSort;
			parameters[12].Value = model.GoverningBody;
			parameters[13].Value = model.FrontFor;
			parameters[14].Value = model.ResponsiblePerson;
			parameters[15].Value = model.Contacter;
			parameters[16].Value = model.TelephoneExchange;
			parameters[17].Value = model.Fax;
			parameters[18].Value = model.AddressOne;
			parameters[19].Value = model.AddressTwo;
			parameters[20].Value = model.WebAddress;
			parameters[21].Value = model.Cooperate;
			parameters[22].Value = model.PhoneOne;
			parameters[23].Value = model.PhoneTwo;
			parameters[24].Value = model.AgencyLogo;
			parameters[25].Value = model.AgencyIntro;
			parameters[26].Value = model.EngIntro;
			parameters[27].Value = model.Remarks;
			parameters[28].Value = model.TopicName;
			parameters[29].Value = model.TopicProperty;
			parameters[30].Value = model.OverrideRange;
			parameters[31].Value = model.ChannelWebAddress;
			parameters[32].Value = model.countryid;
			parameters[33].Value = model.provinceid;
			parameters[34].Value = model.cityid;
			parameters[35].Value = model.del;
			parameters[36].Value = model.MediaType;
			parameters[37].Value = model.addr1_provinceid;
			parameters[38].Value = model.Agency;
			parameters[39].Value = model.addr1_countryid;
			parameters[40].Value = model.addr2_provinceid;
			parameters[41].Value = model.addr2_cityid;
			parameters[42].Value = model.addr2_countryid;
			parameters[43].Value = model.PostCode;
			parameters[44].Value = model.MediaID;
            parameters[45].Value = model.RegionAttribute;

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
		public int Update(AgencyInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Media_Agency set ");
			strSql.Append("AgencyCName=@AgencyCName,");
			strSql.Append("AgencyEName=@AgencyEName,");
			strSql.Append("CShortName=@CShortName,");
			strSql.Append("EShortName=@EShortName,");
			strSql.Append("AgencyType=@AgencyType,");
			strSql.Append("Status=@Status,");
			strSql.Append("CreatedByUserID=@CreatedByUserID,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("LastModifiedByUserID=@LastModifiedByUserID,");
			strSql.Append("LastModifiedDate=@LastModifiedDate,");
			strSql.Append("MediumSort=@MediumSort,");
			strSql.Append("ReaderSort=@ReaderSort,");
			strSql.Append("GoverningBody=@GoverningBody,");
			strSql.Append("FrontFor=@FrontFor,");
			strSql.Append("ResponsiblePerson=@ResponsiblePerson,");
			strSql.Append("Contacter=@Contacter,");
			strSql.Append("TelephoneExchange=@TelephoneExchange,");
			strSql.Append("Fax=@Fax,");
			strSql.Append("AddressOne=@AddressOne,");
			strSql.Append("AddressTwo=@AddressTwo,");
			strSql.Append("WebAddress=@WebAddress,");
			strSql.Append("Cooperate=@Cooperate,");
			strSql.Append("PhoneOne=@PhoneOne,");
			strSql.Append("PhoneTwo=@PhoneTwo,");
			strSql.Append("AgencyLogo=@AgencyLogo,");
			strSql.Append("AgencyIntro=@AgencyIntro,");
			strSql.Append("EngIntro=@EngIntro,");
			strSql.Append("Remarks=@Remarks,");
			strSql.Append("TopicName=@TopicName,");
			strSql.Append("TopicProperty=@TopicProperty,");
			strSql.Append("OverrideRange=@OverrideRange,");
			strSql.Append("ChannelWebAddress=@ChannelWebAddress,");
			strSql.Append("countryid=@countryid,");
			strSql.Append("provinceid=@provinceid,");
			strSql.Append("cityid=@cityid,");
			strSql.Append("del=@del,");
			strSql.Append("MediaType=@MediaType,");
			strSql.Append("addr1_provinceid=@addr1_provinceid,");
			strSql.Append("Agency=@Agency,");
			strSql.Append("addr1_countryid=@addr1_countryid,");
			strSql.Append("addr2_provinceid=@addr2_provinceid,");
			strSql.Append("addr2_cityid=@addr2_cityid,");
			strSql.Append("addr2_countryid=@addr2_countryid,");
			strSql.Append("PostCode=@PostCode,");
			strSql.Append("MediaID=@MediaID,");
            strSql.Append("RegionAttribute=@RegionAttribute");
			strSql.Append(" where AgencyID=@AgencyID");
			SqlParameter[] parameters = {
					new SqlParameter("@AgencyID", SqlDbType.Int,4),
					new SqlParameter("@AgencyCName", SqlDbType.NVarChar),
					new SqlParameter("@AgencyEName", SqlDbType.NVarChar),
					new SqlParameter("@CShortName", SqlDbType.NVarChar),
					new SqlParameter("@EShortName", SqlDbType.NVarChar),
					new SqlParameter("@AgencyType", SqlDbType.SmallInt,2),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedByUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@LastModifiedByUserID", SqlDbType.Int,4),
					new SqlParameter("@LastModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@MediumSort", SqlDbType.NVarChar),
					new SqlParameter("@ReaderSort", SqlDbType.NVarChar),
					new SqlParameter("@GoverningBody", SqlDbType.NVarChar),
					new SqlParameter("@FrontFor", SqlDbType.NVarChar),
					new SqlParameter("@ResponsiblePerson", SqlDbType.NVarChar),
					new SqlParameter("@Contacter", SqlDbType.NVarChar),
					new SqlParameter("@TelephoneExchange", SqlDbType.NVarChar),
					new SqlParameter("@Fax", SqlDbType.NVarChar),
					new SqlParameter("@AddressOne", SqlDbType.NVarChar),
					new SqlParameter("@AddressTwo", SqlDbType.NVarChar),
					new SqlParameter("@WebAddress", SqlDbType.NVarChar),
					new SqlParameter("@Cooperate", SqlDbType.NVarChar),
					new SqlParameter("@PhoneOne", SqlDbType.NVarChar),
					new SqlParameter("@PhoneTwo", SqlDbType.NVarChar),
					new SqlParameter("@AgencyLogo", SqlDbType.NVarChar),
					new SqlParameter("@AgencyIntro", SqlDbType.NVarChar),
					new SqlParameter("@EngIntro", SqlDbType.NVarChar),
					new SqlParameter("@Remarks", SqlDbType.NVarChar),
					new SqlParameter("@TopicName", SqlDbType.NVarChar),
					new SqlParameter("@TopicProperty", SqlDbType.Int,4),
					new SqlParameter("@OverrideRange", SqlDbType.NVarChar),
					new SqlParameter("@ChannelWebAddress", SqlDbType.NVarChar),
					new SqlParameter("@countryid", SqlDbType.Int,4),
					new SqlParameter("@provinceid", SqlDbType.Int,4),
					new SqlParameter("@cityid", SqlDbType.Int,4),
					new SqlParameter("@del", SqlDbType.SmallInt,2),
					new SqlParameter("@MediaType", SqlDbType.NVarChar),
					new SqlParameter("@addr1_provinceid", SqlDbType.Int,4),
					new SqlParameter("@Agency", SqlDbType.Int,4),
					new SqlParameter("@addr1_countryid", SqlDbType.Int,4),
					new SqlParameter("@addr2_provinceid", SqlDbType.Int,4),
					new SqlParameter("@addr2_cityid", SqlDbType.Int,4),
					new SqlParameter("@addr2_countryid", SqlDbType.Int,4),
					new SqlParameter("@PostCode", SqlDbType.NVarChar),
					new SqlParameter("@MediaID", SqlDbType.Int,4),
                    new SqlParameter("@RegionAttribute",SqlDbType.Int,4)  };
			parameters[0].Value = model.AgencyID;
			parameters[1].Value = model.AgencyCName;
			parameters[2].Value = model.AgencyEName;
			parameters[3].Value = model.CShortName;
			parameters[4].Value = model.EShortName;
			parameters[5].Value = model.AgencyType;
			parameters[6].Value = model.Status;
			parameters[7].Value = model.CreatedByUserID;
			parameters[8].Value = model.CreatedDate;
			parameters[9].Value = model.LastModifiedByUserID;
			parameters[10].Value = model.LastModifiedDate;
			parameters[11].Value = model.MediumSort;
			parameters[12].Value = model.ReaderSort;
			parameters[13].Value = model.GoverningBody;
			parameters[14].Value = model.FrontFor;
			parameters[15].Value = model.ResponsiblePerson;
			parameters[16].Value = model.Contacter;
			parameters[17].Value = model.TelephoneExchange;
			parameters[18].Value = model.Fax;
			parameters[19].Value = model.AddressOne;
			parameters[20].Value = model.AddressTwo;
			parameters[21].Value = model.WebAddress;
			parameters[22].Value = model.Cooperate;
			parameters[23].Value = model.PhoneOne;
			parameters[24].Value = model.PhoneTwo;
			parameters[25].Value = model.AgencyLogo;
			parameters[26].Value = model.AgencyIntro;
			parameters[27].Value = model.EngIntro;
			parameters[28].Value = model.Remarks;
			parameters[29].Value = model.TopicName;
			parameters[30].Value = model.TopicProperty;
			parameters[31].Value = model.OverrideRange;
			parameters[32].Value = model.ChannelWebAddress;
			parameters[33].Value = model.countryid;
			parameters[34].Value = model.provinceid;
			parameters[35].Value = model.cityid;
			parameters[36].Value = model.del;
			parameters[37].Value = model.MediaType;
			parameters[38].Value = model.addr1_provinceid;
			parameters[39].Value = model.Agency;
			parameters[40].Value = model.addr1_countryid;
			parameters[41].Value = model.addr2_provinceid;
			parameters[42].Value = model.addr2_cityid;
			parameters[43].Value = model.addr2_countryid;
			parameters[44].Value = model.PostCode;
			parameters[45].Value = model.MediaID;
            parameters[46].Value = model.RegionAttribute;

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
		public void Delete(int AgencyID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete Media_Agency ");
			strSql.Append(" where AgencyID=@AgencyID");
			SqlParameter[] parameters = {
					new SqlParameter("@AgencyID", SqlDbType.Int,4)
				};
			parameters[0].Value = AgencyID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public AgencyInfo GetModel(int AgencyID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from Media_Agency ");
			strSql.Append(" where AgencyID=@AgencyID");
			SqlParameter[] parameters = {
					new SqlParameter("@AgencyID", SqlDbType.Int,4)};
			parameters[0].Value = AgencyID;
			AgencyInfo model=new AgencyInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.AgencyID=AgencyID;
			if(ds.Tables[0].Rows.Count>0)
			{
				model.AgencyCName=ds.Tables[0].Rows[0]["AgencyCName"].ToString();
				model.AgencyEName=ds.Tables[0].Rows[0]["AgencyEName"].ToString();
				model.CShortName=ds.Tables[0].Rows[0]["CShortName"].ToString();
				model.EShortName=ds.Tables[0].Rows[0]["EShortName"].ToString();
				if(ds.Tables[0].Rows[0]["AgencyType"].ToString()!="")
				{
					model.AgencyType=int.Parse(ds.Tables[0].Rows[0]["AgencyType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedByUserID"].ToString()!="")
				{
					model.CreatedByUserID=int.Parse(ds.Tables[0].Rows[0]["CreatedByUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastModifiedByUserID"].ToString()!="")
				{
					model.LastModifiedByUserID=int.Parse(ds.Tables[0].Rows[0]["LastModifiedByUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastModifiedDate"].ToString()!="")
				{
					model.LastModifiedDate=DateTime.Parse(ds.Tables[0].Rows[0]["LastModifiedDate"].ToString());
				}
				model.MediumSort=ds.Tables[0].Rows[0]["MediumSort"].ToString();
				model.ReaderSort=ds.Tables[0].Rows[0]["ReaderSort"].ToString();
				model.GoverningBody=ds.Tables[0].Rows[0]["GoverningBody"].ToString();
				model.FrontFor=ds.Tables[0].Rows[0]["FrontFor"].ToString();
				model.ResponsiblePerson=ds.Tables[0].Rows[0]["ResponsiblePerson"].ToString();
				model.Contacter=ds.Tables[0].Rows[0]["Contacter"].ToString();
				model.TelephoneExchange=ds.Tables[0].Rows[0]["TelephoneExchange"].ToString();
				model.Fax=ds.Tables[0].Rows[0]["Fax"].ToString();
				model.AddressOne=ds.Tables[0].Rows[0]["AddressOne"].ToString();
				model.AddressTwo=ds.Tables[0].Rows[0]["AddressTwo"].ToString();
				model.WebAddress=ds.Tables[0].Rows[0]["WebAddress"].ToString();
				model.Cooperate=ds.Tables[0].Rows[0]["Cooperate"].ToString();
				model.PhoneOne=ds.Tables[0].Rows[0]["PhoneOne"].ToString();
				model.PhoneTwo=ds.Tables[0].Rows[0]["PhoneTwo"].ToString();
				model.AgencyLogo=ds.Tables[0].Rows[0]["AgencyLogo"].ToString();
				model.AgencyIntro=ds.Tables[0].Rows[0]["AgencyIntro"].ToString();
				model.EngIntro=ds.Tables[0].Rows[0]["EngIntro"].ToString();
				model.Remarks=ds.Tables[0].Rows[0]["Remarks"].ToString();
				model.TopicName=ds.Tables[0].Rows[0]["TopicName"].ToString();
				if(ds.Tables[0].Rows[0]["TopicProperty"].ToString()!="")
				{
					model.TopicProperty=int.Parse(ds.Tables[0].Rows[0]["TopicProperty"].ToString());
				}
				model.OverrideRange=ds.Tables[0].Rows[0]["OverrideRange"].ToString();
				model.ChannelWebAddress=ds.Tables[0].Rows[0]["ChannelWebAddress"].ToString();
				if(ds.Tables[0].Rows[0]["countryid"].ToString()!="")
				{
					model.countryid=int.Parse(ds.Tables[0].Rows[0]["countryid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["provinceid"].ToString()!="")
				{
					model.provinceid=int.Parse(ds.Tables[0].Rows[0]["provinceid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["cityid"].ToString()!="")
				{
					model.cityid=int.Parse(ds.Tables[0].Rows[0]["cityid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["del"].ToString()!="")
				{
					model.del=int.Parse(ds.Tables[0].Rows[0]["del"].ToString());
				}
				model.MediaType=ds.Tables[0].Rows[0]["MediaType"].ToString();
				if(ds.Tables[0].Rows[0]["addr1_provinceid"].ToString()!="")
				{
					model.addr1_provinceid=int.Parse(ds.Tables[0].Rows[0]["addr1_provinceid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Agency"].ToString()!="")
				{
					model.Agency=int.Parse(ds.Tables[0].Rows[0]["Agency"].ToString());
				}
				if(ds.Tables[0].Rows[0]["addr1_countryid"].ToString()!="")
				{
					model.addr1_countryid=int.Parse(ds.Tables[0].Rows[0]["addr1_countryid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["addr2_provinceid"].ToString()!="")
				{
					model.addr2_provinceid=int.Parse(ds.Tables[0].Rows[0]["addr2_provinceid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["addr2_cityid"].ToString()!="")
				{
					model.addr2_cityid=int.Parse(ds.Tables[0].Rows[0]["addr2_cityid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["addr2_countryid"].ToString()!="")
				{
					model.addr2_countryid=int.Parse(ds.Tables[0].Rows[0]["addr2_countryid"].ToString());
				}
				model.PostCode=ds.Tables[0].Rows[0]["PostCode"].ToString();
				if(ds.Tables[0].Rows[0]["MediaID"].ToString()!="")
				{
					model.MediaID=int.Parse(ds.Tables[0].Rows[0]["MediaID"].ToString());
				}
                if (ds.Tables[0].Rows[0]["RegionAttribute"].ToString() != "")
                {
                    model.RegionAttribute = int.Parse(ds.Tables[0].Rows[0]["RegionAttribute"].ToString());
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
            strSql.Append("select [AgencyID],[AgencyCName],[AgencyEName],[CShortName],[EShortName],[AgencyType],[Status],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate],[MediumSort],[ReaderSort],[GoverningBody],[FrontFor],[ResponsiblePerson],[Contacter],[TelephoneExchange],[Fax],[AddressOne],[AddressTwo],[WebAddress],[Cooperate],[PhoneOne],[PhoneTwo],[AgencyLogo],[AgencyIntro],[EngIntro],[Remarks],[TopicName],[TopicProperty],[OverrideRange],[ChannelWebAddress],[countryid],[provinceid],[cityid],[del],[MediaType],[addr1_provinceid],[Agency],[addr1_countryid],[addr2_provinceid],[addr2_cityid],[addr2_countryid],[PostCode],[MediaID],[RegionAttribute] ");
			strSql.Append(" FROM Media_Agency ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


		#endregion  成员方法
    }
}
