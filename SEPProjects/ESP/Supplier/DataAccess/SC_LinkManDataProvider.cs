using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_LinkManDataProvider
    {
        public SC_LinkManDataProvider()
        {}

        #region  成员方法

        public int Add(SC_LinkMan model)
        {
            return Add(model, null);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_LinkMan model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_LinkMan(");
            strSql.Append("SupplierId,Name,SN,Birthday,Title,Sex,Tel,Fax,Mobile,Address,ZIP,Email,QQ,MSN,Icon,Note,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status,CompanyName)");
            strSql.Append(" values (");
            strSql.Append("@SupplierId,@Name,@SN,@Birthday,@Title,@Sex,@Tel,@Fax,@Mobile,@Address,@ZIP,@Email,@QQ,@MSN,@Icon,@Note,@CreatedIP,@CreatTime,@LastModifiedIP,@LastUpdateTime,@Type,@Status,@CompanyName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar),
					new SqlParameter("@SN", SqlDbType.NVarChar),
					new SqlParameter("@Birthday", SqlDbType.SmallDateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@Tel", SqlDbType.NVarChar),
					new SqlParameter("@Fax", SqlDbType.NChar),
					new SqlParameter("@Mobile", SqlDbType.NVarChar),
					new SqlParameter("@Address", SqlDbType.NVarChar),
					new SqlParameter("@ZIP", SqlDbType.NVarChar),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@QQ", SqlDbType.NVarChar),
					new SqlParameter("@MSN", SqlDbType.NVarChar),
					new SqlParameter("@Icon", SqlDbType.NVarChar),
					new SqlParameter("@Note", SqlDbType.Text),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@CompanyName",SqlDbType.NVarChar)                                        
                                        };
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.SN;
            parameters[3].Value = model.Birthday;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.Tel;
            parameters[7].Value = model.Fax;
            parameters[8].Value = model.Mobile;
            parameters[9].Value = model.Address;
            parameters[10].Value = model.ZIP;
            parameters[11].Value = model.Email;
            parameters[12].Value = model.QQ;
            parameters[13].Value = model.MSN;
            parameters[14].Value = model.Icon;
            parameters[15].Value = model.Note;
            parameters[16].Value = model.CreatedIP;
            parameters[17].Value = model.CreatTime;
            parameters[18].Value = model.LastModifiedIP;
            parameters[19].Value = model.LastUpdateTime;
            parameters[20].Value = model.Type;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.CompanyName;

            object obj = null;
            if (trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public void Update(SC_LinkMan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_LinkMan set ");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("Name=@Name,");
            strSql.Append("SN=@SN,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("Title=@Title,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Fax=@Fax,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("Address=@Address,");
            strSql.Append("ZIP=@ZIP,");
            strSql.Append("Email=@Email,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("MSN=@MSN,");
            strSql.Append("Icon=@Icon,");
            strSql.Append("Note=@Note,");
            strSql.Append("CreatedIP=@CreatedIP,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("LastModifiedIP=@LastModifiedIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status");
            strSql.Append(" where LinkerId=@LinkerId");
            SqlParameter[] parameters = {
					new SqlParameter("@LinkerId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar),
					new SqlParameter("@SN", SqlDbType.NVarChar),
					new SqlParameter("@Birthday", SqlDbType.SmallDateTime),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@Tel", SqlDbType.NVarChar),
					new SqlParameter("@Fax", SqlDbType.NChar),
					new SqlParameter("@Mobile", SqlDbType.NVarChar),
					new SqlParameter("@Address", SqlDbType.NVarChar),
					new SqlParameter("@ZIP", SqlDbType.NVarChar),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@QQ", SqlDbType.NVarChar),
					new SqlParameter("@MSN", SqlDbType.NVarChar),
					new SqlParameter("@Icon", SqlDbType.NVarChar),
					new SqlParameter("@Note", SqlDbType.Text),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.LinkerId;
            parameters[1].Value = model.SupplierId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.SN;
            parameters[4].Value = model.Birthday;
            parameters[5].Value = model.Title;
            parameters[6].Value = model.Sex;
            parameters[7].Value = model.Tel;
            parameters[8].Value = model.Fax;
            parameters[9].Value = model.Mobile;
            parameters[10].Value = model.Address;
            parameters[11].Value = model.ZIP;
            parameters[12].Value = model.Email;
            parameters[13].Value = model.QQ;
            parameters[14].Value = model.MSN;
            parameters[15].Value = model.Icon;
            parameters[16].Value = model.Note;
            parameters[17].Value = model.CreatedIP;
            parameters[18].Value = model.CreatTime;
            parameters[19].Value = model.LastModifiedIP;
            parameters[20].Value = model.LastUpdateTime;
            parameters[21].Value = model.Type;
            parameters[22].Value = model.Status;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int LinkerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_LinkMan ");
            strSql.Append(" where LinkerId=@LinkerId");
            SqlParameter[] parameters = {
					new SqlParameter("@LinkerId", SqlDbType.Int,4)
				};
            parameters[0].Value = LinkerId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_LinkMan GetModel(int LinkerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_LinkMan ");
            strSql.Append(" where LinkerId=@LinkerId");
            SqlParameter[] parameters = {
					new SqlParameter("@LinkerId", SqlDbType.Int,4)};
            parameters[0].Value = LinkerId;
            SC_LinkMan model = new SC_LinkMan();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.LinkerId = LinkerId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.SN = ds.Tables[0].Rows[0]["SN"].ToString();
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                if (ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
                }
                model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                model.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.ZIP = ds.Tables[0].Rows[0]["ZIP"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.MSN = ds.Tables[0].Rows[0]["MSN"].ToString();
                model.Icon = ds.Tables[0].Rows[0]["Icon"].ToString();
                model.Note = ds.Tables[0].Rows[0]["Note"].ToString();
                model.CreatedIP = ds.Tables[0].Rows[0]["CreatedIP"].ToString();
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                model.LastModifiedIP = ds.Tables[0].Rows[0]["LastModifiedIP"].ToString();
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_LinkMan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_LinkMan> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_LinkMan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_LinkMan>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        #endregion  成员方法
    }
}
