using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.DataAccess
{
    public class MatterReasonDataProvider
    {
        public MatterReasonDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_MatterReason");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_MatterReason");
            strSql.Append(" where ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Administrative.Entity.MatterReasonInfo model)
        {
            //model.ID=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_MatterReason(");
            strSql.Append("SingleOverTimeID,Details,CreatedDate,StartDate,EndDate)");
            strSql.Append(" values (");
            strSql.Append("@SingleOverTimeID,@Details,@CreatedDate,@StartDate,@EndDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@SingleOverTimeID", SqlDbType.Int,4),
					new SqlParameter("@Details", SqlDbType.NVarChar,800),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime)};
            parameters[0].Value = model.SingleOverTimeID;
            parameters[1].Value = model.Details;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.StartDate;
            parameters[4].Value = model.EndDate;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return model.ID;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.MatterReasonInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_MatterReason set ");
            strSql.Append("SingleOverTimeID=@SingleOverTimeID,");
            strSql.Append("Details=@Details,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@SingleOverTimeID", SqlDbType.Int,4),
					new SqlParameter("@Details", SqlDbType.NVarChar,800),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.SingleOverTimeID;
            parameters[2].Value = model.Details;
            parameters[3].Value = model.StartDate;
            parameters[4].Value = model.EndDate;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_MatterReason ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.MatterReasonInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_MatterReason ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            ESP.Administrative.Entity.MatterReasonInfo model = new ESP.Administrative.Entity.MatterReasonInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = ID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
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
        public IList<ESP.Administrative.Entity.MatterReasonInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_MatterReason ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ESP.Administrative.Entity.MatterReasonInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
        #endregion  成员方法
    }
}