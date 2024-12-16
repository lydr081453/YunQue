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
    public class EmpLogProvider
    {
        public EmpLogProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EmpLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_emplog(");
            strSql.Append("OperatorId,Operator,UserId,EditTime,OperateType,Remark)");
            strSql.Append(" values (");
            strSql.Append("@OperatorId,@Operator,@UserId,@EditTime,@OperateType,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OperatorId", SqlDbType.Int,8),
					new SqlParameter("@Operator", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@EditTime", SqlDbType.DateTime),
                    new SqlParameter("@OperateType", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,100)};

            parameters[0].Value = model.OperatorId;
            parameters[1].Value = model.Operator;
            parameters[2].Value = model.UserId;
            parameters[3].Value = model.EditTime;
            parameters[4].Value = model.OperateType;
            parameters[5].Value = model.Remark;
          
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
            strSql.Append("delete from sep_emplog ");
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
        public EmpLogInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from sep_emplog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            EmpLogInfo model = new EmpLogInfo();
            return CBO.FillObject<EmpLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<EmpLogInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM sep_emplog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<EmpLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

    }
}
