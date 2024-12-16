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
    public class SC_EnquiryListDataProvider
    {
        public SC_EnquiryListDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SC_EnquiryList");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_EnquiryList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_EnquiryList(");
            strSql.Append("[TemplateID],[TypeID],[UserID],[MessageID],[Note],[CreateTime],[PEID]");
            strSql.Append(") values (");
            strSql.Append("@TemplateID,@TypeID,@UserID,@MessageID,@Note,@CreateTime,@PEID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TemplateID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@MessageID", SqlDbType.Int,4),
                    new SqlParameter("@Note", SqlDbType.NText),
					new SqlParameter("@CreateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@PEID", SqlDbType.Int,4)};
            parameters[0].Value = model.TemplateID;
            parameters[1].Value = model.TypeID;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.MessageId;
            parameters[4].Value = model.Note;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.PEID;

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
        public void Update(SC_EnquiryList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_EnquiryList set ");
            strSql.Append("TemplateID=@TemplateID,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("MessageId=@MessageId,");
            strSql.Append("Note=@Note,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("PEID=@PEID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TemplateID", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@MessageId", SqlDbType.Int,4),
					new SqlParameter("@Note", SqlDbType.NText),
					new SqlParameter("@CreateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@PEID", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TemplateID;
            parameters[2].Value = model.TypeID;
            parameters[3].Value = model.UserID;
            parameters[4].Value = model.MessageId;
            parameters[5].Value = model.Note;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.PEID;
  
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_EnquiryList ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据TemplateId删除数据
        /// </summary>
        /// <param name="TemplateId"></param>
        public void DeleteByTemplateID(int TemplateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_EnquiryList ");
            strSql.Append(" where TemplateId=@TemplateId ");
            SqlParameter[] parameters = {
					new SqlParameter("@TemplateId", SqlDbType.Int,4)};
            parameters[0].Value = TemplateId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_EnquiryList GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from SC_EnquiryList ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SC_EnquiryList model = new SC_EnquiryList();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                model.TemplateID = int.Parse(ds.Tables[0].Rows[0]["TemplateID"].ToString());
                model.TypeID = int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                model.MessageId = int.Parse(ds.Tables[0].Rows[0]["MessageId"].ToString());
                model.Note = ds.Tables[0].Rows[0]["Note"].ToString();
                model.PEID = int.Parse(ds.Tables[0].Rows[0]["PEID"].ToString());
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
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
            strSql.Append(" FROM SC_EnquiryList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion  成员方法
    }
}
