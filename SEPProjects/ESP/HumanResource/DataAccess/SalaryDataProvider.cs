using System;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
    public class SalaryDataProvider
    {
        public SalaryDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SalaryInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_SalaryInfo(");
            strSql.Append("createrName,createDate,memo,declareStatus,declarer,declarerName,declareDate,companyID,companyName,departmentID,departmentName,groupID,groupName,groupApproveStatus,groupApprover,groupApproverName,sysid,groupApproveDate,groupHrStatus,groupHr,groupHrName,groupHrDate,userCode,sysUserName,job,payChange,salaryDetailID,operationDate,creater)");
            strSql.Append(" values (");
            strSql.Append("@createrName,@createDate,@memo,@declareStatus,@declarer,@declarerName,@declareDate,@companyID,@companyName,@departmentID,@departmentName,@groupID,@groupName,@groupApproveStatus,@groupApprover,@groupApproverName,@sysid,@groupApproveDate,@groupHrStatus,@groupHr,@groupHrName,@groupHrDate,@userCode,@sysUserName,@job,@payChange,@salaryDetailID,@operationDate,@creater)");
            SqlParameter[] parameters = {					
					new SqlParameter("@createrName", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime),
					new SqlParameter("@memo", SqlDbType.NVarChar,2000),
					new SqlParameter("@declareStatus", SqlDbType.Int,4),
					new SqlParameter("@declarer", SqlDbType.Int,4),
					new SqlParameter("@declarerName", SqlDbType.NVarChar,50),
					new SqlParameter("@declareDate", SqlDbType.SmallDateTime),
					new SqlParameter("@groupApproveStatus", SqlDbType.Int,4),
					new SqlParameter("@groupApprover", SqlDbType.Int,4),
					new SqlParameter("@groupApproverName", SqlDbType.NVarChar,50),
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@groupApproveDate", SqlDbType.SmallDateTime),
					new SqlParameter("@groupHrStatus", SqlDbType.Int,4),
					new SqlParameter("@groupHr", SqlDbType.Int,4),
					new SqlParameter("@groupHrName", SqlDbType.NVarChar,50),
					new SqlParameter("@groupHrDate", SqlDbType.SmallDateTime),
					new SqlParameter("@userCode", SqlDbType.NChar,20),
					new SqlParameter("@sysUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@job", SqlDbType.NVarChar,50),
					new SqlParameter("@payChange", SqlDbType.Int,4),
					new SqlParameter("@salaryDetailID", SqlDbType.Int,4),
					new SqlParameter("@operationDate", SqlDbType.SmallDateTime),
					new SqlParameter("@creater", SqlDbType.Int,4),
                    new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar),
					new SqlParameter("@departmentID", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar),
					new SqlParameter("@groupID", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar)};
            parameters[0].Value = model.createrName;
            parameters[1].Value = model.createDate;
            parameters[2].Value = model.memo;
            parameters[3].Value = model.declareStatus;
            parameters[4].Value = model.declarer;
            parameters[5].Value = model.declarerName;
            parameters[6].Value = model.declareDate;
            parameters[7].Value = model.groupApproveStatus;
            parameters[8].Value = model.groupApprover;
            parameters[9].Value = model.groupApproverName;
            parameters[10].Value = model.sysid;
            parameters[11].Value = model.groupApproveDate;
            parameters[12].Value = model.groupHrStatus;
            parameters[13].Value = model.groupHr;
            parameters[14].Value = model.groupHrName;
            parameters[15].Value = model.groupHrDate;
            parameters[16].Value = model.userCode;
            parameters[17].Value = model.sysUserName;
            parameters[18].Value = model.job;
            parameters[19].Value = model.payChange;
            parameters[20].Value = model.salaryDetailID;
            parameters[21].Value = model.operationDate;
            parameters[22].Value = model.creater;
            parameters[23].Value = model.companyID;
            parameters[24].Value = model.companyName;
            parameters[25].Value = model.departmentID;
            parameters[26].Value = model.departmentName;
            parameters[27].Value = model.groupID;
            parameters[28].Value = model.groupName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
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
        public int Update(SalaryInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_SalaryInfo set ");
            strSql.Append("createrName=@createrName,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("memo=@memo,");
            strSql.Append("companyID=@companyID,");
            strSql.Append("companyName=@companyName,");
            strSql.Append("departmentID=@departmentID,");
            strSql.Append("departmentName=@departmentName,");
            strSql.Append("groupID=@groupID,");
            strSql.Append("groupName=@groupName,");
            strSql.Append("declareStatus=@declareStatus,");
            strSql.Append("declarer=@declarer,");
            strSql.Append("declarerName=@declarerName,");
            strSql.Append("declareDate=@declareDate,");
            strSql.Append("groupApproveStatus=@groupApproveStatus,");
            strSql.Append("groupApprover=@groupApprover,");
            strSql.Append("groupApproverName=@groupApproverName,");
            strSql.Append("sysid=@sysid,");
            strSql.Append("groupApproveDate=@groupApproveDate,");
            strSql.Append("groupHrStatus=@groupHrStatus,");
            strSql.Append("groupHr=@groupHr,");
            strSql.Append("groupHrName=@groupHrName,");
            strSql.Append("groupHrDate=@groupHrDate,");
            strSql.Append("userCode=@userCode,");
            strSql.Append("sysUserName=@sysUserName,");
            strSql.Append("job=@job,");
            strSql.Append("payChange=@payChange,");
            strSql.Append("salaryDetailID=@salaryDetailID,");
            strSql.Append("operationDate=@operationDate,");
            strSql.Append("creater=@creater");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@createrName", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime),
					new SqlParameter("@memo", SqlDbType.NVarChar,2000),
                    new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar),
					new SqlParameter("@departmentID", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar),
					new SqlParameter("@groupID", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar),
					new SqlParameter("@declareStatus", SqlDbType.Int,4),
					new SqlParameter("@declarer", SqlDbType.Int,4),
					new SqlParameter("@declarerName", SqlDbType.NVarChar,50),
					new SqlParameter("@declareDate", SqlDbType.SmallDateTime),
					new SqlParameter("@groupApproveStatus", SqlDbType.Int,4),
					new SqlParameter("@groupApprover", SqlDbType.Int,4),
					new SqlParameter("@groupApproverName", SqlDbType.NVarChar,50),
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@groupApproveDate", SqlDbType.SmallDateTime),
					new SqlParameter("@groupHrStatus", SqlDbType.Int,4),
					new SqlParameter("@groupHr", SqlDbType.Int,4),
					new SqlParameter("@groupHrName", SqlDbType.NVarChar,50),
					new SqlParameter("@groupHrDate", SqlDbType.SmallDateTime),
					new SqlParameter("@userCode", SqlDbType.NChar,20),
					new SqlParameter("@sysUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@job", SqlDbType.NVarChar,50),
					new SqlParameter("@payChange", SqlDbType.Int,4),
					new SqlParameter("@salaryDetailID", SqlDbType.Int,4),
					new SqlParameter("@operationDate", SqlDbType.SmallDateTime),
					new SqlParameter("@creater", SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.createrName;
            parameters[2].Value = model.createDate;
            parameters[3].Value = model.memo;
            parameters[4].Value = model.companyID;
            parameters[5].Value = model.companyName;
            parameters[6].Value = model.departmentID;
            parameters[7].Value = model.departmentName;
            parameters[8].Value = model.groupID;
            parameters[9].Value = model.groupName;
            parameters[10].Value = model.declareStatus;
            parameters[11].Value = model.declarer;
            parameters[12].Value = model.declarerName;
            parameters[13].Value = model.declareDate;
            parameters[14].Value = model.groupApproveStatus;
            parameters[15].Value = model.groupApprover;
            parameters[16].Value = model.groupApproverName;
            parameters[17].Value = model.sysid;
            parameters[18].Value = model.groupApproveDate;
            parameters[19].Value = model.groupHrStatus;
            parameters[20].Value = model.groupHr;
            parameters[21].Value = model.groupHrName;
            parameters[22].Value = model.groupHrDate;
            parameters[23].Value = model.userCode;
            parameters[24].Value = model.sysUserName;
            parameters[25].Value = model.job;
            parameters[26].Value = model.payChange;
            parameters[27].Value = model.salaryDetailID;
            parameters[28].Value = model.operationDate;
            parameters[29].Value = model.creater;
            

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
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
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlConnection conn, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_SalaryInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SalaryInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.*,b.*  FROM SEP_SalaryInfo as a inner join SEP_Snapshots as b on a.salaryDetailID = b.id ");
            strSql.Append(" where a.id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            SalaryInfo model = new SalaryInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.createrName = ds.Tables[0].Rows[0]["createrName"].ToString();
                if (ds.Tables[0].Rows[0]["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(ds.Tables[0].Rows[0]["createDate"].ToString());
                }
                model.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                if (ds.Tables[0].Rows[0]["declareStatus"].ToString() != "")
                {
                    model.declareStatus = int.Parse(ds.Tables[0].Rows[0]["declareStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["declarer"].ToString() != "")
                {
                    model.declarer = int.Parse(ds.Tables[0].Rows[0]["declarer"].ToString());
                }
                model.declarerName = ds.Tables[0].Rows[0]["declarerName"].ToString();
                if (ds.Tables[0].Rows[0]["declareDate"].ToString() != "")
                {
                    model.declareDate = DateTime.Parse(ds.Tables[0].Rows[0]["declareDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["groupApproveStatus"].ToString() != "")
                {
                    model.groupApproveStatus = int.Parse(ds.Tables[0].Rows[0]["groupApproveStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["groupApprover"].ToString() != "")
                {
                    model.groupApprover = int.Parse(ds.Tables[0].Rows[0]["groupApprover"].ToString());
                }
                model.groupApproverName = ds.Tables[0].Rows[0]["groupApproverName"].ToString();
                if (ds.Tables[0].Rows[0]["sysid"].ToString() != "")
                {
                    model.sysid = int.Parse(ds.Tables[0].Rows[0]["sysid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["groupApproveDate"].ToString() != "")
                {
                    model.groupApproveDate = DateTime.Parse(ds.Tables[0].Rows[0]["groupApproveDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["groupHrStatus"].ToString() != "")
                {
                    model.groupHrStatus = int.Parse(ds.Tables[0].Rows[0]["groupHrStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["groupHr"].ToString() != "")
                {
                    model.groupHr = int.Parse(ds.Tables[0].Rows[0]["groupHr"].ToString());
                }
                model.groupHrName = ds.Tables[0].Rows[0]["groupHrName"].ToString();
                if (ds.Tables[0].Rows[0]["groupHrDate"].ToString() != "")
                {
                    model.groupHrDate = DateTime.Parse(ds.Tables[0].Rows[0]["groupHrDate"].ToString());
                }
                model.userCode = ds.Tables[0].Rows[0]["userCode"].ToString();
                model.sysUserName = ds.Tables[0].Rows[0]["sysUserName"].ToString();
                model.job = ds.Tables[0].Rows[0]["job"].ToString();
                if (ds.Tables[0].Rows[0]["payChange"].ToString() != "")
                {
                    model.payChange = int.Parse(ds.Tables[0].Rows[0]["payChange"].ToString());
                }
                if (ds.Tables[0].Rows[0]["salaryDetailID"].ToString() != "")
                {
                    model.salaryDetailID = int.Parse(ds.Tables[0].Rows[0]["salaryDetailID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["operationDate"].ToString() != "")
                {
                    model.operationDate = DateTime.Parse(ds.Tables[0].Rows[0]["operationDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["creater"].ToString() != "")
                {
                    model.creater = int.Parse(ds.Tables[0].Rows[0]["creater"].ToString());
                }                
                    model.nowBasePay = (ds.Tables[0].Rows[0]["nowBasePay"].ToString());
                
                    model.nowMeritPay = (ds.Tables[0].Rows[0]["nowMeritPay"].ToString());
                
                
                    model.newBasePay = (ds.Tables[0].Rows[0]["newBasePay"].ToString());
                
                    model.newMeritPay = (ds.Tables[0].Rows[0]["newMeritPay"].ToString());
                
                if (ds.Tables[0].Rows[0]["companyID"].ToString() != "")
                {
                    model.companyID = int.Parse(ds.Tables[0].Rows[0]["companyID"].ToString());
                }
                model.companyName = ds.Tables[0].Rows[0]["companyName"].ToString();
                if (ds.Tables[0].Rows[0]["departmentID"].ToString() != "")
                {
                    model.departmentID = int.Parse(ds.Tables[0].Rows[0]["departmentID"].ToString());
                }
                model.departmentName = ds.Tables[0].Rows[0]["departmentName"].ToString();
                if (ds.Tables[0].Rows[0]["groupID"].ToString() != "")
                {
                    model.groupID = int.Parse(ds.Tables[0].Rows[0]["groupID"].ToString());
                }
                model.groupName = ds.Tables[0].Rows[0]["groupName"].ToString();

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
            strSql.Append("select a.*,b.*  FROM SEP_SalaryInfo as a inner join SEP_Snapshots as b on a.salaryDetailID = b.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1  " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }  

        #endregion  成员方法
    }
}
