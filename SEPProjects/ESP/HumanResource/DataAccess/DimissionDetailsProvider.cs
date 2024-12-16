using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ESP.HumanResource.Utilities;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionDetailsProvider
    {
        public DimissionDetailsProvider()
        { }
        
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DimissionDetailId", "SEP_DimissionDetails");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(1) FROM SEP_DimissionDetails");
            strSql.Append(" WHERE DimissionDetailId= @DimissionDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionDetailId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionDetails(");
            strSql.Append("DimissionId,FormId,FormCode,FormType,UserId,UserName,ProjectId,ProjectCode,Description,TotalPrice,FormStatus,ReceiverId,ReceiverName,ReceiverDepartmentId,ReceiverDepartmentName,Status,Remark,CreateTime,ReceiverTime,Website,Url,UpdateStatus)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@FormId,@FormCode,@FormType,@UserId,@UserName,@ProjectId,@ProjectCode,@Description,@TotalPrice,@FormStatus,@ReceiverId,@ReceiverName,@ReceiverDepartmentId,@ReceiverDepartmentName,@Status,@Remark,@CreateTime,@ReceiverTime,@Website,@Url,@UpdateStatus)");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormCode", SqlDbType.NVarChar),
					new SqlParameter("@FormType", SqlDbType.NVarChar),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FormStatus", SqlDbType.Int,4),
					new SqlParameter("@ReceiverId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverName", SqlDbType.NVarChar),
					new SqlParameter("@ReceiverDepartmentId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverDepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ReceiverTime", SqlDbType.DateTime),
					new SqlParameter("@Website", SqlDbType.NVarChar),
					new SqlParameter("@Url", SqlDbType.NVarChar),
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.FormId;
            parameters[2].Value = model.FormCode;
            parameters[3].Value = model.FormType;
            parameters[4].Value = model.UserId;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.ProjectId;
            parameters[7].Value = model.ProjectCode;
            parameters[8].Value = model.Description;
            parameters[9].Value = model.TotalPrice;
            parameters[10].Value = model.FormStatus;
            parameters[11].Value = model.ReceiverId;
            parameters[12].Value = model.ReceiverName;
            parameters[13].Value = model.ReceiverDepartmentId;
            parameters[14].Value = model.ReceiverDepartmentName;
            parameters[15].Value = model.Status;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.CreateTime;
            parameters[18].Value = model.ReceiverTime;
            parameters[19].Value = model.Website;
            parameters[20].Value = model.Url;
            parameters[21].Value = model.UpdateStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return model.DimissionDetailId;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionDetailsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionDetails(");
            strSql.Append("DimissionId,FormId,FormCode,FormType,UserId,UserName,ProjectId,ProjectCode,Description,TotalPrice,FormStatus,ReceiverId,ReceiverName,ReceiverDepartmentId,ReceiverDepartmentName,Status,Remark,CreateTime,ReceiverTime,Website,Url,UpdateStatus)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@FormId,@FormCode,@FormType,@UserId,@UserName,@ProjectId,@ProjectCode,@Description,@TotalPrice,@FormStatus,@ReceiverId,@ReceiverName,@ReceiverDepartmentId,@ReceiverDepartmentName,@Status,@Remark,@CreateTime,@ReceiverTime,@Website,@Url,@UpdateStatus)");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormCode", SqlDbType.NVarChar),
					new SqlParameter("@FormType", SqlDbType.NVarChar),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FormStatus", SqlDbType.Int,4),
					new SqlParameter("@ReceiverId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverName", SqlDbType.NVarChar),
					new SqlParameter("@ReceiverDepartmentId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverDepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ReceiverTime", SqlDbType.DateTime),
					new SqlParameter("@Website", SqlDbType.NVarChar),
					new SqlParameter("@Url", SqlDbType.NVarChar),
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.FormId;
            parameters[2].Value = model.FormCode;
            parameters[3].Value = model.FormType;
            parameters[4].Value = model.UserId;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.ProjectId;
            parameters[7].Value = model.ProjectCode;
            parameters[8].Value = model.Description;
            parameters[9].Value = model.TotalPrice;
            parameters[10].Value = model.FormStatus;
            parameters[11].Value = model.ReceiverId;
            parameters[12].Value = model.ReceiverName;
            parameters[13].Value = model.ReceiverDepartmentId;
            parameters[14].Value = model.ReceiverDepartmentName;
            parameters[15].Value = model.Status;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.CreateTime;
            parameters[18].Value = model.ReceiverTime;
            parameters[19].Value = model.Website;
            parameters[20].Value = model.Url;
            parameters[21].Value = model.UpdateStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            return model.DimissionDetailId;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionDetails SET ");
            strSql.Append("FormId=@FormId,");
            strSql.Append("FormCode=@FormCode,");
            strSql.Append("FormType=@FormType,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("FormStatus=@FormStatus,");
            strSql.Append("ReceiverId=@ReceiverId,");
            strSql.Append("ReceiverName=@ReceiverName,");
            strSql.Append("ReceiverDepartmentId=@ReceiverDepartmentId,");
            strSql.Append("ReceiverDepartmentName=@ReceiverDepartmentName,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("ReceiverTime=@ReceiverTime,");
            strSql.Append("Website=@Website,");
            strSql.Append("Url=@Url,");
            strSql.Append("UpdateStatus=@UpdateStatus ");
            strSql.Append(" WHERE DimissionDetailId=@DimissionDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormCode", SqlDbType.NVarChar),
					new SqlParameter("@FormType", SqlDbType.NVarChar),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FormStatus", SqlDbType.Int,4),
					new SqlParameter("@ReceiverId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverName", SqlDbType.NVarChar),
					new SqlParameter("@ReceiverDepartmentId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverDepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ReceiverTime", SqlDbType.DateTime),
					new SqlParameter("@Website", SqlDbType.NVarChar),
					new SqlParameter("@Url", SqlDbType.NVarChar),
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4)};
            parameters[0].Value = model.DimissionDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.FormId;
            parameters[3].Value = model.FormCode;
            parameters[4].Value = model.FormType;
            parameters[5].Value = model.UserId;
            parameters[6].Value = model.UserName;
            parameters[7].Value = model.ProjectId;
            parameters[8].Value = model.ProjectCode;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.TotalPrice;
            parameters[11].Value = model.FormStatus;
            parameters[12].Value = model.ReceiverId;
            parameters[13].Value = model.ReceiverName;
            parameters[14].Value = model.ReceiverDepartmentId;
            parameters[15].Value = model.ReceiverDepartmentName;
            parameters[16].Value = model.Status;
            parameters[17].Value = model.Remark;
            parameters[18].Value = model.CreateTime;
            parameters[19].Value = model.ReceiverTime;
            parameters[20].Value = model.Website;
            parameters[21].Value = model.Url;
            parameters[22].Value = model.UpdateStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionDetailsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("FormId=@FormId,");
            strSql.Append("FormCode=@FormCode,");
            strSql.Append("FormType=@FormType,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("FormStatus=@FormStatus,");
            strSql.Append("ReceiverId=@ReceiverId,");
            strSql.Append("ReceiverName=@ReceiverName,");
            strSql.Append("ReceiverDepartmentId=@ReceiverDepartmentId,");
            strSql.Append("ReceiverDepartmentName=@ReceiverDepartmentName,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("ReceiverTime=@ReceiverTime,");
            strSql.Append("Website=@Website,");
            strSql.Append("Url=@Url,");
            strSql.Append("UpdateStatus=@UpdateStatus ");
            strSql.Append(" WHERE DimissionDetailId=@DimissionDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@FormCode", SqlDbType.NVarChar),
					new SqlParameter("@FormType", SqlDbType.NVarChar),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@FormStatus", SqlDbType.Int,4),
					new SqlParameter("@ReceiverId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverName", SqlDbType.NVarChar),
					new SqlParameter("@ReceiverDepartmentId", SqlDbType.Int,4),
					new SqlParameter("@ReceiverDepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ReceiverTime", SqlDbType.DateTime),
					new SqlParameter("@Website", SqlDbType.NVarChar),
					new SqlParameter("@Url", SqlDbType.NVarChar),
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4)};
            parameters[0].Value = model.DimissionDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.FormId;
            parameters[3].Value = model.FormCode;
            parameters[4].Value = model.FormType;
            parameters[5].Value = model.UserId;
            parameters[6].Value = model.UserName;
            parameters[7].Value = model.ProjectId;
            parameters[8].Value = model.ProjectCode;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.TotalPrice;
            parameters[11].Value = model.FormStatus;
            parameters[12].Value = model.ReceiverId;
            parameters[13].Value = model.ReceiverName;
            parameters[14].Value = model.ReceiverDepartmentId;
            parameters[15].Value = model.ReceiverDepartmentName;
            parameters[16].Value = model.Status;
            parameters[17].Value = model.Remark;
            parameters[18].Value = model.CreateTime;
            parameters[19].Value = model.ReceiverTime;
            parameters[20].Value = model.Website;
            parameters[21].Value = model.Url;
            parameters[22].Value = model.UpdateStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DimissionDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE SEP_DimissionDetails ");
            strSql.Append(" WHERE DimissionDetailId=@DimissionDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionDetailId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionDetailsInfo GetModel(int DimissionDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionDetails ");
            strSql.Append(" WHERE DimissionDetailId=@DimissionDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionDetailId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionDetailId;
            ESP.HumanResource.Entity.DimissionDetailsInfo model = new ESP.HumanResource.Entity.DimissionDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.DimissionDetailId = DimissionDetailId;
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_DimissionDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public IList<ESP.HumanResource.Entity.DimissionDetailsInfo> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_DimissionDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return CBO.FillCollection<ESP.HumanResource.Entity.DimissionDetailsInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得待处理的单据信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        public List<ESP.HumanResource.Entity.DimissionDetailsInfo> GetDimissionDataByUserId(int userId)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("GetDimissionData", userId);
            System.Data.DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> dimissionDetailList = new List<ESP.HumanResource.Entity.DimissionDetailsInfo>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    ESP.HumanResource.Entity.DimissionDetailsInfo dimissionDetailInfo = new ESP.HumanResource.Entity.DimissionDetailsInfo();
                    if (r["FormId"].ToString() != "")
                    {
                        dimissionDetailInfo.FormId = int.Parse(r["FormId"].ToString());
                    }
                    dimissionDetailInfo.FormCode = r["FormCode"].ToString();
                    dimissionDetailInfo.FormType = r["FormType"].ToString();
                    if (r["UserId"].ToString() != "")
                    {
                        dimissionDetailInfo.UserId = int.Parse(r["UserId"].ToString());
                    }
                    dimissionDetailInfo.UserName = r["UserName"].ToString();
                    if (r["ProjectId"].ToString() != "")
                    {
                        dimissionDetailInfo.ProjectId = int.Parse(r["ProjectId"].ToString());
                    }
                    dimissionDetailInfo.ProjectCode = r["ProjectCode"].ToString();
                    dimissionDetailInfo.Description = r["Description"].ToString();
                    if (r["TotalPrice"].ToString() != "")
                    {
                        dimissionDetailInfo.TotalPrice = decimal.Parse(r["TotalPrice"].ToString());
                    }
                    if (r["Status"].ToString() != "")
                    {
                        dimissionDetailInfo.FormStatus = int.Parse(r["Status"].ToString());
                    }
                    dimissionDetailInfo.Website = r["website"].ToString();
                    dimissionDetailInfo.Url = r["url"].ToString();
                    dimissionDetailList.Add(dimissionDetailInfo);
                }
            }
            return dimissionDetailList;
        }

        public DataSet GetDimissionOOPByUserId(int userId)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("DimissionOOP", userId);
            return db.ExecuteDataSet(cmd);
        }

        public DataSet GetDimissionTrafficFeeByUserId(int userId)
        { 
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("GetDimissionTraffic", userId);
            return db.ExecuteDataSet(cmd);
        }



        /// <summary>
        /// 通过离职单编号获得未处理的单据信息
        /// </summary>
        /// <param name="dimissionId">离职单编号</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.DimissionDetailsInfo> GetDimissionDataByDimissionId(int dimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionDetails ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;
            
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> list = new List<ESP.HumanResource.Entity.DimissionDetailsInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.HumanResource.Entity.DimissionDetailsInfo model = new ESP.HumanResource.Entity.DimissionDetailsInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过离职单编号获得未处理的单据信息
        /// </summary>
        /// <param name="dimissionId"></param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.DimissionDetailsInfo> GetDimissionDataByDimissionId(int dimissionId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionDetails ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), trans, parameters);
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> list = new List<ESP.HumanResource.Entity.DimissionDetailsInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.HumanResource.Entity.DimissionDetailsInfo model = new ESP.HumanResource.Entity.DimissionDetailsInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得等待员工接收的离职单据信息
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <returns>返回用户离职未处理单据信息</returns>
        public DataSet GetDimissionDetailsByUserId(int ReceiverId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sep_dimissionDetails d ");
            strSql.Append(" LEFT JOIN sep_dimissionform f ON d.dimissionid=f.dimissionid ");
            strSql.Append(" WHERE d.receiverid=@ReceiverId AND d.status=@DetailStatus AND f.status=@DimStatus ");

            SqlParameter[] parameters = {
					new SqlParameter("@ReceiverId", SqlDbType.Int, 4),
                    new SqlParameter("@DetailStatus", SqlDbType.Int, 4),
                    new SqlParameter("@DimStatus", SqlDbType.Int, 4)};
            parameters[0].Value = ReceiverId;
            parameters[1].Value = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
            parameters[2].Value = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 判断用户是否是该单据的交接人
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="formId">单据ID</param>
        /// <param name="formType">单据类型</param>
        /// <returns>返回值大于0表示用户是该单据的交接人，否则表示用户不是该单据的交接人</returns>
        public int GetDimissionDetail(int userId, int formId, string formType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionDetails ");
            strSql.Append(" WHERE ReceiverId=@ReceiverId and FormId=@FormId and FormType =@FormType");
            SqlParameter[] parameters = {
					new SqlParameter("@ReceiverId", SqlDbType.Int,4),
                    new SqlParameter("@FormId", SqlDbType.Int,4),
                     new SqlParameter("@FormType", SqlDbType.NVarChar,2000)
                                        };
            parameters[0].Value = userId;
            parameters[1].Value = formId;
            parameters[2].Value = formType;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows.Count;
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>
        /// 获得为修改的单据信息
        /// </summary>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.DimissionDetailsInfo> GetNotUpdateDimissionDetail()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionDetails ");
            strSql.Append(" WHERE UpdateStatus=@UpdateStatus AND Status=@Status");
            SqlParameter[] parameters = {
					new SqlParameter("@UpdateStatus", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int, 4)};
            parameters[0].Value = 0; // 0表示未修改，1表示已修改
            parameters[1].Value = (int)ESP.HumanResource.Common.AuditStatus.Audited;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> list = new List<ESP.HumanResource.Entity.DimissionDetailsInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ESP.HumanResource.Entity.DimissionDetailsInfo model = new ESP.HumanResource.Entity.DimissionDetailsInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}
