using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类MessageDataProvider。
    /// </summary>
    public class MessageDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDataProvider"/> class.
        /// </summary>
        public  MessageDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Add(MessageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Message(");
            strSql.Append("subject,body,createrid,createtime,lasttime,areaid,attFile)");
            strSql.Append(" values (");
            strSql.Append("@subject,@body,@createrid,@createtime,@lasttime,@areaid,@attFile)");
            SqlParameter[] parameters = {					
                    new SqlParameter("@subject",SqlDbType.NVarChar,50),
                    new SqlParameter("@body",SqlDbType.NVarChar,4000),
                    new SqlParameter("@createrid",SqlDbType.Int,4),
                    new SqlParameter("@createtime",SqlDbType.DateTime),
                    new SqlParameter("@lasttime",SqlDbType.DateTime),
                    new SqlParameter("@areaid",SqlDbType.Int,4),
                    new SqlParameter("@attFile",SqlDbType.VarChar,100)
                                        };            
            parameters[0].Value = model.subject;
            parameters[1].Value = model.body;
            parameters[2].Value = model.createrid;
            parameters[3].Value = model.createtime;
            parameters[4].Value = model.lasttime;
            parameters[5].Value = model.areaid;
            parameters[6].Value = model.attFile;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(MessageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Message set ");
            strSql.Append("subject=@subject,body=@body,createrid=@createrid,createtime=@createtime,lasttime=@lasttime,areaid=@areaid,attFile=@attFile");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@subject",SqlDbType.NVarChar,50),
                    new SqlParameter("@body",SqlDbType.NVarChar,4000),
                    new SqlParameter("@createrid",SqlDbType.Int,4),
                    new SqlParameter("@createtime",SqlDbType.DateTime),
                    new SqlParameter("@lasttime",SqlDbType.DateTime),
                    new SqlParameter("@areaid",SqlDbType.Int,4),
                    new SqlParameter("@attFile",SqlDbType.VarChar,100)
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.subject;
            parameters[2].Value = model.body;
            parameters[3].Value = model.createrid;
            parameters[4].Value = model.createtime;
            parameters[5].Value = model.lasttime;
            parameters[6].Value = model.areaid;
            parameters[7].Value = model.attFile;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Message ");
            strSql.Append(" where id=@id ");
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
        public static MessageInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Message ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            MessageInfo model = new MessageInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createrid"].ToString() != "")
                {
                    model.createrid = int.Parse(ds.Tables[0].Rows[0]["createrid"].ToString());
                    model.creatername = ESP.Compatible.Employee.GetName(ds.Tables[0].Rows[0]["createrid"].ToString());
                }
                model.subject = ds.Tables[0].Rows[0]["subject"].ToString();
                model.body = ds.Tables[0].Rows[0]["body"].ToString();
                model.createtime = DateTime.Parse(ds.Tables[0].Rows[0]["createtime"].ToString());
                model.lasttime = DateTime.Parse(ds.Tables[0].Rows[0]["lasttime"].ToString());
                if (ds.Tables[0].Rows[0]["areaid"].ToString() != "")
                {
                    model.areaid = int.Parse(ds.Tables[0].Rows[0]["areaid"].ToString());
                }
                model.attFile = ds.Tables[0].Rows[0]["attFile"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["attFile"].ToString();
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
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Message ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="num">The num.</param>
        /// <returns></returns>
        public static DataSet GetList(int num,int areaid)
        {
            string str = "";
            if(num > 0)
                str = string.Format("select top {0} * from T_Message where areaid={1} order by areaid asc,lasttime desc",num.ToString(),areaid.ToString());
            else
                str = string.Format("select * from T_Message where areaid={0} order by areaid asc,lasttime desc", areaid.ToString());
            return DbHelperSQL.Query(str);
        }


        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public static List<MessageInfo> GetList()
        {
            List<MessageInfo> list = new List<MessageInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Message ");
            
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (r.Read())
                {
                    MessageInfo m = new MessageInfo();
                    m.PopupData(r);
                    list.Add(m);
                }
                r.Close();
            }
            return list;
        }
        #endregion  成员方法
    }
}