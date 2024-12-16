using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Administrative.DataAccess
{
    public class DisciplineDataProvider
    {
        public DisciplineDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_Discipline");
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
        public int Add(DisciplineInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_Discipline(");
            strSql.Append("UserID,UserName,BeginTime,EndTime,DisciplineLength,DisciplineTime,Type,IsNoNeed,OperatorID,OperaterTime)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserName,@BeginTime,@EndTime,@DisciplineLength,@DisciplineTime,@Type,@IsNoNeed,@OperatorID,@OperaterTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@DisciplineLength", SqlDbType.Decimal,9),
					new SqlParameter("@DisciplineTime", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsNoNeed", SqlDbType.Bit,1),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperaterTime", SqlDbType.DateTime)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.BeginTime;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.DisciplineLength;
            parameters[5].Value = model.DisciplineTime;
            parameters[6].Value = model.Type;
            parameters[7].Value = model.IsNoNeed;
            parameters[8].Value = model.OperatorID;
            parameters[9].Value = model.OperaterTime;

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
        public void Update(DisciplineInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_Discipline set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("DisciplineLength=@DisciplineLength,");
            strSql.Append("DisciplineTime=@DisciplineTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("IsNoNeed=@IsNoNeed,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperaterTime=@OperaterTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@DisciplineLength", SqlDbType.Decimal,9),
					new SqlParameter("@DisciplineTime", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsNoNeed", SqlDbType.Bit,1),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperaterTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.BeginTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.DisciplineLength;
            parameters[6].Value = model.DisciplineTime;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.IsNoNeed;
            parameters[9].Value = model.OperatorID;
            parameters[10].Value = model.OperaterTime;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_Discipline ");
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
        public DisciplineInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_Discipline ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            DisciplineInfo model = new DisciplineInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = ID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["BeginTime"].ToString() != "")
                {
                    model.BeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["BeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DisciplineLength"].ToString() != "")
                {
                    model.DisciplineLength = decimal.Parse(ds.Tables[0].Rows[0]["DisciplineLength"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DisciplineTime"].ToString() != "")
                {
                    model.DisciplineTime = DateTime.Parse(ds.Tables[0].Rows[0]["DisciplineTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsNoNeed"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsNoNeed"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsNoNeed"].ToString().ToLower() == "true"))
                    {
                        model.IsNoNeed = true;
                    }
                    else
                    {
                        model.IsNoNeed = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["OperatorID"].ToString() != "")
                {
                    model.OperatorID = int.Parse(ds.Tables[0].Rows[0]["OperatorID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OperaterTime"].ToString() != "")
                {
                    model.OperaterTime = DateTime.Parse(ds.Tables[0].Rows[0]["OperaterTime"].ToString());
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
            strSql.Append("select [ID],[UserID],[UserName],[BeginTime],[EndTime],[DisciplineLength],[DisciplineTime],[Type],[IsNoNeed],[OperatorID],[OperaterTime] ");
            strSql.Append(" FROM AD_Discipline ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}