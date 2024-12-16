using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Linq;
using ESP.Finance.Entity;

public partial class project_ProjectTabList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.GridProject.CausedCallback)
        {
            this.GridProject.DataSource = Search();
            this.GridProject.DataBind();
        }
    }

    private string GetUser()
    {
        string user = string.Empty; // ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        string contractor = ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters();
        string finalAccounter = ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();

        if (!string.IsNullOrEmpty(contractor))
            user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters()+",";
        if (!string.IsNullOrEmpty(finalAccounter))
        user += ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();
        string backup = string.Empty;
        if (!string.IsNullOrEmpty(user))
        {
            DataSet ds = ESP.Framework.BusinessLogic.AuditBackUpManager.GetList(" userid in(" + user + ") and type=1");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                backup += ds.Tables[0].Rows[i]["backupuserid"].ToString().Trim() + ",";
            }
        }

        user = "," + user + "," + backup;

        return user;
    }

    private string GetDelegateUser()
    {
        string users = string.Empty;
        DataTable dt = ESP.Framework.BusinessLogic.AuditBackUpManager.GetList("backupuserid=" + CurrentUser.SysID + " and type=3").Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                users += dt.Rows[i]["UserID"].ToString().Trim() + ",";
            }
        }
        return users.TrimEnd(',');
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        this.GridProject.DataSource = Search();
        this.GridProject.DataBind();
    }

    private string getGroupIds()
    {
        string managerid = string.Empty;
        //if (CurrentUser.SysID == System.Configuration.ConfigurationSettings.AppSettings["TCGAssistant"].ToString().Trim())
        //{
        //    managerid = System.Configuration.ConfigurationSettings.AppSettings["TCGManager"].ToString().Trim();
        //}
        //else
            managerid = CurrentUser.SysID;
        string str = string.Format(" (directorid={0} or managerid={0} or ceoid ={0} or faid={0}) and depid>0 ", managerid);
        DataSet ds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(str);

        var projectAuditList = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(CurrentUserID);
        
        string groupids = string.Empty;
        if (ds != null && ds.Tables[0] != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                groupids += ds.Tables[0].Rows[i]["depid"].ToString() + ",";
            }
        }

        if (projectAuditList != null && projectAuditList.Count > 0)
        {
            foreach (var p in projectAuditList)
            {
                groupids += p.DeptId + ",";
            }
        }


        return groupids.TrimEnd(',');
    }

    private IList<ProjectInfo> Search()
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<SqlParameter>();
        string users = GetUser();
        string BranchCodes = string.Empty;
        string groupids = string.Empty;
        groupids = getGroupIds();
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" (otherfinancialusers like '%," + CurrentUserID + ",%' or projectaccounter=" + CurrentUserID + " or firstfinanceid=" + CurrentUserID  + ")");
        foreach (ESP.Finance.Entity.BranchInfo model in branchList)
        {
            BranchCodes += "'" + model.BranchCode + "',";
        }
        BranchCodes = BranchCodes.TrimEnd(',');
        if (users.IndexOf("," + CurrentUserID.ToString() + ",") < 0)
        {
            string delegateusers = GetDelegateUser();
            if (!string.IsNullOrEmpty(delegateusers))
            {
                term = " and (projectid in(select projectid from f_projectmember where memberuserid=@memberuserid) or projectid in(select distinct projectid from f_audithistory where auditoruserid=@memberuserid or auditoruserid in(" + delegateusers + ")) or creatorid=@memberuserid or applicantUserID=@memberuserid or BusinessPersonId=@memberuserid";
            }
            else
                term = " and (projectid in(select projectid from f_projectmember where memberuserid=@memberuserid) or projectid in(select distinct projectid from f_audithistory where auditoruserid=@memberuserid) or creatorid=@memberuserid or applicantUserID=@memberuserid or BusinessPersonId=@memberuserid";

            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@memberuserid", System.Data.SqlDbType.Int, 4);
            p1.Value = CurrentUserID;
            paramlist.Add(p1);
            if (!string.IsNullOrEmpty(BranchCodes))
            {
                term += " or branchcode in(" + BranchCodes + ")";
            }

            if (!string.IsNullOrEmpty(groupids))
            {
                term += " or groupid in(" + groupids + ")";
            }

            term += ")";

        }


        if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
        {
            string customerIDs = string.Empty;
            IList<ESP.Finance.Entity.CustomerTmpInfo> customerList = ESP.Finance.BusinessLogic.CustomerTmpManager.GetList(" NameCN1 like '%" + this.txtKey.Text.Trim() + "%' or  NameEN1 like '%" + this.txtKey.Text.Trim() + "%' ");
            foreach (ESP.Finance.Entity.CustomerTmpInfo cusModel in customerList)
            {
                customerIDs += cusModel.CustomerTmpID.ToString() + ",";
            }
            customerIDs = customerIDs.TrimEnd(',');

            term += " and (serialcode like '%'+@serialcode+'%' or bdprojectcode like '%'+@serialcode+'%' or BusinessDescription like '%'+@serialcode+'%' or projectcode like '%'+@serialcode+'%' or GroupName like '%'+@serialcode+'%' or BranchName like '%'+@serialcode+'%' or BusinessTypeName like '%'+@serialcode+'%' or applicantEmployeename like '%'+@serialcode+'%'";
            if (!string.IsNullOrEmpty(customerIDs))
                term += " or CustomerID in(" + customerIDs + "))";
            else
                term += ")";
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
        }

        if (this.ddlStatus.SelectedItem.Value != "-1")
        {
            switch (this.ddlStatus.SelectedItem.Value)
            {
                case "0":
                    term += " and status in(0,10,30,20)";
                    break;
                case "1":
                    term += " and status in(1,11)";
                    break;
                case "2":
                    term += " and status in(13)";
                    break;
                case "3":
                    term += " and status in(12,19,21,31)";
                    break;
                case "32":
                    term += " and status in (32)";
                    break;
                case "33":
                    term += " and status in (33)";
                    break;
                case "34":
                    term += " and status in (34)";
                    break;
            }
        }

       return ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GridProject.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(GridProject_DeleteCommand);
        GridProject.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridProject_NeedRebind);
        GridProject.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridProject_PageIndexChanged);

    }
    protected void GridProject_NeedRebind(object sender, EventArgs e)
    {
        GridProject.DataBind();
    }

    protected void GridProject_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
    {
        GridProject.CurrentPageIndex = e.NewIndex;
    }

    protected void GridProject_NeedDataSource(object sender, EventArgs e)
    {
        GridProject.DataSource = Search();
    }

    protected void GridProject_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
        int projectId = int.Parse(e.Item["ProjectId"].ToString());
        ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectId);
        if (projectModel.Status == (int)ESP.Finance.Utility.Status.Submit && (projectModel.CreatorID==CurrentUserID || projectModel.ApplicantUserID==CurrentUserID))
        {

            if (ESP.Finance.BusinessLogic.ProjectManager.CancelProject(projectModel, CurrentUser))
            {
                //删除工作流数据
                WorkFlowDAO.ProcessInstanceDao dao = new WorkFlowDAO.ProcessInstanceDao();
                dao.TerminateProcess(projectModel.ProcessID ?? 0, projectModel.InstanceID ?? 0);
            }
        }
        else
        {
            string Msg = "";
            IList<ESP.Finance.Entity.AuditHistoryInfo> auditHist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(" projectid=" + projectModel.ProjectId + " and auditStatus=" + (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);
            if (auditHist != null && auditHist.Count > 0)
            {
                auditHist = auditHist.OrderByDescending(N => N.AuditID).ToList();
                Msg = "项目号已通过" + auditHist[0].AuditorEmployeeName + "审核。";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "不能进行撤销操作！" + "');", true);
        }
    }

    protected void GridProject_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
    {
        //所有部门级联字符串拼接
        ESP.Finance.Entity.ProjectInfo projectModel = (ESP.Finance.Entity.ProjectInfo)e.Item.DataItem;
        List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
        depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(projectModel.GroupID.Value, depList);
        string groupname = string.Empty;
        foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
        {
            groupname += dept.DepartmentName + "-";
        }
        if (!string.IsNullOrEmpty(groupname))
            e.Item["GroupName"] = groupname.Substring(0, groupname.Length - 1);
        e.Item["StatusText"] = State.SetState(projectModel.Status);
        e.Item["ViewAudit"] = "<a href=\"/project/ProjectWorkFlow.aspx?Type=project&FlowID=" + e.Item["ProjectID"].ToString() + "\" target=\"_blank\">" +
                                                        "<img src=\"/images/AuditStatus.gif\" border=\"0px;\" title=\"审批状态\" /></a>";

        e.Item["BusinessDescription"] = "<span title=" + e.Item["BusinessDescription"] + ">" + e.Item["BusinessDescription"] + "</span>";
        e.Item["ApplicantEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectModel.ApplicantUserID) + "');\">" + e.Item["ApplicantEmployeeName"] + "</a>";
        e.Item["GroupName"] = "<span title=" + e.Item["GroupName"] + ">" + e.Item["GroupName"] + "</span>";
        e.Item["ProjectCode"] = "<span title=" + e.Item["ProjectCode"] + ">" + e.Item["ProjectCode"] + "</span>";

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectModel.BranchCode);
        ESP.Finance.Entity.BranchProjectInfo branchProject=null;
        if(branchModel!=null)
            branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectModel.GroupID.Value);

        ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(projectModel.GroupID.Value);
       
        string users = string.Empty;
        if (branchModel != null && branchModel.BranchCode != null)
        {
            users = "," + branchModel.FirstFinanceID.ToString() + "," + branchModel.FinalAccounter.ToString() + "," + branchModel.PaymentAccounter.ToString() + "," + branchModel.OtherFinancialUsers + "," + branchModel.ContractAccounter.ToString() + "," + branchModel.ProjectAccounter.ToString() + ",";
        }
        else
            users = ",";

        if (branchProject != null)
        {
            users += branchProject.AuditorID + ",";
        }
        users += projectModel.ApplicantUserID + ",";
        if (operationModel != null)
            users += operationModel.DirectorId.ToString() + "," + operationModel.ManagerId.ToString() + "," + operationModel.FAId.ToString() + "," + operationModel.CEOId.ToString() + "," + operationModel.RiskControlAccounter.ToString() + operationModel.CostView;

        if (users.IndexOf("," + CurrentUser.SysID + ",") >= 0)
        {
            e.Item["CBX"] = " <a href=\"/CostView/SinglePrjView.aspx?ProjectID=" + projectModel.ProjectId.ToString() + "\"  target=\"_blank\">" +
                                                            "<img src=\"/images/dc.gif\" border=\"0px;\" title=\"查看成本\" /></a>";
        }
        else
        {
            e.Item["CBX"] = "";
        }

        e.Item["View"] = "<a href=\"/project/ProjectDisplay.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + projectModel.ProjectId.ToString() + "\"  target=\"_blank\">" +
                                                       "<img src=\"../images/dc.gif\" border=\"0px;\" title=\"查看\" /></a>";

        if (users.IndexOf("," + CurrentUser.SysID + ",") >= 0 || Convert.ToInt32(CurrentUser.SysID) == projectModel.CreatorID || Convert.ToInt32(CurrentUser.SysID) == projectModel.ApplicantUserID || Convert.ToInt32(CurrentUser.SysID) == projectModel.BusinessPersonId)
        {

            e.Item["Print"] = "<a href='/project/ProjectPrint.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + projectModel.ProjectId.ToString() +
                                                            "' target=\"_blank\"><img title=\"项目号申请单打印预览\" src=\"/images/ProjectPrint.gif\" border=\"0px;\" /></a>";

            string historyurl = string.Format("/project/ProjectHist.aspx?{0}={1}", ESP.Finance.Utility.RequestName.ProjectID, projectModel.ProjectId.ToString());

            e.Item["History"] = "<a href=\"" + historyurl + "\"  target=\"_blank\">" +
                                                        "<img src=\"/images/history.gif\" border=\"0px;\" title=\"历史\" /></a>";

            if (projectModel.Status == (int)ESP.Finance.Utility.Status.FinanceAuditComplete || projectModel.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose || projectModel.Status == (int)ESP.Finance.Utility.Status.Waiting)
            {
                string changeurl = string.Format("/project/ProjectAuditedModify.aspx?{0}={1}&{2}={3}&{4}={5}", RequestName.ProjectID, projectModel.ProjectId.ToString(), RequestName.Operate, "update", RequestName.BackUrl, "/Search/ProjectTabList.aspx");
                e.Item["Change"] = "<a href=\"" + changeurl + "\">" +
                                                                "<img src=\"/images/edit.gif\" border=\"0px;\" title=\"变更\" /></a>";
            }
            if (!string.IsNullOrEmpty(projectModel.ProjectCode))
            {
                string contractUrl = string.Format("/contractfiles/ContractEdit.aspx?{0}={1}&{2}={3}", RequestName.ProjectID, projectModel.ProjectId.ToString(), RequestName.BackUrl, "/Search/ProjectTabList.aspx");
                e.Item["Contract"] = "<a href=\"" + contractUrl + "\">" +
                                                                "<img src=\"/images/edit.gif\" border=\"0px;\" title=\"证据链\" /></a>";

                string applyForInvioceUrl = string.Format("/applyForInvioce/applyForInvioceEdit.aspx?{0}={1}&{2}={3}", RequestName.ProjectID, projectModel.ProjectId.ToString(), RequestName.BackUrl, "/Search/ProjectTabList.aspx");
                e.Item["ApplyForInvioce"] = "<a href=\"" + applyForInvioceUrl + "\">" +
                                                                "<img src=\"/images/Icon_Output.gif\" border=\"0px;\" title=\"发票申请\" /></a>";
            }
        }
        else
        {
            e.Item["Print"] = "";
            e.Item["History"] = "";
            e.Item["Change"] = "";
            e.Item["Cancel"] = "";
            e.Item["Contract"] = "";
        }
    }



}
