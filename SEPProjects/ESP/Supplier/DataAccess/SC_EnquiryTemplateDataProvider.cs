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
    public class SC_EnquiryTemplateDataProvider
    {
        public SC_EnquiryTemplateDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SC_EnquiryTemplate");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_EnquiryTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_EnquiryTemplate(");
            strSql.Append("[Name],[Xml],[TypeID],[UserID],[CreateTime],[UpdateTime],[IsDelete],[MessageId]");
            strSql.Append(") values (");
            strSql.Append("@Name,@Xml,@TypeID,@UserID,@CreateTime,@UpdateTime,@IsDelete,@MessageId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Xml", SqlDbType.NText),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@MessageId", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Xml;
            parameters[2].Value = model.TypeID;
            parameters[3].Value = model.UserID;
            parameters[4].Value = model.CreateTime.ToString();
            parameters[5].Value = model.UpdateTime.ToString();
            parameters[6].Value = model.IsDelete;
            parameters[7].Value = model.MessageId;

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
        public void Update(SC_EnquiryTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_EnquiryTemplate set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Xml=@Xml,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("IsDelete=@IsDelete,");
            strSql.Append("MessageId=@MessageId");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Xml", SqlDbType.NText),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@MessageId", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Xml;
            parameters[3].Value = model.TypeID;
            parameters[4].Value = model.UserID;
            parameters[5].Value = model.CreateTime.ToString();
            parameters[6].Value = model.UpdateTime.ToString();
            parameters[7].Value = model.IsDelete;
            parameters[8].Value = model.MessageId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_EnquiryTemplate ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_EnquiryTemplate GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from SC_EnquiryTemplate ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SC_EnquiryTemplate model = new SC_EnquiryTemplate();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Xml = ds.Tables[0].Rows[0]["Xml"].ToString();
                model.TypeID = int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                model.MessageId = int.Parse(ds.Tables[0].Rows[0]["MessageId"].ToString());

                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }

                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
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
            strSql.Append(" FROM SC_EnquiryTemplate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion  成员方法
    }
}
