using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.DataAccess;
using System.Configuration;

namespace TimingService
{
    public class DimissionDetail
    {
        /// <summary>
        /// 设置离职交接单据信息
        /// </summary>
        /// <param name="DimissionId">离职单编号</param>
        /// <returns></returns>
        public int DoDimission()
        {
            int ret = 0;
            IList<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetNotUpdateDimissionDetail();
            ESP.Logging.Logger.Add("Test Dimission Detail 2.  count=" + detailList.Count);
            try
            {
                Dictionary<int, string> mailInfo = new Dictionary<int, string>();
                foreach (ESP.HumanResource.Entity.DimissionDetailsInfo model in detailList)
                {
                    string desc = string.Empty;
                    using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            switch (model.FormType)
                            {
                                case "项目号":
                                    {
                                        //变更项目号负责人
                                        ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.FormId, trans);
                                        string logdesc = "离职交接,项目号负责人由" + projectModel.ApplicantEmployeeName + "变更为" + model.ReceiverName;
                                        projectModel.ApplicantUserID = model.ReceiverId;
                                        projectModel.ApplicantEmployeeName = model.ReceiverName;
                                        ESP.Finance.BusinessLogic.ProjectManager.Update(projectModel, trans);
                                        //添加项目号成员
                                        IList<ESP.Finance.Entity.ProjectMemberInfo> memberlist = ESP.Finance.BusinessLogic.ProjectMemberManager.GetList(" projectId=" + projectModel.ProjectId.ToString() + " and MemberUserID=" + model.ReceiverId.ToString(), new List<SqlParameter>(), trans);
                                        if (memberlist == null || memberlist.Count == 0)
                                        {
                                            ESP.Finance.Entity.ProjectMemberInfo member = new ESP.Finance.Entity.ProjectMemberInfo();
                                            member.CreateTime = DateTime.Now;
                                            member.GroupID = model.ReceiverDepartmentId;
                                            member.GroupName = model.ReceiverDepartmentName;
                                            member.MemberUserID = model.ReceiverId;
                                            member.MemberEmployeeName = model.ReceiverName;
                                            member.ProjectId = projectModel.ProjectId;
                                            member.ProjectCode = projectModel.ProjectCode;
                                            ESP.Finance.BusinessLogic.ProjectMemberManager.Add(member, trans);
                                        }
                                        //增加接手人权限
                                        ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Project, model.FormId, trans);
                                        ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                        permission.DataInfoId = data.Id;
                                        permission.IsEditor = true;
                                        permission.IsViewer = true;
                                        permission.UserId = model.ReceiverId;
                                        ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                        //增加日志
                                        ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                                        log.FormType = (int)ESP.Finance.Utility.FormType.Project;
                                        log.FormID = projectModel.ProjectId;
                                        log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                                        log.AuditDate = DateTime.Now;
                                        log.Suggestion = logdesc;
                                        ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                                        // 项目号和支持方变更负责人邮件提醒相关人员
                                        desc = projectModel.ProjectCode + ": 项目号（" + projectModel.GroupName + "）负责人，于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 由 " + model.UserName + " 变更为 " + model.ReceiverName;

                                        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
                                        if (!mailInfo.ContainsKey(branchModel.DepartmentId))
                                            mailInfo.Add(branchModel.DepartmentId, desc);
                                        else
                                            mailInfo[branchModel.DepartmentId] += "<br/>" + desc;
                                        break;
                                    }
                                case "支持方":
                                    {
                                        //变更支持方负责人
                                        ESP.Finance.Entity.SupporterInfo supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(model.FormId, trans);
                                        string logdesc = "离职交接,支持方负责人由" + supporterModel.LeaderEmployeeName + "变更为" + model.ReceiverName;
                                        supporterModel.LeaderUserID = model.ReceiverId;
                                        supporterModel.LeaderEmployeeName = model.ReceiverName;
                                        ESP.Finance.BusinessLogic.SupporterManager.UpdateDimission(supporterModel, trans);
                                        //添加支持方成员
                                        IList<ESP.Finance.Entity.SupportMemberInfo> memberlist = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(" supportId=" + supporterModel.SupportID.ToString() + " and MemberUserID=" + model.ReceiverId.ToString(), new List<SqlParameter>(), trans);
                                        if (memberlist == null || memberlist.Count == 0)
                                        {
                                            ESP.Finance.Entity.SupportMemberInfo member = new ESP.Finance.Entity.SupportMemberInfo();
                                            member.CreateTime = DateTime.Now;
                                            member.GroupID = model.ReceiverDepartmentId;
                                            member.GroupName = model.ReceiverDepartmentName;
                                            member.MemberUserID = model.ReceiverId;
                                            member.MemberEmployeeName = model.ReceiverName;
                                            member.SupportID = supporterModel.SupportID;
                                            ESP.Finance.BusinessLogic.SupportMemberManager.Add(member, trans);
                                        }
                                        //增加接手人权限
                                        ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Supporter, model.FormId, trans);
                                        ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                        permission.DataInfoId = data.Id;
                                        permission.IsEditor = true;
                                        permission.IsViewer = true;
                                        permission.UserId = model.ReceiverId;
                                        ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                        //增加日志
                                        ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                                        log.FormType = (int)ESP.Finance.Utility.FormType.Supporter;
                                        log.FormID = supporterModel.SupportID;
                                        log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                                        log.AuditDate = DateTime.Now;
                                        log.Suggestion = logdesc;
                                        ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                                        // 项目号和支持方变更负责人邮件提醒相关人员
                                        ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);
                                        desc = projectModel.ProjectCode + ": 支持方（" + supporterModel.GroupName + "）负责人，于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 由 " + model.UserName + " 变更为 " + model.ReceiverName;

                                        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
                                        if (!mailInfo.ContainsKey(branchModel.DepartmentId))
                                            mailInfo.Add(branchModel.DepartmentId, desc);
                                        else
                                            mailInfo[branchModel.DepartmentId] += "<br/>" + desc;
                                        break;
                                    }
                                case "PR单":
                                    {

                                        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.FormId, trans);
                                        string logdesc = "离职交接,PR申请人由" + generalModel.requestorname + "变更为" + model.ReceiverName;
                                        generalModel.requestor = model.ReceiverId;
                                        generalModel.requestorname = model.ReceiverName;
                                        generalModel.goods_receiver = model.ReceiverId;
                                        generalModel.receivername = model.ReceiverName;
                                        ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel, trans.Connection, trans);

