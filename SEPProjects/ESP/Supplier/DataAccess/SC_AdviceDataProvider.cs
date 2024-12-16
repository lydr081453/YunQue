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
    public class SC_AdviceDataProvider
    {
        public SC_AdviceDataProvider()
        { }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Advice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Advice(");
            strSql.Append("AdviceType,AdviceTitle,AdviceContent,CommitUser,CommitEmail,CommitDate,CommitIp,CommitUserName,CommitType)");
            strSql.Append(" values (");
            strSql.Append("@AdviceType,@AdviceTitle,@AdviceContent,@CommitUser,@CommitEmail,@CommitDate,@CommitIp,@CommitUserName,@CommitType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AdviceType", SqlDbType.NChar,10),
					new SqlParameter("@AdviceTitle", SqlDbType.NVarChar,100),
                    new SqlParameter("@AdviceContent", SqlDbType.NVarChar,2000),
                    new SqlParameter("@CommitUser", SqlDbType.Int,4),
                    new SqlParameter("@CommitEmail", SqlDbType.NVarChar,100),
                    new SqlParameter("@CommitDate", SqlDbType.DateTime),
                    new SqlParameter("@CommitIp", SqlDbType.VarChar,20),
                    new SqlParameter("@CommitUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@CommitType",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.AdviceType;
            parameters[1].Value = model.AdviceTitle;
            parameters[2].Value = model.AdviceContent;
            parameters[3].Value = model.CommitUser;
            parameters[4].Value = model.CommitEmail;
            parameters[5].Value = model.CommitDate;
            parameters[6].Value = model.CommitIp;
            parameters[7].Value = model.CommitUserName;
            parameters[8].Value = model.CommitType;

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
        public void Update(SC_Advice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Advice set ");
            strSql.Append("AdviceType=@AdviceType,AdviceTitle=@AdviceTitle,AdviceContent=@AdviceContent,CommitUser=@CommitUser,CommitEmail=@CommitEmail,CommitDate=@CommitDate,CommitIp=@CommitIp,CommitUserName=@CommitUserName,CommitType=@CommitType");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@AdviceType", SqlDbType.NChar,10),
					new SqlParameter("@AdviceTitle", SqlDbType.NVarChar,100),
                    new SqlParameter("@AdviceContent", SqlDbType.NVarChar,2000),
                    new SqlParameter("@CommitUser", SqlDbType.Int,4),
                    new SqlParameter("@CommitEmail", SqlDbType.NVarChar,100),
                    new SqlParameter("@CommitDate", SqlDbType.DateTime),
                    new SqlParameter("@CommitIp", SqlDbType.VarChar,20),
                    new SqlParameter("@CommitUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@CommitType",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.AdviceType;
            parameters[2].Value = model.AdviceTitle;
            parameters[3].Value = model.AdviceContent;
            parameters[4].Value = model.CommitUser;
            parameters[5].Value = model.CommitEmail;
            parameters[6].Value = model.CommitDate;
            parameters[7].Value = model.CommitIp;
            parameters[8].Value = model.CommitUserName;
            parameters[9].Value = model.CommitType;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Advice ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
            parameters[0].Value = Id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Advice GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Advice ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return ESP.ConfigCommon.CBO.FillObject<SC_Advice>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM SC_Advice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Advice> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Advice where 1=1 ");
            strSql.Append(strWhere);
            return ESP.ConfigCommon.CBO.FillCollection<SC_Advice>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_Advice> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Advice>(GetList(""));
        }
    }
}
