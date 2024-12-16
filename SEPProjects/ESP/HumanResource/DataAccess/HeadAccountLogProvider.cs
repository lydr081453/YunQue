using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Utilities;


namespace ESP.HumanResource.DataAccess
{
   public  class HeadAccountLogProvider
    {
       public HeadAccountLogProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Sep_HeadAccountLog");
            strSql.Append(" where id= @id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HeadAccountLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sep_HeadAccountLog(");
            strSql.Append("HeadAccountId,Remark,AuditDate,Status,AuditorId,Auditor,AuditType)");
            strSql.Append(" values (");
            strSql.Append("@HeadAccountId,@Remark,@AuditDate,@Status,@AuditorId,@Auditor,@AuditType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@HeadAccountId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NText),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@AuditorId", SqlDbType.Int,4),
                    new SqlParameter("@Auditor", SqlDbType.NVarChar),
                    new SqlParameter("@AuditType", SqlDbType.Int,4)
                                        
                                        };
            parameters[0].Value = model.HeadAccountId;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.AuditDate;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.AuditorId;
            parameters[5].Value = model.Auditor;
            parameters[6].Value = model.AuditType;

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
        public int Add(HeadAccountLogInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sep_HeadAccountLog(");
            strSql.Append("HeadAccountId,Remark,AuditDate,Status,AuditorId,Auditor,AuditType)");
            strSql.Append(" values (");
            strSql.Append("@HeadAccountId,@Remark,@AuditDate,@Status,@AuditorId,@Auditor,@AuditType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@HeadAccountId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NText),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@AuditorId", SqlDbType.Int,4),
                    new SqlParameter("@Auditor", SqlDbType.NVarChar),
                    new SqlParameter("@AuditType", SqlDbType.Int,4)
                                        
                                        };
            parameters[0].Value = model.HeadAccountId;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.AuditDate;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.AuditorId;
            parameters[5].Value = model.Auditor;
            parameters[6].Value = model.AuditType;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection,trans, parameters);
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
        public int Update(HeadAccountLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sep_HeadAccountLog set ");
            strSql.Append("HeadAccountId=@HeadAccountId,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AuditDate=@AuditDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("Auditor=@Auditor,");
            strSql.Append("AuditType=@AuditType");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),      
					new SqlParameter("@HeadAccountId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NText),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@AuditorId", SqlDbType.Int,4),
                    new SqlParameter("@Auditor", SqlDbType.NVarChar),
                    new SqlParameter("@AuditType", SqlDbType.Int,4)      
                                        };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.HeadAccountId;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.AuditDate;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.AuditorId;
            parameters[6].Value = model.Auditor;
            parameters[7].Value = model.AuditType;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Sep_HeadAccountLog ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HeadAccountLogInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sep_HeadAccountLog ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            HeadAccountLogInfo model = new HeadAccountLogInfo();
            return CBO.FillObject<HeadAccountLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Sep_HeadAccountLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<HeadAccountLogInfo> GetModelList(string strWhere, SqlParameter[] param)
        {
            string strSql = "select * from Sep_HeadAccountLog where 1=1 ";
            strSql += strWhere;
            strSql += " order by id desc";
            return CBO.FillCollection<HeadAccountLogInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<HeadAccountLogInfo> GetModelList(string strWhere)
        {
            string strSql = "select * from Sep_HeadAccountLog where 1=1 ";
            strSql += strWhere;
            strSql += " order by AuditDate asc";
            return CBO.FillCollection<HeadAccountLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion  成员方法
    }
}