                                        //增加接手人权限
                                        ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.PR, model.FormId, trans);
                                        if (data != null)
                                        {
                                            ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                            permission.DataInfoId = data.Id;
                                            permission.IsEditor = true;
                                            permission.IsViewer = true;
                                            permission.UserId = model.ReceiverId;
                                            ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                        }
                                        //增加日志
                                        ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                                        log.Des = logdesc;
                                        log.Gid = generalModel.id;
                                        log.PrNo = generalModel.PrNo;
                                        log.Status = 0;
                                        log.LogUserId = 0;
                                        log.LogMedifiedTeme = DateTime.Now;
                                        ESP.Purchase.BusinessLogic.LogManager.AddLog(log, trans);

                                        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetDimissionList("prid=" + generalModel.id.ToString(), trans);
                                        foreach (ESP.Finance.Entity.ReturnInfo returnModel in returnList)
                                        {
                                            logdesc = "离职交接,PN申请人由" + returnModel.RequestEmployeeName + "变更为" + model.ReceiverName;
                                            returnModel.RequestorID = model.ReceiverId;
                                            returnModel.RequestEmployeeName = model.ReceiverName;
                                            ESP.Finance.BusinessLogic.ReturnManager.UpdateDismission(returnModel, trans);
                                            //增加接手人权限
                                            ESP.Purchase.Entity.DataInfo returndata = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Return, returnModel.ReturnID, trans);
                                            ESP.Purchase.Entity.DataPermissionInfo returnpermission = new ESP.Purchase.Entity.DataPermissionInfo();
                                            returnpermission.DataInfoId = returndata.Id;
                                            returnpermission.IsEditor = true;
                                            returnpermission.IsViewer = true;
                                            returnpermission.UserId = model.ReceiverId;
                                            ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(returnpermission, trans);

                                            ESP.Finance.Entity.AuditLogInfo auditlog = new ESP.Finance.Entity.AuditLogInfo();
                                            auditlog.FormType = (int)ESP.Finance.Utility.FormType.Return;
                                            auditlog.FormID = returnModel.ReturnID;
                                            auditlog.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                                            auditlog.AuditDate = DateTime.Now;
                                            auditlog.Suggestion = logdesc;
                                            ESP.Finance.BusinessLogic.AuditLogManager.Add(auditlog, trans);
                                        }
                                        break;
                                    }
                                case "收货":
                                    {
                                        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.FormId, trans);
                                        string logdesc = "离职交接,收货人由" + generalModel.receivername + "变更为" + model.ReceiverName;
                                        generalModel.goods_receiver = model.ReceiverId;
                                        generalModel.receivername = model.ReceiverName;
                                        ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel, trans.Connection, trans);

                                        //增加接手人权限
                                        ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.PR, model.FormId, trans);
                                        if (data != null)
                                        {
                                            ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                            permission.DataInfoId = data.Id;
                                            permission.IsEditor = true;
                                            permission.IsViewer = true;
                                            permission.UserId = model.ReceiverId;
                                            ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                        }
                                        //增加日志
                                        ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                                        log.Des = logdesc;
                                        log.Gid = generalModel.id;
                                        log.PrNo = generalModel.PrNo;
                                        log.Status = 0;
                                        log.LogUserId = 0;
                                        log.LogMedifiedTeme = DateTime.Now;
                                        ESP.Purchase.BusinessLogic.LogManager.AddLog(log, trans);
                                        break;
                                    }
                                case "附加收货":
                                    {
                                        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.FormId, trans);
                                        string logdesc = "离职交接,附加收货人由" + generalModel.appendReceiverName + "变更为" + model.ReceiverName;
                                        generalModel.appendReceiver = model.ReceiverId;
                                        generalModel.appendReceiverName = model.ReceiverName;
                                        generalModel.appendReceiverGroup = model.ReceiverDepartmentName;

                                        ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel, trans.Connection, trans);

                                        //增加接手人权限
                                        ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.PR, model.FormId, trans);
                                        if (data != null)
                                        {
                                            ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                            permission.DataInfoId = data.Id;
                                            permission.IsEditor = true;
                                            permission.IsViewer = true;
                                            permission.UserId = model.ReceiverId;
                                            ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                        }
                                        //增加日志
                                        ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                                        log.Des = logdesc;
                                        log.Gid = generalModel.id;
                                        log.PrNo = generalModel.PrNo;
                                        log.Status = 0;
                                        log.LogUserId = 0;
                                        log.LogMedifiedTeme = DateTime.Now;
                                        ESP.Purchase.BusinessLogic.LogManager.AddLog(log, trans);
                                        break;
                                    }
                            }
                            model.UpdateStatus = 1;
                            ESP.HumanResource.BusinessLogic.DimissionDetailsManager.Update(model, trans);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex);
                        }
                    }
                }

                if (mailInfo != null && mailInfo.Count > 0)
                {
                    foreach (KeyValuePair<int, string> kvp in mailInfo)
                    {
                        List<System.Net.Mail.MailAddress> recipients = new List<System.Net.Mail.MailAddress>();
                        List<string> userIds = new List<string>();

                        userIds = ConfigurationManager.AppSettings["BeijingProjectAccounterChange"].Split(new char[] { ',' }).ToList<string>();

                        foreach (string userid in userIds)
                        {
                            ESP.Framework.Entity.UserInfo empInfo = ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(userid));
                            if (empInfo != null && !string.IsNullOrEmpty(empInfo.Email))
                            {
                                recipients.Add(new System.Net.Mail.MailAddress(empInfo.Email));
                            }
                        }
                        ESP.Mail.MailManager.Send("离职交接", kvp.Value, false, recipients.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex);
            }
            return ret;
        }
    }
}
