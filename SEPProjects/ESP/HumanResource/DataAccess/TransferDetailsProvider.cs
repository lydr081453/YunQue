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
using ESP.HumanResource.Entity;
using System.Net.Mail;

namespace ESP.HumanResource.DataAccess
{
    public class TransferDetailsProvider
    {
        public TransferDetailsProvider() { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.TransferDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_TransferDetails(");
            strSql.Append("transferId,FormId,FormCode,FormType,UserId,UserName,ProjectId,ProjectCode,Description,TotalPrice,FormStatus,ReceiverId,ReceiverName,ReceiverDepartmentId,ReceiverDepartmentName,Status,Remark,CreateTime,ReceiverTime,Website,Url,UpdateStatus,TransGroup)");
            strSql.Append(" VALUES (");
            strSql.Append("@transferId,@FormId,@FormCode,@FormType,@UserId,@UserName,@ProjectId,@ProjectCode,@Description,@TotalPrice,@FormStatus,@ReceiverId,@ReceiverName,@ReceiverDepartmentId,@ReceiverDepartmentName,@Status,@Remark,@CreateTime,@ReceiverTime,@Website,@Url,@UpdateStatus,@TransGroup)");
            SqlParameter[] parameters = {
					new SqlParameter("@transferId", SqlDbType.Int,4),
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
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4),
                    new SqlParameter("@TransGroup", SqlDbType.Int, 4)
                                        };
            parameters[0].Value = model.TransferId;
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
            parameters[19].Value = model.WebSite;
            parameters[20].Value = model.Url;
            parameters[21].Value = model.UpdateStatus;
            parameters[22].Value = model.TransGroup;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return model.Id;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.TransferDetailsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_TransferDetails(");
            strSql.Append("transferId,FormId,FormCode,FormType,UserId,UserName,ProjectId,ProjectCode,Description,TotalPrice,FormStatus,ReceiverId,ReceiverName,ReceiverDepartmentId,ReceiverDepartmentName,Status,Remark,CreateTime,ReceiverTime,Website,Url,UpdateStatus,TransGroup)");
            strSql.Append(" VALUES (");
            strSql.Append("@transferId,@FormId,@FormCode,@FormType,@UserId,@UserName,@ProjectId,@ProjectCode,@Description,@TotalPrice,@FormStatus,@ReceiverId,@ReceiverName,@ReceiverDepartmentId,@ReceiverDepartmentName,@Status,@Remark,@CreateTime,@ReceiverTime,@Website,@Url,@UpdateStatus,@TransGroup)");
            SqlParameter[] parameters = {
					new SqlParameter("@transferId", SqlDbType.Int,4),
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
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4),
                    new SqlParameter("@TransGroup", SqlDbType.Int, 4)
                                        };
            parameters[0].Value = model.TransferId;
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
            parameters[19].Value = model.WebSite;
            parameters[20].Value = model.Url;
            parameters[21].Value = model.UpdateStatus;
            parameters[22].Value = model.TransGroup;


            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            return model.Id;
        }



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.HumanResource.Entity.TransferDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_TransferDetails SET transferId=@transferId,");
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
            strSql.Append("UpdateStatus=@UpdateStatus,TransGroup=@TransGroup ");
            strSql.Append(" WHERE Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@transferId", SqlDbType.Int,4),
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
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4),
                    new SqlParameter("@TransGroup", SqlDbType.Int, 4)                
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TransferId;
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
            parameters[20].Value = model.WebSite;
            parameters[21].Value = model.Url;
            parameters[22].Value = model.UpdateStatus;
            parameters[23].Value = model.TransGroup;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.HumanResource.Entity.TransferDetailsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_TransferDetails SET ");
            strSql.Append("transferId=@transferId,");
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
            strSql.Append("UpdateStatus=@UpdateStatus,TransGroup=@TransGroup ");
            strSql.Append(" WHERE Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@transferId", SqlDbType.Int,4),
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
                    new SqlParameter("@UpdateStatus", SqlDbType.Int, 4),
                     new SqlParameter("@TransGroup", SqlDbType.Int, 4)
                                        
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TransferId;
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
            parameters[20].Value = model.WebSite;
            parameters[21].Value = model.Url;
            parameters[22].Value = model.UpdateStatus;
            parameters[23].Value = model.TransGroup;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE SEP_TransferDetails ");
            strSql.Append(" WHERE Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
            parameters[0].Value = Id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.TransferDetailsInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_TransferDetails ");
            strSql.Append(" WHERE Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<TransferDetailsInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }


        public IList<ESP.HumanResource.Entity.TransferDetailsInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_TransferDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            if (parms==null)
                return CBO.FillCollection<TransferDetailsInfo>(DbHelperSQL.Query(strSql.ToString()));
            else
            return CBO.FillCollection<TransferDetailsInfo>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));

        }

        public List<ESP.Framework.Entity.UserInfo> GetReceiverInfo(int transferId)
        {
             StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sep_users where userid in(SELECT distinct receiverId  FROM SEP_TransferDetails where transferId=@transferId and receiverid<>0)");
             SqlParameter[] parameters = {
					new SqlParameter("@transferId", SqlDbType.Int,4)
                                         };

             parameters[0].Value = transferId;
             return CBO.FillCollection<ESP.Framework.Entity.UserInfo>(DbHelperSQL.Query(strSql.ToString(), parameters.ToArray()));

        }


        public IList<ESP.HumanResource.Entity.TransferDetailsInfo> GetList(string strWhere, List<SqlParameter> parms,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_TransferDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return CBO.FillCollection<TransferDetailsInfo>(DbHelperSQL.Query(strSql.ToString(),trans, parms.ToArray()));

        }

        #endregion  成员方法



        #region 转组

        public int TransProject(int projectId, int newGroupId, string newGroupName, int oldGroupId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update F_Project set GroupID =@GroupID,GroupName=@GroupName where ProjectId =@ProjectId;");
            strSql.Append("update T_GeneralInfo set DepartmentId=@GroupID,Department=@GroupName where project_id =@ProjectId and DepartmentId=@OldGroupId;");
            strSql.Append("update F_Return set DepartmentId=@GroupID,DepartmentName=@GroupName where projectid =@ProjectId and DepartmentId=@OldGroupId;");

            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
                    new SqlParameter("@GroupName", SqlDbType.NVarChar),
					new SqlParameter("@OldGroupId", SqlDbType.Int,4)
                                          };

            parameters[0].Value = projectId;
            parameters[1].Value = newGroupId;
            parameters[2].Value = newGroupName;
            parameters[3].Value = oldGroupId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }


        public int TransSupporter(int supporterId, int newGroupId, string newGroupName, int oldGroupId, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update F_Supporter set GroupID =@GroupID,GroupName=@GroupName where SupportID =@SupportID");
            strSql.Append("update T_GeneralInfo set DepartmentId=@GroupID,Department=@GroupName where project_id =@ProjectId and DepartmentId=@OldGroupId;");
            strSql.Append("update F_Return set DepartmentId=@GroupID,DepartmentName=@GroupName where projectid =@ProjectId and DepartmentId=@OldGroupId;");

            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
                    new SqlParameter("@GroupName", SqlDbType.NVarChar),
					new SqlParameter("@OldGroupId", SqlDbType.Int,4)
                                          };

            parameters[0].Value = supporterId;
            parameters[1].Value = newGroupId;
            parameters[2].Value = newGroupName;
            parameters[3].Value = oldGroupId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }
        #endregion

        #region
        public int ReceiverProject(int projectId, EmployeeBaseInfo receiver, EmployeesInPositionsInfo positionModel, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update F_Project set  ApplicantUserID=@ApplicantUserID,ApplicantUserName=@ApplicantUserName,");
            strSql.Append("ApplicantCode=@ApplicantCode,ApplicantEmployeeName=@ApplicantEmployeeName,ApplicantUserEmail=@ApplicantUserEmail,ApplicantUserPhone=@ApplicantUserPhone,");
            strSql.Append("ApplicantUserPosition=@ApplicantUserPosition where ProjectId =@ProjectId;");
            strSql.Append("insert into t_datapermission ");
            strSql.Append("select id,@ApplicantUserID,1,1 from t_datainfo where dataid=@ProjectId and datatype =2");

            SqlParameter[] parameters = {
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar),
                    new SqlParameter("@ApplicantCode", SqlDbType.NVarChar),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar),
                    new SqlParameter("@ApplicantUserEmail", SqlDbType.NVarChar),
                    new SqlParameter("@ApplicantUserPhone", SqlDbType.NVarChar),
                    new SqlParameter("@ApplicantUserPosition", SqlDbType.NVarChar),
                    new SqlParameter("@ProjectId", SqlDbType.Int,4)
                                          };

            parameters[0].Value = receiver.UserID;
            parameters[1].Value = receiver.Username;
            parameters[2].Value = receiver.Code;
            parameters[3].Value = receiver.FullNameCN;
            parameters[4].Value = receiver.InternalEmail;
            parameters[5].Value = receiver.Phone1;
            parameters[6].Value = positionModel.DepartmentPositionName;
            parameters[7].Value = projectId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }

        public int ReceiverSupporter(int  supportId, EmployeeBaseInfo empModel, EmployeesInPositionsInfo positionModel, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update F_Supporter set LeaderUserID=@LeaderUserID,LeaderUserName=@LeaderUserName,LeaderCode=@LeaderCode,LeaderEmployeeName=@LeaderEmployeeName");
            strSql.Append(" where SupportID =@SupportID;");
            strSql.Append("insert into t_datapermission ");
            strSql.Append("select id,@LeaderUserID,1,1 from t_datainfo where dataid =@SupportID and datatype =3");

            SqlParameter[] parameters = {
					new SqlParameter("@LeaderUserID", SqlDbType.Int,4),
					new SqlParameter("@LeaderUserName", SqlDbType.NVarChar),
                    new SqlParameter("@LeaderCode", SqlDbType.NVarChar),
					new SqlParameter("@LeaderEmployeeName", SqlDbType.NVarChar),
                    new SqlParameter("@SupportID", SqlDbType.Int,4)
                                          };

            parameters[0].Value = empModel.UserID;
            parameters[1].Value = empModel.Username;
            parameters[2].Value = empModel.Code;
            parameters[3].Value = empModel.FullNameCN;
            parameters[4].Value = supportId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }
        
        public int ReceiverPR(int prid, EmployeeBaseInfo empModel, EmployeesInPositionsInfo positionModel, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update t_generalinfo set requestor=@requestor,requestorname=@requestorname,requestor_info=@requestor_info,requestor_group=@requestor_group where id =@id;");
            strSql.Append("insert into t_datapermission ");
            strSql.Append("select id,@requestor,1,1 from t_datainfo where dataid =@id and datatype =0");

            strSql.Append("update f_return set RequestorID=@requestor,RequestUserCode=@RequestUserCode,RequestUserName=@RequestUserName,RequestEmployeeName=@requestorname where prid =@id;");
            strSql.Append("insert into t_datapermission ");
            strSql.Append("select id,@requestor,1,1 from t_datainfo where dataid in(select returnid from f_return where prid=@id) and datatype =5");
            SqlParameter[] parameters = {
					new SqlParameter("@requestor", SqlDbType.Int,4),
					new SqlParameter("@requestorname", SqlDbType.NVarChar),
                    new SqlParameter("@requestor_info", SqlDbType.NVarChar),
					new SqlParameter("@requestor_group", SqlDbType.NVarChar),
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@RequestUserCode", SqlDbType.NVarChar),
                    new SqlParameter("@RequestUserName", SqlDbType.NVarChar)
                                          };

            parameters[0].Value = empModel.UserID;
            parameters[1].Value = empModel.FullNameCN;
            parameters[2].Value = empModel.Phone1;
            parameters[3].Value = positionModel.DepartmentPositionName ;
            parameters[4].Value = prid;
            parameters[5].Value = empModel.Code;
            parameters[6].Value = empModel.Username;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }

        public int ReceiverRecipient(int prid, EmployeeBaseInfo empModel, EmployeesInPositionsInfo positionModel, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update t_generalinfo set goods_receiver=@goods_receiver,receivername=@receivername,receiver_info=@receiver_info where id =@id;");
            strSql.Append("insert into t_datapermission ");
            strSql.Append("select id,@goods_receiver,1,1 from t_datainfo where dataid =@id and datatype =0");

         
            SqlParameter[] parameters = {
					new SqlParameter("@goods_receiver", SqlDbType.Int,4),
					new SqlParameter("@receivername", SqlDbType.NVarChar),
                    new SqlParameter("@receiver_info", SqlDbType.NVarChar),
                    new SqlParameter("@id", SqlDbType.Int,4)
                                          };

            parameters[0].Value = empModel.UserID;
            parameters[1].Value = empModel.FullNameCN;
            parameters[2].Value = empModel.Phone1;
            parameters[3].Value = prid;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }

        public int ReceiverRecipientAppend(int prid, EmployeeBaseInfo empModel, EmployeesInPositionsInfo positionModel, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update t_generalinfo set appendReceiver=@appendReceiver,appendReceiverName=@appendReceiverName,appendReceiverInfo=@appendReceiverInfo,appendReceiverGroup=@appendReceiverGroup where id =@id;");

            SqlParameter[] parameters = {
					new SqlParameter("@appendReceiver", SqlDbType.Int,4),
					new SqlParameter("@appendReceiverName", SqlDbType.NVarChar),
                    new SqlParameter("@appendReceiverInfo", SqlDbType.NVarChar),
                    new SqlParameter("@appendReceiverGroup", SqlDbType.NVarChar),
                    new SqlParameter("@id", SqlDbType.Int,4)
                                          };

            parameters[0].Value = empModel.UserID;
            parameters[1].Value = empModel.FullNameCN;
            parameters[2].Value = empModel.Phone1;
            parameters[3].Value = positionModel.DepartmentName;
            parameters[4].Value = prid;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }

        public int ReceiverOOP(int prid, EmployeeBaseInfo empModel, EmployeesInPositionsInfo positionModel, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update f_return set RequestorID=@RequestorID,RequestUserCode=@RequestUserCode,RequestUserName=@RequestUserName,RequestEmployeeName=@RequestEmployeeName where returnid =@id;");
            strSql.Append("insert into t_datapermission ");
            strSql.Append("select id,@RequestorID,1,1 from t_datainfo where dataid =@id and datatype =5");
            SqlParameter[] parameters = {
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@RequestUserCode", SqlDbType.NVarChar),
                    new SqlParameter("@RequestUserName", SqlDbType.NVarChar),
					new SqlParameter("@RequestEmployeeName", SqlDbType.NVarChar),
                    new SqlParameter("@id", SqlDbType.Int,4)
                                          };

            parameters[0].Value = empModel.UserID;
            parameters[1].Value = empModel.Code;
            parameters[2].Value = empModel.Username;
            parameters[3].Value = empModel.FullNameCN;
            parameters[4].Value = prid;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters.ToArray());
        }

        #endregion

    }
}
