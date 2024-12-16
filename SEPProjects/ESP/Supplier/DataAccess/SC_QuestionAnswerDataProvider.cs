using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;

namespace ESP.Supplier.DataAccess
{
    public class SC_QuestionAnswerDataProvider
    {
        public SC_QuestionAnswerDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_QuestionAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_QuestionAnswer(");
            strSql.Append("SupplierId,QuestionId,QuestionNum,AnswerContent,AnswerLength,AttachContent,CreatTime,CreatIP,LastUpdateTime,LastUpdateIP,Type,Status");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("" + model.SupplierId + ",");
            strSql.Append("" + model.QuestionId + ",");
            strSql.Append("'" + model.QuestionNum + "',");
            strSql.Append("'" + model.AnswerContent + "',");
            strSql.Append("" + model.AnswerLength + ",");
            strSql.Append("'" + model.AttachContent + "',");
            strSql.Append("'" + model.CreatTime + "',");
            strSql.Append("'" + model.CreatIP + "',");
            strSql.Append("'" + model.LastUpdateTime + "',");
            strSql.Append("'" + model.LastUpdateIP + "',");
            strSql.Append("" + model.Type + ",");
            strSql.Append("" + model.Status + "");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        public void Update(SC_QuestionAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_QuestionAnswer set ");
            strSql.Append("SupplierId=" + model.SupplierId + ",");
            strSql.Append("QuestionId=" + model.QuestionId + ",");
            strSql.Append("QuestionNum='" + model.QuestionNum + "',");
            strSql.Append("AnswerContent='" + model.AnswerContent + "',");
            strSql.Append("AnswerLength=" + model.AnswerLength + ",");
            strSql.Append("AttachContent='" + model.AttachContent + "',");
            strSql.Append("CreatTime='" + model.CreatTime + "',");
            strSql.Append("CreatIP='" + model.CreatIP + "',");
            strSql.Append("LastUpdateTime='" + model.LastUpdateTime + "',");
            strSql.Append("LastUpdateIP='" + model.LastUpdateIP + "',");
            strSql.Append("Type=" + model.Type + ",");
            strSql.Append("Status=" + model.Status + "");
            strSql.Append(" where Id=" + model.Id + "");
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_QuestionAnswer ");
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_QuestionAnswer GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append(" [Id],[SupplierId],[QuestionId],[QuestionNum],[AnswerContent],[AnswerLength],[AttachContent],[CreatTime],[CreatIP],[LastUpdateTime],[LastUpdateIP],[Type],[Status] ");
            strSql.Append(" from SC_QuestionAnswer ");
            strSql.Append(" where Id=" + Id);
            SC_QuestionAnswer model = new SC_QuestionAnswer();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            model.Id = Id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QuestionId"].ToString() != "")
                {
                    model.QuestionId = int.Parse(ds.Tables[0].Rows[0]["QuestionId"].ToString());
                }
                model.QuestionNum = ds.Tables[0].Rows[0]["QuestionNum"].ToString();
                model.AnswerContent = ds.Tables[0].Rows[0]["AnswerContent"].ToString();
                if (ds.Tables[0].Rows[0]["AnswerLength"].ToString() != "")
                {
                    model.AnswerLength = int.Parse(ds.Tables[0].Rows[0]["AnswerLength"].ToString());
                }
                model.AttachContent = ds.Tables[0].Rows[0]["AttachContent"].ToString();
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                model.CreatIP = ds.Tables[0].Rows[0]["CreatIP"].ToString();
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
                }
                model.LastUpdateIP = ds.Tables[0].Rows[0]["LastUpdateIP"].ToString();
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
            strSql.Append("select [Id],[SupplierId],[QuestionId],[QuestionNum],[AnswerContent],[AnswerLength],[AttachContent],[CreatTime],[CreatIP],[LastUpdateTime],[LastUpdateIP],[Type],[Status] ");
            strSql.Append(" FROM SC_QuestionAnswer ");
            if (strWhere.Trim() != "")
            {

                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public static List<SC_QuestionAnswer> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_QuestionAnswer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_QuestionAnswer>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public static List<SC_QuestionAnswer> GetListBySupplierId(int sid)
        {
            string strWhere = string.Empty;
            strWhere += " SupplierId=@SupplierId";
            SqlParameter[] parameters = { new SqlParameter("@SupplierId", SqlDbType.Int, 4) };
            parameters[0].Value = sid;

            return GetList(strWhere, parameters);
        }
        #endregion  成员方法
    }
}
