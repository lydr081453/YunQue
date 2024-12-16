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
    public class EmpContractProvider
    {
        public EmpContractProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.EmpContractInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sep_empContract(");
            strSql.Append("UserId,Branch,ContractYear,BeginDate,EndDate,SignDate,Status)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserId,@Branch,@ContractYear,@BeginDate,@EndDate,@SignDate,@Status)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Branch", SqlDbType.NVarChar),
					new SqlParameter("@ContractYear", SqlDbType.Int),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@SignDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Branch;
            parameters[2].Value = model.ContractYear;
            parameters[3].Value = model.BeginDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.SignDate;
            parameters[6].Value = model.Status;
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
        public void Update(ESP.HumanResource.Entity.EmpContractInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sep_empContract SET ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("Branch=@Branch,ContractYear=@ContractYear,BeginDate=@BeginDate,EndDate=@EndDate,SignDate=@SignDate,Status=@Status");
            strSql.Append(" WHERE ContractId=@ContractId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Branch", SqlDbType.NVarChar),
					new SqlParameter("@ContractYear", SqlDbType.Int,4),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@SignDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ContractId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Branch;
            parameters[2].Value = model.ContractYear;
            parameters[3].Value = model.BeginDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.SignDate;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.ContractId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int UpdateStatus(int status,string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sep_empContract SET ");
            strSql.Append("Status=@Status where "+term);

            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4)
				};

            parameters[0].Value = status;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ContractId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_empContract ");
            strSql.Append(" where ContractId=@ContractId");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractId", SqlDbType.Int,4)
				};
            parameters[0].Value = ContractId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.EmpContractInfo GetModel(int ContractId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sep_empContract ");
            strSql.Append(" WHERE ContractId=@ContractId");
            SqlParameter[] parameters = {
					new SqlParameter("@ContractId", SqlDbType.Int,4)};
            parameters[0].Value = ContractId;
            ESP.HumanResource.Entity.EmpContractInfo model = new ESP.HumanResource.Entity.EmpContractInfo();
            return CBO.FillObject<EmpContractInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.HumanResource.Entity.EmpContractInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM sep_empContract ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<EmpContractInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
        #endregion  成员方法
    }
}
