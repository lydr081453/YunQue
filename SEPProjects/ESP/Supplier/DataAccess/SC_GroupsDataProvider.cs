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
    public class SC_GroupsDataProvider
    {
        public SC_GroupsDataProvider()
        { }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        //public int Add(SC_Advice model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into _GroupNameCN(");
        //    strSql.Append("AdviceType,AdviceTitle,AdviceContent,CommitUser,CommitEmail,CommitDate,CommitIp,CommitUserName,CommitType)");
        //    strSql.Append(" values (");
        //    strSql.Append("@AdviceType,@AdviceTitle,@AdviceContent,@CommitUser,@CommitEmail,@CommitDate,@CommitIp,@CommitUserName,@CommitType)");
        //    strSql.Append(";select @@IDENTITY");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@AdviceType", SqlDbType.NChar,10),
        //            new SqlParameter("@AdviceTitle", SqlDbType.NVarChar,100),
        //            new SqlParameter("@AdviceContent", SqlDbType.NVarChar,2000),
        //            new SqlParameter("@CommitUser", SqlDbType.Int,4),
        //            new SqlParameter("@CommitEmail", SqlDbType.NVarChar,100),
        //            new SqlParameter("@CommitDate", SqlDbType.DateTime),
        //            new SqlParameter("@CommitIp", SqlDbType.VarChar,20),
        //            new SqlParameter("@CommitUserName",SqlDbType.NVarChar,50),
        //            new SqlParameter("@CommitType",SqlDbType.Int,4)
        //                                };
        //    parameters[0].Value = model.AdviceType;
        //    parameters[1].Value = model.AdviceTitle;
        //    parameters[2].Value = model.AdviceContent;
        //    parameters[3].Value = model.CommitUser;
        //    parameters[4].Value = model.CommitEmail;
        //    parameters[5].Value = model.CommitDate;
        //    parameters[6].Value = model.CommitIp;
        //    parameters[7].Value = model.CommitUserName;
        //    parameters[8].Value = model.CommitType;

        //    object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
        //    if (obj == null)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //}
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_Groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Groups set ");
            strSql.Append(@"GroupNameCN=@GroupNameCN,GroupNameEN=@GroupNameEN,Phone=@Phone,Address=@Address,
CommitEmail=@CommitEmail,CommitDate=@CommitDate,CommitIp=@CommitIp,CommitUserName=@CommitUserName,CommitType=@CommitType");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@GroupNameCN", SqlDbType.NVarChar,500),
					new SqlParameter("@GroupNameEN", SqlDbType.NVarChar,500),
                    new SqlParameter("@Phone", SqlDbType.NVarChar,500),
                    new SqlParameter("@Address", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@IsApproved", SqlDbType.Bit)
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.GroupNameCN;
            parameters[2].Value = model.GroupNameEN;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.CreatedDate;
            parameters[6].Value = model.ModifiedDate;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.IsApproved;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_Groups ");
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
        public SC_Groups GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_Groups ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return ESP.ConfigCommon.CBO.FillObject<SC_Groups>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM SC_Groups ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Groups> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Groups where 1=1 ");
            strSql.Append(strWhere);
            return ESP.ConfigCommon.CBO.FillCollection<SC_Groups>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_Groups> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Groups>(GetList(""));
        }
    }
}
