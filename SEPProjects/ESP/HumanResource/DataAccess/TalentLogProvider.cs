using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    class  TalentLogProvider
    {
        public TalentLogProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TalentLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_TalentLog(");
            strSql.Append("TalentId,AuditorId,AuditorName,Remark,auditDate)");
            strSql.Append(" values (");
            strSql.Append("@TalentId,@AuditorId,@AuditorName,@Remark,@auditDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TalentId", SqlDbType.Int),
                    new SqlParameter("@AuditorId", SqlDbType.Int),
                    new SqlParameter("@AuditorName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@auditDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.TalentId;
            parameters[1].Value = model.AuditorId;
            parameters[2].Value = model.AuditorName;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.auditDate;


            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SEP_TalentLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TalentLogInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from SEP_TalentLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            TalentLogInfo model = new TalentLogInfo();
            return CBO.FillObject<TalentLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<TalentLogInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SEP_TalentLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<TalentLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
