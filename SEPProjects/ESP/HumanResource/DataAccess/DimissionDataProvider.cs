using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionDataProvider
    {
        public DimissionDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SEP_DimissionInfo");
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
        public int Add(DimissionInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_DimissionInfo(");
            strSql.Append("userId,userName,joinJobDate,companyID,companyName,departmentID,departmentName,groupID,groupName,dimissionDate,dimissionCause,createDate,creater,departmentMajordomo,departmentMajordomoName,departmentMajordomoStatus,departmentMajordomoMemo,departmentMajordomoDate,groupManager,groupManagerName,groupManagerStatus,groupManagerMemo,groupManagerDate,hrAuditer,hrAuditerName,hrAuditStatus,hrAuditMemo,hrAuditDate,userCode,snapshotsId,isFinish,status)");
            strSql.Append(" values (");
            strSql.Append("@userId,@userName,@joinJobDate,@companyID,@companyName,@departmentID,@departmentName,@groupID,@groupName,@dimissionDate,@dimissionCause,@createDate,@creater,@departmentMajordomo,@departmentMajordomoName,@departmentMajordomoStatus,@departmentMajordomoMemo,@departmentMajordomoDate,@groupManager,@groupManagerName,@groupManagerStatus,@groupManagerMemo,@groupManagerDate,@hrAuditer,@hrAuditerName,@hrAuditStatus,@hrAuditMemo,@hrAuditDate,@userCode,@snapshotsId,@isFinish,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@userName", SqlDbType.NVarChar),
					new SqlParameter("@joinJobDate", SqlDbType.SmallDateTime),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar),
					new SqlParameter("@departmentID", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar),
					new SqlParameter("@groupID", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar),
					new SqlParameter("@dimissionDate", SqlDbType.SmallDateTime),
					new SqlParameter("@dimissionCause", SqlDbType.NVarChar),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime),
					new SqlParameter("@creater", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomo", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomoName", SqlDbType.NVarChar),
					new SqlParameter("@departmentMajordomoStatus", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomoMemo", SqlDbType.NVarChar),
					new SqlParameter("@departmentMajordomoDate", SqlDbType.SmallDateTime),
					new SqlParameter("@groupManager", SqlDbType.Int,4),
					new SqlParameter("@groupManagerName", SqlDbType.NVarChar),
					new SqlParameter("@groupManagerStatus", SqlDbType.Int,4),
					new SqlParameter("@groupManagerMemo", SqlDbType.NVarChar),
					new SqlParameter("@groupManagerDate", SqlDbType.SmallDateTime),
					new SqlParameter("@hrAuditer", SqlDbType.Int,4),
					new SqlParameter("@hrAuditerName", SqlDbType.NVarChar),
					new SqlParameter("@hrAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@hrAuditMemo", SqlDbType.NVarChar),
					new SqlParameter("@hrAuditDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@userCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@snapshotsId",SqlDbType.Int,4),
                    new SqlParameter("@isFinish",SqlDbType.Bit,1),
                    new SqlParameter("@status",SqlDbType.Int,4)};
            parameters[0].Value = model.userId;
            parameters[1].Value = model.userName;
            parameters[2].Value = model.joinJobDate;
            parameters[3].Value = model.companyID;
            parameters[4].Value = model.companyName;
            parameters[5].Value = model.departmentID;
            parameters[6].Value = model.departmentName;
            parameters[7].Value = model.groupID;
            parameters[8].Value = model.groupName;
            parameters[9].Value = model.dimissionDate;
            parameters[10].Value = model.dimissionCause;
            parameters[11].Value = model.createDate;
            parameters[12].Value = model.creater;
            parameters[13].Value = model.departmentMajordomo;
            parameters[14].Value = model.departmentMajordomoName;
            parameters[15].Value = model.departmentMajordomoStatus;
            parameters[16].Value = model.departmentMajordomoMemo;
            parameters[17].Value = model.departmentMajordomoDate;
            parameters[18].Value = model.groupManager;
            parameters[19].Value = model.groupManagerName;
            parameters[20].Value = model.groupManagerStatus;
            parameters[21].Value = model.groupManagerMemo;
            parameters[22].Value = model.groupManagerDate;
            parameters[23].Value = model.hrAuditer;
            parameters[24].Value = model.hrAuditerName;
            parameters[25].Value = model.hrAuditStatus;
            parameters[26].Value = model.hrAuditMemo;
            parameters[27].Value = model.hrAuditDate;
            parameters[28].Value = model.userCode;
            parameters[29].Value = model.snapshotsId;
            parameters[30].Value = model.isFinish;
            parameters[31].Value = model.status;

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
        public int Update(DimissionInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_DimissionInfo set ");
            strSql.Append("userId=@userId,");
            strSql.Append("userName=@userName,");
            strSql.Append("joinJobDate=@joinJobDate,");
            strSql.Append("companyID=@companyID,");
            strSql.Append("companyName=@companyName,");
            strSql.Append("departmentID=@departmentID,");
            strSql.Append("departmentName=@departmentName,");
            strSql.Append("groupID=@groupID,");
            strSql.Append("groupName=@groupName,");
            strSql.Append("dimissionDate=@dimissionDate,");
            strSql.Append("dimissionCause=@dimissionCause,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("creater=@creater,");
            strSql.Append("departmentMajordomo=@departmentMajordomo,");
            strSql.Append("departmentMajordomoName=@departmentMajordomoName,");
            strSql.Append("departmentMajordomoStatus=@departmentMajordomoStatus,");
            strSql.Append("departmentMajordomoMemo=@departmentMajordomoMemo,");
            strSql.Append("departmentMajordomoDate=@departmentMajordomoDate,");
            strSql.Append("groupManager=@groupManager,");
            strSql.Append("groupManagerName=@groupManagerName,");
            strSql.Append("groupManagerStatus=@groupManagerStatus,");
            strSql.Append("groupManagerMemo=@groupManagerMemo,");
            strSql.Append("groupManagerDate=@groupManagerDate,");
            strSql.Append("hrAuditer=@hrAuditer,");
            strSql.Append("hrAuditerName=@hrAuditerName,");
            strSql.Append("hrAuditStatus=@hrAuditStatus,");
            strSql.Append("hrAuditMemo=@hrAuditMemo,");
            strSql.Append("hrAuditDate=@hrAuditDate,");
            strSql.Append("userCode=@userCode,");
            strSql.Append("snapshotsId=@snapshotsId,");
            strSql.Append("isFinish=@isFinish, ");
            strSql.Append("status=@status");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@userName", SqlDbType.NVarChar),
					new SqlParameter("@joinJobDate", SqlDbType.SmallDateTime),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar),
					new SqlParameter("@departmentID", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar),
					new SqlParameter("@groupID", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar),
					new SqlParameter("@dimissionDate", SqlDbType.SmallDateTime),
					new SqlParameter("@dimissionCause", SqlDbType.NVarChar),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime),
					new SqlParameter("@creater", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomo", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomoName", SqlDbType.NVarChar),
					new SqlParameter("@departmentMajordomoStatus", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomoMemo", SqlDbType.NVarChar),
					new SqlParameter("@departmentMajordomoDate", SqlDbType.SmallDateTime),
					new SqlParameter("@groupManager", SqlDbType.Int,4),
					new SqlParameter("@groupManagerName", SqlDbType.NVarChar),
					new SqlParameter("@groupManagerStatus", SqlDbType.Int,4),
					new SqlParameter("@groupManagerMemo", SqlDbType.NVarChar),
					new SqlParameter("@groupManagerDate", SqlDbType.SmallDateTime),
					new SqlParameter("@hrAuditer", SqlDbType.Int,4),
					new SqlParameter("@hrAuditerName", SqlDbType.NVarChar),
					new SqlParameter("@hrAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@hrAuditMemo", SqlDbType.NVarChar),
					new SqlParameter("@hrAuditDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@userCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@snapshotsId",SqlDbType.Int,4),
                    new SqlParameter("@isFinish",SqlDbType.Bit,1),
                    new SqlParameter("@status",SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.userId;
            parameters[2].Value = model.userName;
            parameters[3].Value = model.joinJobDate;
            parameters[4].Value = model.companyID;
            parameters[5].Value = model.companyName;
            parameters[6].Value = model.departmentID;
            parameters[7].Value = model.departmentName;
            parameters[8].Value = model.groupID;
            parameters[9].Value = model.groupName;
            parameters[10].Value = model.dimissionDate;
            parameters[11].Value = model.dimissionCause;
            parameters[12].Value = model.createDate;
            parameters[13].Value = model.creater;
            parameters[14].Value = model.departmentMajordomo;
            parameters[15].Value = model.departmentMajordomoName;
            parameters[16].Value = model.departmentMajordomoStatus;
            parameters[17].Value = model.departmentMajordomoMemo;
            parameters[18].Value = model.departmentMajordomoDate;
            parameters[19].Value = model.groupManager;
            parameters[20].Value = model.groupManagerName;
            parameters[21].Value = model.groupManagerStatus;
            parameters[22].Value = model.groupManagerMemo;
            parameters[23].Value = model.groupManagerDate;
            parameters[24].Value = model.hrAuditer;
            parameters[25].Value = model.hrAuditerName;
            parameters[26].Value = model.hrAuditStatus;
            parameters[27].Value = model.hrAuditMemo;
            parameters[28].Value = model.hrAuditDate;
            parameters[29].Value = model.userCode;
            parameters[30].Value = model.snapshotsId;
            parameters[31].Value = model.isFinish;
            parameters[32].Value = model.status;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(DimissionInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_DimissionInfo set ");
            strSql.Append("userId=@userId,");
            strSql.Append("userName=@userName,");
            strSql.Append("joinJobDate=@joinJobDate,");
            strSql.Append("companyID=@companyID,");
            strSql.Append("companyName=@companyName,");
            strSql.Append("departmentID=@departmentID,");
            strSql.Append("departmentName=@departmentName,");
            strSql.Append("groupID=@groupID,");
            strSql.Append("groupName=@groupName,");
            strSql.Append("dimissionDate=@dimissionDate,");
            strSql.Append("dimissionCause=@dimissionCause,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("creater=@creater,");
            strSql.Append("departmentMajordomo=@departmentMajordomo,");
            strSql.Append("departmentMajordomoName=@departmentMajordomoName,");
            strSql.Append("departmentMajordomoStatus=@departmentMajordomoStatus,");
            strSql.Append("departmentMajordomoMemo=@departmentMajordomoMemo,");
            strSql.Append("departmentMajordomoDate=@departmentMajordomoDate,");
            strSql.Append("groupManager=@groupManager,");
            strSql.Append("groupManagerName=@groupManagerName,");
            strSql.Append("groupManagerStatus=@groupManagerStatus,");
            strSql.Append("groupManagerMemo=@groupManagerMemo,");
            strSql.Append("groupManagerDate=@groupManagerDate,");
            strSql.Append("hrAuditer=@hrAuditer,");
            strSql.Append("hrAuditerName=@hrAuditerName,");
            strSql.Append("hrAuditStatus=@hrAuditStatus,");
            strSql.Append("hrAuditMemo=@hrAuditMemo,");
            strSql.Append("hrAuditDate=@hrAuditDate,");
            strSql.Append("userCode=@userCode,");
            strSql.Append("snapshotsId=@snapshotsId,");
            strSql.Append("isFinish=@isFinish, ");
            strSql.Append("status=@status");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@userName", SqlDbType.NVarChar),
					new SqlParameter("@joinJobDate", SqlDbType.SmallDateTime),
					new SqlParameter("@companyID", SqlDbType.Int,4),
					new SqlParameter("@companyName", SqlDbType.NVarChar),
					new SqlParameter("@departmentID", SqlDbType.Int,4),
					new SqlParameter("@departmentName", SqlDbType.NVarChar),
					new SqlParameter("@groupID", SqlDbType.Int,4),
					new SqlParameter("@groupName", SqlDbType.NVarChar),
					new SqlParameter("@dimissionDate", SqlDbType.SmallDateTime),
					new SqlParameter("@dimissionCause", SqlDbType.NVarChar),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime),
					new SqlParameter("@creater", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomo", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomoName", SqlDbType.NVarChar),
					new SqlParameter("@departmentMajordomoStatus", SqlDbType.Int,4),
					new SqlParameter("@departmentMajordomoMemo", SqlDbType.NVarChar),
					new SqlParameter("@departmentMajordomoDate", SqlDbType.SmallDateTime),
					new SqlParameter("@groupManager", SqlDbType.Int,4),
					new SqlParameter("@groupManagerName", SqlDbType.NVarChar),
					new SqlParameter("@groupManagerStatus", SqlDbType.Int,4),
					new SqlParameter("@groupManagerMemo", SqlDbType.NVarChar),
					new SqlParameter("@groupManagerDate", SqlDbType.SmallDateTime),
					new SqlParameter("@hrAuditer", SqlDbType.Int,4),
					new SqlParameter("@hrAuditerName", SqlDbType.NVarChar),
					new SqlParameter("@hrAuditStatus", SqlDbType.Int,4),
					new SqlParameter("@hrAuditMemo", SqlDbType.NVarChar),
					new SqlParameter("@hrAuditDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@userCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@snapshotsId",SqlDbType.Int,4),
                    new SqlParameter("@isFinish",SqlDbType.Bit,1),
                    new SqlParameter("@status",SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.userId;
            parameters[2].Value = model.userName;
            parameters[3].Value = model.joinJobDate;
            parameters[4].Value = model.companyID;
            parameters[5].Value = model.companyName;
            parameters[6].Value = model.departmentID;
            parameters[7].Value = model.departmentName;
            parameters[8].Value = model.groupID;
            parameters[9].Value = model.groupName;
            parameters[10].Value = model.dimissionDate;
            parameters[11].Value = model.dimissionCause;
            parameters[12].Value = model.createDate;
            parameters[13].Value = model.creater;
            parameters[14].Value = model.departmentMajordomo;
            parameters[15].Value = model.departmentMajordomoName;
            parameters[16].Value = model.departmentMajordomoStatus;
            parameters[17].Value = model.departmentMajordomoMemo;
            parameters[18].Value = model.departmentMajordomoDate;
            parameters[19].Value = model.groupManager;
            parameters[20].Value = model.groupManagerName;
            parameters[21].Value = model.groupManagerStatus;
            parameters[22].Value = model.groupManagerMemo;
            parameters[23].Value = model.groupManagerDate;
            parameters[24].Value = model.hrAuditer;
            parameters[25].Value = model.hrAuditerName;
            parameters[26].Value = model.hrAuditStatus;
            parameters[27].Value = model.hrAuditMemo;
            parameters[28].Value = model.hrAuditDate;
            parameters[29].Value = model.userCode;
            parameters[30].Value = model.snapshotsId;
            parameters[31].Value = model.isFinish;
            parameters[32].Value = model.status;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_DimissionInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimissionInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SEP_DimissionInfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DimissionInfo model = new DimissionInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.userId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
                }
                model.userName = ds.Tables[0].Rows[0]["userName"].ToString();
                if (ds.Tables[0].Rows[0]["joinJobDate"].ToString() != "")
                {
                    model.joinJobDate = DateTime.Parse(ds.Tables[0].Rows[0]["joinJobDate"].ToString());
                }
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
                if (ds.Tables[0].Rows[0]["dimissionDate"].ToString() != "")
                {
                    model.dimissionDate = DateTime.Parse(ds.Tables[0].Rows[0]["dimissionDate"].ToString());
                }
                model.dimissionCause = ds.Tables[0].Rows[0]["dimissionCause"].ToString();
                if (ds.Tables[0].Rows[0]["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(ds.Tables[0].Rows[0]["createDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["creater"].ToString() != "")
                {
                    model.creater = int.Parse(ds.Tables[0].Rows[0]["creater"].ToString());
                }
                if (ds.Tables[0].Rows[0]["departmentMajordomo"].ToString() != "")
                {
                    model.departmentMajordomo = int.Parse(ds.Tables[0].Rows[0]["departmentMajordomo"].ToString());
                }
                model.departmentMajordomoName = ds.Tables[0].Rows[0]["departmentMajordomoName"].ToString();
                if (ds.Tables[0].Rows[0]["departmentMajordomoStatus"].ToString() != "")
                {
                    model.departmentMajordomoStatus = int.Parse(ds.Tables[0].Rows[0]["departmentMajordomoStatus"].ToString());
                }
                model.departmentMajordomoMemo = ds.Tables[0].Rows[0]["departmentMajordomoMemo"].ToString();
                if (ds.Tables[0].Rows[0]["departmentMajordomoDate"].ToString() != "")
                {
                    model.departmentMajordomoDate = DateTime.Parse(ds.Tables[0].Rows[0]["departmentMajordomoDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["groupManager"].ToString() != "")
                {
                    model.groupManager = int.Parse(ds.Tables[0].Rows[0]["groupManager"].ToString());
                }
                model.groupManagerName = ds.Tables[0].Rows[0]["groupManagerName"].ToString();
                if (ds.Tables[0].Rows[0]["groupManagerStatus"].ToString() != "")
                {
                    model.groupManagerStatus = int.Parse(ds.Tables[0].Rows[0]["groupManagerStatus"].ToString());
                }
                model.groupManagerMemo = ds.Tables[0].Rows[0]["groupManagerMemo"].ToString();
                if (ds.Tables[0].Rows[0]["groupManagerDate"].ToString() != "")
                {
                    model.groupManagerDate = DateTime.Parse(ds.Tables[0].Rows[0]["groupManagerDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["hrAuditer"].ToString() != "")
                {
                    model.hrAuditer = int.Parse(ds.Tables[0].Rows[0]["hrAuditer"].ToString());
                }
                model.hrAuditerName = ds.Tables[0].Rows[0]["hrAuditerName"].ToString();
                if (ds.Tables[0].Rows[0]["hrAuditStatus"].ToString() != "")
                {
                    model.hrAuditStatus = int.Parse(ds.Tables[0].Rows[0]["hrAuditStatus"].ToString());
                }
                model.hrAuditMemo = ds.Tables[0].Rows[0]["hrAuditMemo"].ToString();
                if (ds.Tables[0].Rows[0]["hrAuditDate"].ToString() != "")
                {
                    model.hrAuditDate = DateTime.Parse(ds.Tables[0].Rows[0]["hrAuditDate"].ToString());
                }
                model.userCode = ds.Tables[0].Rows[0]["userCode"].ToString();
                if (ds.Tables[0].Rows[0]["snapshotsId"].ToString() != "")
                {
                    model.snapshotsId = int.Parse(ds.Tables[0].Rows[0]["snapshotsId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isFinish"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isFinish"].ToString() == "1") || (ds.Tables[0].Rows[0]["isFinish"].ToString().ToLower() == "true"))
                    {
                        model.isFinish = true;
                    }
                    else
                    {
                        model.isFinish = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimissionInfo GetModelByUserID(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SEP_DimissionInfo ");
            strSql.Append(" where userid=@userid");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)};
            parameters[0].Value = userid;
            DimissionInfo model = new DimissionInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.userId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
                }
                model.userName = ds.Tables[0].Rows[0]["userName"].ToString();
                if (ds.Tables[0].Rows[0]["joinJobDate"].ToString() != "")
                {
                    model.joinJobDate = DateTime.Parse(ds.Tables[0].Rows[0]["joinJobDate"].ToString());
                }
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
                if (ds.Tables[0].Rows[0]["dimissionDate"].ToString() != "")
                {
                    model.dimissionDate = DateTime.Parse(ds.Tables[0].Rows[0]["dimissionDate"].ToString());
                }
                model.dimissionCause = ds.Tables[0].Rows[0]["dimissionCause"].ToString();
                if (ds.Tables[0].Rows[0]["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(ds.Tables[0].Rows[0]["createDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["creater"].ToString() != "")
                {
                    model.creater = int.Parse(ds.Tables[0].Rows[0]["creater"].ToString());
                }
                if (ds.Tables[0].Rows[0]["departmentMajordomo"].ToString() != "")
                {
                    model.departmentMajordomo = int.Parse(ds.Tables[0].Rows[0]["departmentMajordomo"].ToString());
                }
                model.departmentMajordomoName = ds.Tables[0].Rows[0]["departmentMajordomoName"].ToString();
                if (ds.Tables[0].Rows[0]["departmentMajordomoStatus"].ToString() != "")
                {
                    model.departmentMajordomoStatus = int.Parse(ds.Tables[0].Rows[0]["departmentMajordomoStatus"].ToString());
                }
                model.departmentMajordomoMemo = ds.Tables[0].Rows[0]["departmentMajordomoMemo"].ToString();
                if (ds.Tables[0].Rows[0]["departmentMajordomoDate"].ToString() != "")
                {
                    model.departmentMajordomoDate = DateTime.Parse(ds.Tables[0].Rows[0]["departmentMajordomoDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["groupManager"].ToString() != "")
                {
                    model.groupManager = int.Parse(ds.Tables[0].Rows[0]["groupManager"].ToString());
                }
                model.groupManagerName = ds.Tables[0].Rows[0]["groupManagerName"].ToString();
                if (ds.Tables[0].Rows[0]["groupManagerStatus"].ToString() != "")
                {
                    model.groupManagerStatus = int.Parse(ds.Tables[0].Rows[0]["groupManagerStatus"].ToString());
                }
                model.groupManagerMemo = ds.Tables[0].Rows[0]["groupManagerMemo"].ToString();
                if (ds.Tables[0].Rows[0]["groupManagerDate"].ToString() != "")
                {
                    model.groupManagerDate = DateTime.Parse(ds.Tables[0].Rows[0]["groupManagerDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["hrAuditer"].ToString() != "")
                {
                    model.hrAuditer = int.Parse(ds.Tables[0].Rows[0]["hrAuditer"].ToString());
                }
                model.hrAuditerName = ds.Tables[0].Rows[0]["hrAuditerName"].ToString();
                if (ds.Tables[0].Rows[0]["hrAuditStatus"].ToString() != "")
                {
                    model.hrAuditStatus = int.Parse(ds.Tables[0].Rows[0]["hrAuditStatus"].ToString());
                }
                model.hrAuditMemo = ds.Tables[0].Rows[0]["hrAuditMemo"].ToString();
                if (ds.Tables[0].Rows[0]["hrAuditDate"].ToString() != "")
                {
                    model.hrAuditDate = DateTime.Parse(ds.Tables[0].Rows[0]["hrAuditDate"].ToString());
                }
                model.userCode = ds.Tables[0].Rows[0]["userCode"].ToString();
                if (ds.Tables[0].Rows[0]["snapshotsId"].ToString() != "")
                {
                    model.snapshotsId = int.Parse(ds.Tables[0].Rows[0]["snapshotsId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isFinish"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isFinish"].ToString() == "1") || (ds.Tables[0].Rows[0]["isFinish"].ToString().ToLower() == "true"))
                    {
                        model.isFinish = true;
                    }
                    else
                    {
                        model.isFinish = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
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
            strSql.Append("select [id],[userId],[userName],[joinJobDate],[companyID],[companyName],[departmentID],[departmentName],[groupID],[groupName],[dimissionDate],[dimissionCause],[createDate],[creater],[departmentMajordomo],[departmentMajordomoName],[departmentMajordomoStatus],[departmentMajordomoMemo],[departmentMajordomoDate],[groupManager],[groupManagerName],[groupManagerStatus],[groupManagerMemo],[groupManagerDate],[hrAuditer],[hrAuditerName],[hrAuditStatus],[hrAuditMemo],[hrAuditDate],[userCode],[snapshotsId],[isFinish],[status] ");
            strSql.Append(" FROM SEP_DimissionInfo ");
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
        public List<DimissionInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            string strSql = "select * from SEP_DimissionInfo where 1=1 ";
            strSql += strWhere;
            strSql += " order by id desc";
            List<DimissionInfo> list = new List<DimissionInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms))
            {
                while (r.Read())
                {
                    DimissionInfo model = new DimissionInfo();
                    model.PopupData(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<DimissionInfo> GetModelList(string strWhere)
        {
            string strSql = "select * from SEP_DimissionInfo where 1=1 ";
            strSql += strWhere;
            List<DimissionInfo> list = new List<DimissionInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    DimissionInfo model = new DimissionInfo();
                    model.PopupData(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        #endregion  成员方法
    }
}
